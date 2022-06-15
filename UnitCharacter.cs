using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacter : Unit
{


    [SerializeField] private CharacterInput _controlInput;
    [SerializeField] private Animator _animator;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _controlInput = GetComponent<CharacterInput>();
    }
    // �������� ����� ���������� ������ ������
    private void Update()
    {
        float directionX = _controlInput.GetDirectionsX();


        base.Walk(directionX);

        // �������� ��� �������
        if (base._onGround == true)
            _animator.SetBool("isJump", false);
        else
            _animator.SetBool("isJump", true);

        // �������� ��� ������
        if (base._onGround == true)
        {
            _animator.SetFloat("moveX", System.Math.Abs(directionX));
        }

        // �������� ��� ����
        if (_controlInput.IsRun())
        {
            base.Run(directionX);
            if (base._onGround == true)
                _animator.SetBool("isRun", true);
        }
        else
        {
            _animator.SetBool("isRun", false);
        }

        // �������� ��� ������
        if (_controlInput.IsJump() && _controlInput.IsRun())
        {
            base.Jump();
            _animator.SetBool("isJump", true);
        }
     


        if (_controlInput.IsRespawn())
            base.RespawnToPoint(new Vector2(0,2));


    }


}
