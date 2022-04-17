using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb : ScriptableObject, ISkill
{
    public float Cooldown { get; set; } = 5;
    public float PreferredRange { get; set; } = 5;

    private GameObject bombPrefab;

    private void OnEnable()
    {
        bombPrefab = Resources.Load("Skills/Bomb") as GameObject;
    }

    public void UseSkill(CharacterStats userStats, Vector3 userLocation, Vector3 targetLocation)
    {
        Vector3 position = userLocation + Vector3.up * 0.5f;
        Vector3 direction = (targetLocation - userLocation).normalized;

        GameObject projectile = Instantiate(bombPrefab);
        projectile.transform.position = position + direction * 0.4f;
        Bomb bombComponent = projectile.GetComponent<Bomb>();
        bombComponent.enemy = true;
        bombComponent.duration = 1;
        bombComponent.target = targetLocation;
    }
}
