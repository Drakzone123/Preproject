using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerType
{
    Small, Big
}
public class PlayerController : MonoBehaviour
{
    public PlayerSO playerSmallSO;
    public PlayerSO playerBigSO;
    public float jumpHeight;
    public float gravity ;
    public float moveSpeed ; 
    CharacterController controller;
    public Vector3 velocity;
    PlayerType currentPlayerType;
    Transform currentCharacter;

    void Awake()
    {
        //initialize
        controller = GetComponent<CharacterController>();

        //init player state
        SwapCharacter(PlayerType.Small);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var isOnGround = controller.isGrounded;
            if(isOnGround)
            {
                Jump(jumpHeight);
            }
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            //switch 0, 1
            currentPlayerType = currentPlayerType == PlayerType.Small ? PlayerType.Big: PlayerType.Small;
            SwapCharacter(currentPlayerType);
        }

        UpdateMovement();
    }
    void UpdateMovement()
    {
        velocity.y += gravity * Time.deltaTime;
        var isOnGround = controller.isGrounded;
        if(isOnGround && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        //added layer velocity
        var movement = Vector3.zero;
        var moveLeftRight =  Input.GetAxis("Horizontal");
        movement.x = moveLeftRight * moveSpeed;
        controller.Move(movement * Time.deltaTime);
        //on move left
        if(movement.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90f, 0);
        }
        //on move right
        else
        {
            transform.rotation = Quaternion.Euler(0, 90f, 0);
        }

        
        //default velocity
        controller.Move(velocity * Time.deltaTime);
    }
    void Jump(float jumpHeight)
    {
        var jumpVelo = Mathf.Sqrt(-2.0f * gravity * jumpHeight);
        Debug.Log(jumpVelo);
        velocity.y = jumpVelo;
    }
    void setPlayerStatus(PlayerSO status) {
        jumpHeight = status.jumpHeight;
        gravity = status.gravity;
        moveSpeed = status.moveSpeed;
    }
    void SwapCharacter(PlayerType type)
    {
        var index = (int)type;
        currentCharacter = this.transform.GetChild(index);
        Jump(0.5f);
    
        //preset character state
        //set config player status
        if(type == PlayerType.Small) {
            setPlayerStatus(playerSmallSO);
        } else {
            setPlayerStatus(playerBigSO);
        }
        
        if(type == PlayerType.Small)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            controller.center = new Vector3(0, 1, 0);
            controller.height = 2f;
            controller.radius = 0.55f;
        //big
        } else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            controller.center = new Vector3(0, 2.29f, 0);
            controller.height = 4.5f;
            controller.radius = 1.2f;
        }
    }
}

