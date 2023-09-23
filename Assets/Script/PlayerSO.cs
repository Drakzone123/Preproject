using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Player Status", menuName = "Player/Player Status New", order = 1)]
public class PlayerSO : ScriptableObject
{
    public float jumpHeight = 1.5f;
    public float gravity = -10f;
    public float moveSpeed = 4f;
}