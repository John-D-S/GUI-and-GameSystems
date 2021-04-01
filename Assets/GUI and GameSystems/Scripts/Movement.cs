﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("RPG/Player/Movement")]
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Speed Vars")]
    public float moveSpeed;
    public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;
    private float _gravity = 20.0f;
    private Vector3 _moveDir;
    public CharacterController _charC;
    private Animator characterAnimator;

    private void Start()
    {
        _charC = GetComponent<CharacterController>();
        characterAnimator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 controlVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (controlVector.magnitude >= 0.05f)
        {
            characterAnimator.SetBool("moving", true);
        }
        else
        {
            characterAnimator.SetBool("moving", false);
        }

        if (_charC.isGrounded)
        {
            if (Input.GetButton("Sprint"))
            {
                moveSpeed = runSpeed;
            }
            else if (Input.GetButton("Crouch"))
            {
                moveSpeed = crouchSpeed;
            }
            else
            {
                moveSpeed = walkSpeed;
            }
            _moveDir = transform.TransformDirection(new Vector3(controlVector.x, 0, controlVector.y).normalized * moveSpeed); 
            if (Input.GetButton("Jump"))
            {
                _moveDir.y = jumpSpeed;
            }
        }
        _moveDir.y -= _gravity * Time.deltaTime;
        _charC.Move(_moveDir * Time.deltaTime);
    }
}