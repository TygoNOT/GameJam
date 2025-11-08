using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovment : MonoBehaviour
{
    [Header("Attribute")]
    [SerializeField] private CharacterStats stats;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] private Animator animator;

    private Vector2 moveInput;
    public Vector2 lastMoveDir = Vector2.down;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(horizontal, vertical).normalized;
       
        if (moveInput != Vector2.zero)
        {
            lastMoveDir = moveInput;
        }

        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * stats.playerMovementSpeed;
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);
        animator.SetFloat("LastMoveX", lastMoveDir.x);
        animator.SetFloat("LastMoveY", lastMoveDir.y);
        animator.SetBool("IsMoving", moveInput != Vector2.zero);
    }

}
