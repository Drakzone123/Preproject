using UnityEngine;

[CreateAssetMenu(fileName = "New Heart Perk", menuName = "Perk/Heart Perk SO", order = 1)]
public class HeartPerk : PerkSO {
    public override void OnUse(PlayerController player) {
        player.lifeMax = 4;
        player.life ++;
        Debug.Log($"on use perk {id} player max life {player.lifeMax}");
       
    }
}