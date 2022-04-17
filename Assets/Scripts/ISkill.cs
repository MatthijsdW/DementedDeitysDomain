using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    public float Cooldown { get; set; }
    public float PreferredRange { get; set; }

    public void UseSkill(CharacterStats userStats, Vector3 userLocation, Vector3 targetLocation);
}
