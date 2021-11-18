using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSprite : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
        transform.Rotate(transform.forward, -transform.parent.eulerAngles.y + Camera.main.transform.eulerAngles.y, Space.World);
    }
}
