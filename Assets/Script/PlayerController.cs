using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Editor;
using UnityEngine;

public enum PlayerType
{
    Small, Big
}
public class PlayerController : Being
{
    public static PlayerController Singleton;
    [Header("Player Status")]
    public PlayerSO playerSmallSO;
    public PlayerSO playerBigSO;
    public float jumpHeight;
    public float gravity;
    public float gravityDown;
    public float moveSpeed;
    public Vector3 velocity;
    public bool isGrounded;

    CharacterController controller;
    PlayerType currentPlayerType;
    Transform currentCharacter;
    int jumpCount = 0;
    PlayerAttacker attacker = new PlayerAttacker();

    [Header("Animation")]
    Animator animator;
    AnimatorVarSmooth animMoveX;

    void Awake()
    {
        Singleton = this;
        //initialize
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        //Test Setup Characcter
        //init player state
        SwapCharacter(PlayerType.Small);
        PerkManager.Singleton.GetPerk("heart").OnUse(this);
        PerkManager.Singleton.UnlockPerk("dash");
        //set tranform
        transform.rotation = Quaternion.Euler(0, 90f, 0);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var isOnGround = controller.isGrounded;
            if (isOnGround)
            {
                Jump(jumpHeight);
                Debug.Log("on first jump");
            }
            //double jump
            else if (jumpCount == 1 && currentPlayerType == PlayerType.Small)
            {
                Jump(jumpHeight);
                Debug.Log("on double jump");
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //switch 0, 1
            currentPlayerType = currentPlayerType == PlayerType.Small ? PlayerType.Big : PlayerType.Small;
            SwapCharacter(currentPlayerType);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Dash(10, .2f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("on try attack");
            attacker.Attack();
        }


        UpdateMovement();

    }
    private void LateUpdate()
    {
        var moveX = controller.velocity.x;
        moveX = Mathf.Abs(moveX);
        animMoveX.Set(moveX, 0.05f);
        animMoveX.Update();
        Debug.Log("moe x val: " + animMoveX.value);
        Debug.Log("character velo: " + controller.velocity);
        animator.SetFloat("moveX", animMoveX.value);
    }
    void onTouchFirstGround()
    {
        jumpCount = 0;
        Debug.Log("on touch first on ground");
    }
    void UpdateMovement()
    {
        var newIsOnGround = controller.isGrounded;
        if (newIsOnGround != isGrounded && newIsOnGround) onTouchFirstGround();
        isGrounded = newIsOnGround;

        if (velocity.y < 0)
        {
            velocity.y += gravityDown * 3f * Time.deltaTime;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        //added layer velocity
        var movement = Vector3.zero;
        var moveLeftRight = Input.GetAxis("Horizontal");
        movement.x = moveLeftRight * moveSpeed;
        controller.Move(movement * Time.deltaTime);
        //on move left
        if (movement.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90f, 0);
        }
        //on move right
        if (movement.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90f, 0);
        }


        //default velocity
        controller.Move(velocity * Time.deltaTime);

    }
    void Jump(float jumpHeight)
    {
        var jumpVelo = Mathf.Sqrt(-2.0f * gravity * jumpHeight);
        velocity.y = jumpVelo;
        jumpCount++;
    }
    void setPlayerStatus(PlayerSO status)
    {
        jumpHeight = status.jumpHeight;
        gravity = status.gravity;
        gravityDown = status.gravityDown;
        moveSpeed = status.moveSpeed;
    }
    void SwapCharacter(PlayerType type)
    {
        var index = (int)type;
        currentCharacter = this.transform.GetChild(index);
        Jump(0.5f);

        //preset character state
        //set config player status
        if (type == PlayerType.Small)
        {
            setPlayerStatus(playerSmallSO);
        }
        else
        {
            setPlayerStatus(playerBigSO);
        }

        if (type == PlayerType.Small)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            controller.center = new Vector3(0, 0.7f, 0);
            controller.height = 1.3f;
            controller.radius = 0.22f;
            //big
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            controller.center = new Vector3(0, 1.125f, 0);
            controller.height = 2.25f;
            controller.radius = .38f;
        }

    }

    // move dir * move speed
    void Dash(float moveDir, float time)
    {
        var perkManager = PerkManager.Singleton;

        if (!perkManager.IsUnlockPerk("dash"))
        {
            return;
        }

        if (currentPlayerType == PlayerType.Big)
        {
            return;
        }

        StartCoroutine(DashAsync(moveDir, time));
    }
    IEnumerator DashAsync(float moveDir, float time)
    {
        var timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > time)
            {
                break;
            }
            Debug.Log($"on udpate movement with time {timer}");
            var movement = transform.TransformDirection(new Vector3(0, 0, moveDir));
            controller.Move(movement * Time.deltaTime);
            //wait every 1 frame
            yield return null;
        }
    }
}

