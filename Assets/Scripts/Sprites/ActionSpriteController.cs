using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionSpriteController : SpriteController
{
    protected override void Update()
    {
        base.Update();

        if (animationChanged)
        {
            spriteRenderer.sprite = currentAnimation.Sprites[currentAnimationIndex];
            animationChanged = false;
        }
    }

    public void TriggerAnimation(float animationTime)
    {
        if (currentAnimation.Sprites.Count > 1)
        {
            currentAnimationIndex = 1;
            animationTimer = animationTime;
            spriteRenderer.sprite = currentAnimation.Sprites[currentAnimationIndex];
        }
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;

        if (currentAnimation != null && currentAnimation.Sprites.Any())
        {
            animationTimer -= Time.deltaTime;
            if (animationTimer < 0)
            {
                currentAnimationIndex = 0;
                spriteRenderer.sprite = currentAnimation.Sprites[0];
            }
        }
    }
}
