using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker
{
    List<WeaponSO> weapons = new();
    int currentWeaponIndex = 0;
    WeaponSO getCurrentWeapon { get { return weapons[currentWeaponIndex]; }}
    public void Attack()
    {
        weapons.Add(WeaponManager.Singleton.GetWeapon("bigweapon"));
        //attack -> on weapon override function
        // -> play animator
        var pc = PlayerController.Singleton;
        var weapon = getCurrentWeapon;
        weapon.OnUse();
        
    }
}
