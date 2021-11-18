using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float aggroRadius = 1;
    public float moveSpeed = 1;
    public float fireRate;

    private State state;
    private float wanderTime;
    private float wanderDistance;
    private GameObject player;
    private float lastFireTime;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().transform.parent.gameObject;
        wanderTime = Time.time + Random.Range(2, 5);
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Wandering:
                Wander();
                break;
            case State.Aggroed:
                AttackPlayer();
                break;
        }
    }

    private void Idle()
    {
        if (wanderTime < Time.time)
        {
            float wanderAngle = Random.Range(0, 360);
            transform.eulerAngles = new Vector3(0, wanderAngle, 0);
            wanderDistance = Random.Range(0.5f, 1.5f);
            state = State.Wandering;
        }

        DetectPlayer();
    }

    private void Wander()
    {
        if (wanderDistance < 0)
        {
            state = State.Idle;
            wanderTime = Time.time + Random.Range(2, 5);
            return;
        }

        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        wanderDistance -= moveSpeed * Time.deltaTime;

        DetectPlayer();
    }

    private void AttackPlayer()
    {
        transform.forward = (player.transform.position - transform.position).normalized;
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);


        if (fireRate < Time.time - lastFireTime)
        {
            Vector3 position = transform.position + Vector3.up * 0.5f;
            Vector3 direction = (player.transform.position - transform.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.position = position + direction * 0.4f;
            projectile.transform.forward = direction;
            projectile.GetComponent<Projectile>().enemy = true;

            lastFireTime = Time.time;
        }
    }

    private void DetectPlayer()
    {
        if ((transform.position - player.transform.position).magnitude < aggroRadius)
        {
            state = State.Aggroed;
        }
    }

    private enum State
    {
        Idle,
        Wandering,
        Aggroed,
    }
}