using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private int damage = 8;
    private Vector2 movement = Vector2.zero;
    private PlayerController controller;
    private Rigidbody2D rb;
    private Animator animator;

    private bool canMove = true;
    private bool isBlocking;
    private bool isAttacking = false;
    private int currentAttack = 0;
    private Coroutine attackCoroutine;
    [SerializeField] private AttackZone attackZone;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        controller = new PlayerController();
        controller.Enable();
    }

    void Start()
    {
        SetController();
    }

    private void SetController()
    {
        controller.Player.MoveRight.performed += ctx => Move(ctx, Vector2.right);
        controller.Player.MoveRight.canceled += ctx => Move(ctx, Vector2.left);

        controller.Player.MoveLeft.performed += ctx => Move(ctx, Vector2.left);
        controller.Player.MoveLeft.canceled += ctx => Move(ctx, Vector2.right);

        controller.Player.MoveUp.performed += ctx => Move(ctx, Vector2.up);
        controller.Player.MoveUp.canceled += ctx => Move(ctx, Vector2.down);

        controller.Player.MoveDown.performed += ctx => Move(ctx, Vector2.down);
        controller.Player.MoveDown.canceled += ctx => Move(ctx, Vector2.up);


        controller.Player.Attack.performed += ctx => Attack();
        controller.Player.Block.performed += ctx => StartBlock();
    }

    private void Move(InputAction.CallbackContext ctx, Vector2 direction)
    {
        movement += direction;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            ProcessMovement(movement * movementSpeed * Time.fixedDeltaTime);
        }
            
    }

    private void ProcessMovement(Vector2 direction)
    {
        rb.position = rb.position + direction;

        Vector3 scale = transform.localScale;
        if (movement.x < 0) scale.x = -1;
        else if (movement.x > 0) scale.x = 1;
        transform.localScale = scale;

        if (movement == Vector2.zero) animator.SetInteger("AnimState", 0);
        else
        {
            animator.SetInteger("AnimState", 1);
            animator.SetBool("IdleBlock", false);
        }
    }

    private void Attack()
    {
        if (!isAttacking || (isAttacking && currentAttack < 3)) {
            animator.SetInteger("AnimState", 0);
            canMove = false;

            isAttacking = true;
            currentAttack++;
            animator.SetInteger("Attack", currentAttack);

            foreach (Character character in attackZone.enemies)
                character.Damage(damage);
        }
    }

    public override void StopAttack()
    {
        isAttacking = false;
        currentAttack = 0;
        animator.SetInteger("Attack", currentAttack);
        canMove = true;
    }

    private void StartBlock()
    {
        canMove = false;

        animator.SetTrigger("Block");
        animator.SetBool("IdleBlock", true);
    }

    public override void Damage(int damage)
    {

    }
    protected override void Death()
    {

    }


}
