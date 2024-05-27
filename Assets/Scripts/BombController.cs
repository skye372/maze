// all the needed methods from Unity will be imported 
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

// die Unterklasse BombController erbt alle Methoden der Oberklasse Monobehavior; Monobehavior ist für alle Unity-Scripts notwendig 
public class BombController : MonoBehaviour
{
    // having a header called "Bomb"
    [Header("Bomb")]
    // assigning which key (Space) has to be pressed to set a bomb
    public KeyCode inputKey = KeyCode.Space;
    // creating a public variable of type GameObject
    public GameObject bombPrefab;
    // assigning the time (3 seconds) a bomb takes to explode, after it has been dropped
    public float bombFuseTime = 3f;
    // assigning how many bombs the player can drop at once
    public int bombAmount = 1;
    // creating a private variable of type int to know how many bombs are remaining
    private int bombsRemaining;

    // having a header called "Explosion"
    [Header("Explosion")]
    // creating public variables 
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    // creating public variables and assinging values
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    // having a header called "Destructible"
    [Header("Destructible")]
    // creating public variables 
    public Tilemap destructibleTiles;
    public Destructible destructiblePrefab;

    // creating private methods of type void
    private void OnEnable()
    {
        // setting the bombs remaining to however many bombs the player starts with
        bombsRemaining = bombAmount;
    }

    private void Update()
    {
        // if it has bombs remaining it checks for the input of the key "Space"
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey)) 
        {
            // starting the coroutine and starting the function
            StartCoroutine(PlaceBomb());
        }
    }

    // creating private method of type IEnumerator; it's a coroutine --> special fuction that allows you to spend the executing of this function over time
    private IEnumerator PlaceBomb()
    {
        // the position of the bomb will be assigned to the current position of the player
        Vector2 position = transform.position;
        // rounding up the x & y values to whole numbers, since the player is not always aligned to the grid perfectly
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        // instantiating the bomb; inside the round brackets are: the object that it wants to account, its position and setting it to have no rotation
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        // reducing the bombs, which are remaining, by one
        bombsRemaining--;

        // suspending the execution of the function for a certain amount of time (3 seconds)
        yield return new WaitForSeconds(bombFuseTime);

        // reseting the position to the current position the bomb is (because you can push the bombs and the position could changed since it got instantiated)
        position = bomb.transform.position;
        // rounding up the x & y values to whole numbers, so the explosion alings perfectly to the grid
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        // instantiating the explosion; inside the round brackets are: the object that it wants to account, its position and setting it to have no rotation
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        // seting the ActiveRenderer to the start of the explosion
        explosion.SetActiveRenderer(explosion.start);
        // destroying the gameobject after some certain amount of seconds
        explosion.DestroyAfter(explosionDuration);

        // calling the explode method for every direction
        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        // a medthod that destroys the bomb after the process 
        Destroy(bomb);
        // after the destruction of the bomb, the bomb incremates by one again
        bombsRemaining++;
    }

    // creating a private method of type void, to explode at some position in some direction and to get the length of the explosion; it is a recursive function
    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        // if the length hits zero or less than zero...
        if (length <= 0) 
        {
            // ... it stops exploding
            return;
        }

        // getting the new position of the current explosion
        position += direction;

        // checking if there is a collider that is overlapping some points (=positions), by checking the layers of the gameobjects
        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            // calling the method at this position
            ClearDestructible(position);
            // it stops 
            return;
        }

        // instantiating the explosion; inside the round brackets are: the object that it wants to account, its position and setting it to have no rotation
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        // if the length is greater than one it is still the middle of the explosion, otherwise it is the end
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        // seting the direction of the explosion
        explosion.SetDirection(direction);
        // destroying the gameobject after some certain amount of seconds
        explosion.DestroyAfter(explosionDuration);

        // calling the explode method again and reduce the length by one, so that it recursively repeats the method until the length hit zero
        Explode(position, direction, length - 1);
    }

    private void ClearDestructible(Vector2 position)
    {
        // assigning a variable (cell) to the destructible tiles and to convert to a world position
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        // getting the tile of this cell
        TileBase tile = destructibleTiles.GetTile(cell);

        // if the tile is not null...
        if (tile != null)
        {
            //... then it instantiates the destructible prefab on top of it in the exact same position
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            // when it set the new object on top of it then it will set that tile at the cell position, to be null
            destructibleTiles.SetTile(cell, null);
        }
    }

    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }

    // creating a private method of type void, which uses the Collider2D as class and "other" as parameter
    private void OnTriggerExit2D(Collider2D other)
    {
        // if the layers of other gameobjects equal the layer of the bomb, it knows when it walked off a bomb
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb")) 
        {
            // the collider is not the trigger, so that the sprite can push the bomb
            other.isTrigger = false;
        }
    }

}
