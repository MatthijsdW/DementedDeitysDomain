using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float damage = 200;
    public float radius = 1;
    public float duration = 1;
    public float maxHeight = 2;
    public bool enemy = false;

    public Vector3 target;

    private float maxDuration;
    private GameObject AoEMarker;

    private void Start()
    {
        maxDuration = duration;
        if (enemy)
        {
            GameObject AoEMarkerPrefab = Resources.Load("AoEMarker") as GameObject;
            AoEMarker = Instantiate(AoEMarkerPrefab);
            AoEMarker.transform.localScale = new Vector3(radius, radius, radius);
            AoEMarker.transform.position = new Vector3(target.x, 0.01f, target.z);
        }
    }

    private void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetPosition = new Vector2(target.x, target.z);
        float distanceToTarget = (targetPosition - currentPosition).magnitude;

        Vector2 targetDirection = (targetPosition - currentPosition).normalized;
        transform.forward = new Vector3(targetDirection.x, 0, targetDirection.y);

        Vector2 movement = (Time.deltaTime / duration) * distanceToTarget * targetDirection;
        float height = -maxHeight * Mathf.Pow((duration - (maxDuration / 2)) / (maxDuration / 2), 2) + maxHeight + 0.5f;
        transform.position = new Vector3(transform.position.x + movement.x, height, transform.position.z + movement.y);

        duration -= Time.deltaTime;

        if (duration < Time.deltaTime)
        {
            if (enemy && Player.instance != null)
            {
                Vector2 playerPosition = new Vector2(Player.instance.transform.position.x, Player.instance.transform.position.z);
                if ((playerPosition - targetPosition).magnitude < radius)
                {
                    Player.instance.playerStats.TakeDamage(damage);
                }
            }
            Destroy(gameObject);
            if (enemy)
                Destroy(AoEMarker);
        }
    }
}
