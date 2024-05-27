// all the needed methods from Unity will be imported 
using UnityEngine;

// requieres components of type Rigidbody2D
[RequireComponent(typeof(Rigidbody2D))]
// die Unterklasse PlayerController erbt alle Methoden der Oberklasse Monobehavior; Monobehavior ist für alle Unity-Scripts notwendig 
public class PlayerController : MonoBehaviour
{
    // creating a private variable of class Rigidbody2D; the Rigidbody detects collision between objects
    private Rigidbody2D rb;
    // creating a private variable of class Vector2; assigning the initial direction (down)
    private Vector2 direction = Vector2.down;
    // creating a public float variable; assigning the the defaulted speed and the f stands for a float number
    public float speed = 5f;

    // giving it a header
    [Header("Input")]
    // creating public variables of class KeyCode; assingning them to keys on the keyboard 
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    // giving it a header
    [Header("Sprites")]
    // creating public variables of class AnimatedSpriteRenderer; assingning them to variables of the directions
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;

    // creating a private Awake Method of type void; Awake() is automatically called by unity; is called before any start functions
    private void Awake()
    {
        // assigning a variable to GetComponent; GetComponent searches in Unity on the object the component "Rigidbody2D"
        rb = GetComponent<Rigidbody2D>();
        // the initial direction is down, so the first activeSpriteRenderer is the spriteRendererDown
        activeSpriteRenderer = spriteRendererDown;
    }

    // creating a Method with the return type void; is automatically called by unity, this method is called every single frame
    private void Update()
    {
        // deciding in which direction the Sprite goes
        // making if else statements; if the input is "the key is pressed" (GetKey checks if a key is prassed every frame); the input variates between diffrent directions
        if (Input.GetKey(inputUp)) {
            // the sprite walks in the chosen direction
            SetDirection(Vector2.up, spriteRendererUp);
        } else if (Input.GetKey(inputDown)) {
            SetDirection(Vector2.down, spriteRendererDown);
        } else if (Input.GetKey(inputLeft)) {
            SetDirection(Vector2.left, spriteRendererLeft);
        } else if (Input.GetKey(inputRight)) {
            SetDirection(Vector2.right, spriteRendererRight);
        } else {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }
    }

    // creating a private Method of type void; is for moving objects; runs in a fixed timeintervall 
    private void FixedUpdate()
    {
        // the current position of the Rigidbody
        Vector2 position = rb.position;
        // how much do we wanna move the rb?; the speed times the time that has elapsed the last time it was called times the current direction
        Vector2 translation = speed * Time.fixedDeltaTime * direction;

        // to move the position: taking the current position plus the translation
        rb.MovePosition(position + translation);
    }

    // creating a private method of type void; setting the direction Vector2 and assigning a variable
    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        // assigning the direction to the newDirection
        direction = newDirection;

        // enableing it if the spriteRenderer that we are passing through our function, is the spriteRendererXYZ; only one spriterendererXYZ can be enabled at a time
        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        // assigning its active spriterenderer to be the spriteRenderer
        activeSpriteRenderer = spriteRenderer;
        // if the direction is anything other than zero, then the activeSpriteRenderer is not idle
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }

    // creating a private method of type void using a 2D collider and "other" as parameter
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the player collides with an explosion...
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion")) 
        {
            //...then the method DeathSequence is called
            DeathSequence();
        }
    }

    // creating a public method of type void
    public void DeathSequence()
    {
        // disableing all the players abilities
        enabled = false;
        // disableing the BombController
        GetComponent<BombController>().enabled = false;

        // de-/ activate the sprites state
        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        // invoking after 1.25 seconds the method OnDeathSequenceEnded
        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    // creating a private method of type void
    private void OnDeathSequenceEnded()
    {
        // completely disabling the gameobject
        gameObject.SetActive(false);
        // calling the methode from GameOver fromm the GameManager
        GameManager.Instance.GameOver();
    }

}
