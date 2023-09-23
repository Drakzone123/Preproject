using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSO : ScriptableObject
{
    public string id;
    //string 
    public string title;
    public string description;
    public virtual void OnUse(PlayerController player) { }
}