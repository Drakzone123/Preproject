using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Singleton;
    Dictionary<string, WeaponSO> weapons = new(); 
    
    private void Awake()
    {
        Singleton = this;

        var items = Resources.LoadAll<WeaponSO>("Weapons");
        foreach(var item in items) {
            weapons.Add(item.weaponId, item);
            Debug.Log($"on loaded weapon {item.weaponId}");
        }
    }
    public WeaponSO GetWeapon(string id) {
        return weapons[id];
    }
}
