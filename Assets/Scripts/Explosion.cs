// all the needed methods from Unity willbe imported 
using UnityEngine;

// die Unterklasse Explosion erbt alle Methoden der Oberklasse Monobehavior; Monobehavior ist für alle Unity-Scripts notwendig 
public class Explosion : MonoBehaviour
{
    // creating poublic variables of type AnimatedSpriteRenderer for the different sections of the explosion
    public AnimatedSpriteRenderer start;
    public AnimatedSpriteRenderer middle;
    public AnimatedSpriteRenderer end;

    // creating a public method of type void using AnimatedSpriteRenderer as the class and "renderer" as the parameter
    public void SetActiveRenderer(AnimatedSpriteRenderer renderer)
    {
        // assigning the enabled AnimatedSpriteRenderer to the rederer, which equals the start; specify which direction is enabled
        start.enabled = renderer == start;
        middle.enabled = renderer == middle;
        end.enabled = renderer == end;
    }

    // creating a public method of type void, so that it can rotate the sprite to face different directions
    public void SetDirection(Vector2 direction)
    {
        // angle (a variable of type float) gets assigned to Math.Atan2 (to get the tangent) with its y & x directions, to get the angle of the direction vector
        float angle = Mathf.Atan2(direction.y, direction.x);
        // from this direction vector it calculates the rotation
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    // creating a public method of type void to destroy the gameobject after a certain amount of time
    public void DestroyAfter(float seconds)
    {
        // destroying the gameobject component within seconds
        Destroy(gameObject, seconds);
    }

}
