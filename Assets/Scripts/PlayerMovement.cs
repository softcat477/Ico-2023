using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    PlayerInputActions playerInputActions;

    InputAction move;

    public float speed = 10.0f;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
    }
    private void OnEnable() {
        move = playerInputActions.Player.Move;
        move.Enable();
    }
    private void OnDisable() {
        move.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 move_vec = move.ReadValue<Vector2>();
        animator.SetFloat("X Direction", -move_vec.x);
        rb.MovePosition(rb.position + move_vec * speed * Time.fixedDeltaTime);
    }
}
