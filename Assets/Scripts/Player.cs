using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player instance;
    public CharacterStats playerStats;

    public GameObject projectilePrefab;
    public Transform playerHolder;
    public float movementSpeed;
    public float fireRate;

    [System.NonSerialized]
    public float terrainSpeedModifier;

    private Vector3 moveDirection;
    private bool firing;
    private float lastFireTime;
    private ActionSpriteController actionSpriteController;


    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 directionValue = context.ReadValue<Vector2>();
        moveDirection = new Vector3(directionValue.x, 0, directionValue.y);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        firing = context.ReadValue<float>() > 0;
    }

    private void Start()
    {
        instance = this;
        playerStats = GetComponent<CharacterStats>();
        actionSpriteController = GetComponentInChildren<ActionSpriteController>();
    }

    private void Update()
    {
        switch (BiomeMap.instance.GetBiomeAt(playerHolder.position.x, playerHolder.position.z))
        {
            case Biome.Water:
                terrainSpeedModifier = 0.7f;
                break;
            case Biome.DeepWater:
                terrainSpeedModifier = 0.4f;
                break;
            default:
                terrainSpeedModifier = 1f;
                break;
        }

        if (firing && fireRate < Time.time - lastFireTime)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                Vector3 position = transform.position + Vector3.up * 0.5f;
                Vector3 direction = (hit.point - Vector3.up * 0.5f - transform.position).normalized;

                GameObject projectile = Instantiate(projectilePrefab);
                projectile.GetComponent<Projectile>().damage = playerStats.power;
                projectile.transform.position = position + direction * 0.4f;
                projectile.transform.forward = direction;

                if (actionSpriteController != null)
                {
                    actionSpriteController.TriggerAnimation(fireRate / 2);
                }
            }

            lastFireTime = Time.time;
        }

        playerStats.CurrentMovementSpeed = moveDirection.magnitude * movementSpeed * terrainSpeedModifier;
        if (playerStats.CurrentMovementSpeed > 0)
        {
            transform.forward = moveDirection;
            transform.Rotate(Vector3.up, Camera.main.transform.eulerAngles.y);

            playerHolder.Translate(transform.forward * playerStats.CurrentMovementSpeed * Time.deltaTime);
        }
    }
}
