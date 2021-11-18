using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float shotSpeed;
    public float range;
    public bool enemy;

    private float distanceTraveled;

    private void Start()
    {
        distanceTraveled = 0;
    }

    private void Update()
    {
        transform.Translate(transform.forward * shotSpeed * Time.deltaTime, Space.World);
        distanceTraveled += shotSpeed * Time.deltaTime;

        if (distanceTraveled > range)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemy && other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (!enemy && other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
