using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Main : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] float move_Speed;

    

    void FixedUpdate()
    {
        Getcontroller(characterController);
    }

    public void Getcontroller(CharacterController characterController)
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        characterController.Move(move * Time.deltaTime);
    }
}
