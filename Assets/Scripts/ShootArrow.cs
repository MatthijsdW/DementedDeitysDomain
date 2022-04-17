using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : ScriptableObject, ISkill
{
    public float Cooldown { get; set; } = 1;
    public float PreferredRange { get; set; } = 5;

    private GameObject projectilePrefab;

    private void OnEnable()
    {
        projectilePrefab = Resources.Load("Skills/Projectile") as GameObject;
    }

    public void UseSkill(CharacterStats userStats, Vector3 userLocation, Vector3 targetLocation)
    {
        Vector3 position = userLocation + Vector3.up * 0.5f;
        Vector3 direction = (targetLocation - userLocation).normalized;

        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.position = position + direction * 0.4f;
        projectile.transform.forward = direction;
        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.enemy = true;
        projectileComponent.damage = userStats.power;
        projectileComponent.range = 7.5f;
    }
}
