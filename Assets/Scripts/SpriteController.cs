using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite up;
    public Sprite left;
    public Sprite down;
    public Sprite right;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float cameraAngle = Camera.main.transform.rotation.eulerAngles.y;
        float spriteAngle = transform.parent.rotation.eulerAngles.y;

        int angleDifference = Mathf.RoundToInt(cameraAngle - spriteAngle);
        if (angleDifference < 0)
            angleDifference += 360;

        if (angleDifference <= 45 || angleDifference >= 315)
            spriteRenderer.sprite = up;
        else if (angleDifference <= 135)
            spriteRenderer.sprite = left;
        else if (angleDifference < 225)
            spriteRenderer.sprite = down;
        else
            spriteRenderer.sprite = right;

    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
