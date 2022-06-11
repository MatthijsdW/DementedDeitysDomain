using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float hitPoints = 100;
    public float power = 10;

    [System.NonSerialized]
    public float CurrentMovementSpeed = 0;

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
