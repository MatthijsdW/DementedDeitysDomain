using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [Header("Animations")]
    public SimpleAnimation downAnimation;
    public SimpleAnimation leftAnimation;
    public SimpleAnimation rightAnimation;
    public SimpleAnimation upAnimation;

    protected SimpleAnimation currentAnimation;
    protected SpriteRenderer spriteRenderer;
    protected CharacterStats characterStats;

    protected int currentAnimationIndex = 0;
    protected float animationTimer;

    protected bool animationChanged;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterStats = GetComponentInParent<CharacterStats>();
    }

    protected virtual void Update()
    {
        float cameraAngle = Camera.main.transform.rotation.eulerAngles.y;
        float spriteAngle = transform.parent.rotation.eulerAngles.y;

        int angleDifference = Mathf.RoundToInt(cameraAngle - spriteAngle);
        if (angleDifference < 0)
            angleDifference += 360;

        SimpleAnimation nextAnimation;
        if (angleDifference <= 45 || angleDifference >= 315)
            nextAnimation = upAnimation;
        else if (angleDifference <= 135)
            nextAnimation = leftAnimation;
        else if (angleDifference < 225)
            nextAnimation = downAnimation;
        else
            nextAnimation = rightAnimation;

        if (currentAnimation == nextAnimation || !nextAnimation.Sprites.Any())
            return;

        currentAnimation = nextAnimation;
        animationChanged = true;
    }
}
