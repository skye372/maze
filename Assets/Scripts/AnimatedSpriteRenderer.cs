// all the needed methods from Unity will be imported 
using UnityEngine;

// requieres components of type SpriteRenderer
[RequireComponent(typeof(SpriteRenderer))]
// die Unterklasse AnimatedSpriteRenderer erbt alle Methoden der Oberklasse Monobehavior; Monobehavior ist für alle Unity-Scripts notwendig 
public class AnimatedSpriteRenderer : MonoBehaviour
{
    // creating a private variable of type SpriteRenderer
    private SpriteRenderer spriteRenderer;

    // creating a public variable of type Sprite
    public Sprite idleSprite;
    // creating a public array of type Sprite for the animationSprites
    public Sprite[] animationSprites;

    // creating a public variable of type float and assigning a float number
    public float animationTime = 0.25f;

    // creating a private variable of type int
    private int animationFrame;

    // animations will have a loop
    public bool loop = true;
    // this animation is idle
    public bool idle = true;

    // creating a private method Awake() of type void
    private void Awake()
    {
        // assigning spriteRenderer to get the component of the SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // creating private methods of type void
    private void OnEnable()
    {
        // the spriteRenderer is enabled
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        // the spriteRenderer is disabled
        spriteRenderer.enabled = false;
    }

    private void Start()
    {
        // invoking a function repeatedly;
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }

    private void NextFrame()
    {
        // when its advanced to the next frame it increments the frame
        animationFrame++;

        // if the animation loops and the animation frame is greater than or equal to however many sprites there are in its "array"...
        if (loop && animationFrame >= animationSprites.Length) 
        {
            // ...the animation frame is set back to zero, so we loop back to the very beginning
            animationFrame = 0;
        }

        // it checks if it's idle
        if (idle) 
        {
            // it sets the spriteRenderer.spride to be the idle sprite
            spriteRenderer.sprite = idleSprite;
        } 
        // else if the animationFrame is greaater than/equal to zero and the animationFrame is less than the animation.Sprites.Length...
        else if (animationFrame >= 0 && animationFrame < animationSprites.Length) 
        {
            // ...it sets the spriteRenderer.sprite to be an animation Sprites for the frame it currently is on
            spriteRenderer.sprite = animationSprites[animationFrame];
        }
    }

}
