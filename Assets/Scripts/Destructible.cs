// all the needed methods from Unity willbe imported 
using UnityEngine;

// die Unterklasse Destructible erbt alle Methoden der Oberklasse Monobehavior; Monobehavior ist für alle Unity-Scripts notwendig 
public class Destructible : MonoBehaviour
{
    // creating a variable of type float to set the time how long it takes for this object to get destroyed (one second)
    public float destructionTime = 1f;
    [Range(0f, 1f)]
    public float itemSpawnChance = 0.2f;
    public GameObject[] spawnableItems;

    // creating a private method of type void, that gets called the very first frame the script gets enabled
    private void Start()
    {
        // destroying the gameobject after the destructiontime
        Destroy(gameObject, destructionTime);
    }

    private void OnDestroy()
    {
        //SpawnItem();
    }

    private void SpawnItem()
    {
        if (spawnableItems.Length > 0 && Random.value < itemSpawnChance)
        {
            int randomIndex = Random.Range(0, spawnableItems.Length);
            Instantiate(spawnableItems[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
