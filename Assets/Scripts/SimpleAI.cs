using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleAI : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float aggroRadius = 1;
    public float moveSpeed = 1;
    public float attackSpeed = 1;
    public float damage;

    private State state;
    private float wanderTime;
    private float wanderDistance;
    private GameObject player;
    private CharacterStats stats;
    private float lastFireTime;

    private List<ListSkill> skillList = new List<ListSkill>();

    void Start()
    {
        player = FindObjectOfType<Player>().transform.parent.gameObject;
        stats = GetComponent<CharacterStats>();
        wanderTime = Time.time + Random.Range(2, 5);
        state = State.Idle;

        skillList.Add(new ListSkill(ScriptableObject.CreateInstance<ShootArrow>()));
        skillList.Add(new ListSkill(ScriptableObject.CreateInstance<ThrowBomb>()));
    }

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

        foreach (ListSkill skill in skillList)
        {
            skill.Cooldown -= Time.deltaTime;
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
        Vector3 playerLocation = player.transform.position;
        float playerDistance = (playerLocation - transform.position).magnitude;
        Vector3 playerDirection = (playerLocation - transform.position).normalized;

        transform.forward = playerDirection;
        if (skillList.Any(x => x.Skill.PreferredRange < playerDistance))
        {
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }

        foreach (ListSkill skill in skillList)
        {
            if (skill.Cooldown <= 0)
            {
                if ((playerLocation - transform.position).magnitude <= skill.Skill.PreferredRange)
                {
                    Vector3 position = transform.position + Vector3.up * 0.5f;
                    Vector3 targetPosition = player.transform.position + Vector3.up * 0.5f;
                    skill.Skill.UseSkill(stats, position, targetPosition);
                    skill.Cooldown = skill.Skill.Cooldown;
                }
            }
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