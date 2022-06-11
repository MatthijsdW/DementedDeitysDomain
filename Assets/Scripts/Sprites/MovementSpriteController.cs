using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementSpriteController : SpriteController
{
    protected override void Update()
    {
        base.Update();
        
        if (animationChanged)
        {
            currentAnimationIndex = 0;
            animationTimer = currentAnimation.AnimationSpeed;
            spriteRenderer.sprite = currentAnimation.Sprites[currentAnimationIndex];
            animationChanged = false;
        }
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;

        if (currentAnimation != null && currentAnimation.Sprites.Any())
        {
            if (characterStats.CurrentMovementSpeed == 0)
            {
                animationTimer = currentAnimation.AnimationSpeed;
                currentAnimationIndex = 0;
                spriteRenderer.sprite = currentAnimation.Sprites[currentAnimationIndex];
            }
            animationTimer -= Time.deltaTime * characterStats.CurrentMovementSpeed;
            if (animationTimer < 0)
            {
                animationTimer = currentAnimation.AnimationSpeed;
                currentAnimationIndex = currentAnimationIndex + 1 >= currentAnimation.Sprites.Count ? 0 : currentAnimationIndex + 1;
                spriteRenderer.sprite = currentAnimation.Sprites[currentAnimationIndex];
            }
        }
    }
}
