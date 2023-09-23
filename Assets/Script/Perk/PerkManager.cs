using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PerkManager : MonoBehaviour
{
    public static PerkManager Singleton {get; private set;}
    Dictionary<string, PerkSO> perks = new(); 
    Dictionary<string, PerkSO> unlockPerks = new(); 
    
    private void Awake() {
        //setup
        Singleton = this;

        var allPerks = Resources.LoadAll<PerkSO>("Perk");
        Debug.Log($"on load file perks {allPerks.Length}");

        foreach(var perk in allPerks) {
            perks.Add(perk.id, perk);
            Debug.Log($"on loaded perk {perk.title}");
            //add perk to database
        }
    }
    public PerkSO GetPerk(string id) {
        return perks[id];
    }
    public bool IsUnlockPerk(string id) {
        return unlockPerks.ContainsKey(id);
    }
    public void UnlockPerk(string id) {
        unlockPerks.Add(id, perks[id]);
    }
}
