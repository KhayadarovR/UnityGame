using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWoman : Unit
{
    [SerializeField] private Transform _ourTransform;
    [SerializeField] private LayerMask _LayerForEye;
    [SerializeField] private LayerMask _LayerForWall;
    [SerializeField] private int _eyeDistance = 6;
    [SerializeField] private int _currentState;
    [SerializeField] private Animator _animator;

    private bool _onWall = false;
    private readonly float _eyeHeight = 1.2f;
    private Vector2 _rayOrigin;
    
    

    private void Start()
    {
        _currentState = (int)WomanState.isSecurity;
        _ourTransform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        base._currentDirection = 1;
    }

    private void FixedUpdate()
    {
        SensorCharacter();
        
    }
    private void Update()
    {

        _animator.SetInteger("WomanState", _currentState);


        switch (_currentState)
        {
            //case (int)WomanState.isIdle:
            //    break;
            case (int)WomanState.isSecurity:
                base.Walk(base._currentDirection);
                SensorWall();
                break;
            case (int)WomanState.isPursuit:        
                base.Run(base._currentDirection);   
                break;
        }  

    }
                           
    private void OnCollisionExit2D(Collision2D collision)
    {
        // ����������� ������� � ������
        return;
    }

    enum WomanState : int
    { 
        isIdle = 0,//�������� �������
        isSecurity = 1,
        isPursuit = 2,
        

        //��� ��������� woman
    }

    private void SensorWall()
    {
        //������� ��� �������� �� ����� (�������)
        RaycastHit2D _wallInfo = Physics2D.Raycast(_rayOrigin, base._currentDirection * _ourTransform.right, 1, _LayerForWall);
        Debug.DrawRay(_rayOrigin, base._currentDirection  * 1 *_ourTransform.right, Color.red);
        if (_wallInfo.collider == null) //���� ������� �� ��������
        {
            _onWall = false;
        }
        else
        {
            _onWall = true;
        }

        if (_onWall == true)
        {
            base._currentDirection = -base._currentDirection; //���� ����� ����������� �������� � -1 �� -(-1)
            ChangeDirection(base._currentDirection);          //������ ���������� �� �������
        }
    }

    private void SensorCharacter()
    {
        _rayOrigin = new Vector2(_ourTransform.position.x, _ourTransform.position.y + _eyeHeight);

        //������� ��� ����������� ����������� (�������)
        RaycastHit2D _eyeInfo = Physics2D.Raycast(_rayOrigin, base._currentDirection * _ourTransform.right, _eyeDistance,_LayerForEye);
        Debug.DrawRay(_rayOrigin, base._currentDirection * _eyeDistance *_ourTransform.right, Color.green);
        if (_eyeInfo.collider.TryGetComponent(out IDiscoverable discoverable))//���� ������� ����� � �����������
        {
            discoverable.Detected(true);
            _currentState = (int)WomanState.isPursuit;
        }
        else// ���� �����
        {
            _currentState = (int)WomanState.isSecurity;
        }
    }
}
