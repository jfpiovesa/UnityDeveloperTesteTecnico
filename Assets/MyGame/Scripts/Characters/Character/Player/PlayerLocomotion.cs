using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotion : BaseCharacterLocomotion
{
    [Header("Move")]
    [SerializeField] private float _moveSppeed = 5;
    [SerializeField] private float _smothRotation = 0.1f;
    [SerializeField] private Vector3 _direction = Vector3.zero;

    [Header("Ground"), Space(5)]
    [SerializeField] private float _gravity = 15;

    [Header("Componemts"), Space(5)]
    [SerializeField] private CharacterController _characterController;


    [Header("Attack"), Space(5)]
    [SerializeField] private bool _attack;


    [Header("Animation"), Space(5)]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _anim_waliking;
    [SerializeField] private string _anim_attack;
    [SerializeField] private string _anim_ground;


    public override void Locomotion(Vector2 movimentValue)
    {
        Vector2 movimentLocal = movimentValue;
        Moviemnt(movimentLocal);
        movimentLocal = Vector2.zero;
    }
    void Moviemnt(Vector2 movimentValue)
    {
        _direction = new Vector3(movimentValue.x, 0f, movimentValue.y).normalized;

        if (_direction.magnitude >= 0.1f)
        {
            float TargetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            float alngle = TargetAngle * _smothRotation;
            this.transform.rotation = Quaternion.Euler(0, alngle, 0);
        }

        _direction.y = -_gravity * Time.deltaTime;
        _characterController.Move(_direction * _moveSppeed * Time.deltaTime);

        _animator.SetBool(_anim_ground, _characterController.isGrounded);

        if (_characterController.isGrounded)
        {
            _animator.SetFloat(_anim_waliking, _characterController.velocity.magnitude);
        }
    }


    public override void Atack()
    {
        _animator.Play(_anim_attack);
    }
}
