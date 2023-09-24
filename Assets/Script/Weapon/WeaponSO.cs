using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : ScriptableObject
{
    public string weaponId;
    public string weaponType;
    public int weaponDmg = 1;
    public float weaponSpeed = 1f;
    public float damageRadius = 1f;

    public virtual void OnUse() { }
}
