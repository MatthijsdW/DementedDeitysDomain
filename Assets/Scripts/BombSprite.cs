using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSprite : MonoBehaviour
{
    public float rotation = 0;

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;

        float cameraAngle = Camera.main.transform.rotation.eulerAngles.y;
        float parentAngle = transform.parent.rotation.eulerAngles.y;

        int angleDifference = Mathf.RoundToInt(cameraAngle - parentAngle);
        if (angleDifference < 0)
            angleDifference += 360;

        if (angleDifference > 180)
        {
            rotation -= 180 * Time.deltaTime;
        }
        else
        {
            rotation += 180 * Time.deltaTime;
        }

        transform.Rotate(transform.forward, rotation, Space.World);
    }
}
