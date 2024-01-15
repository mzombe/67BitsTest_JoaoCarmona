using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float gravity = -9.81f;

    [Range (0f,1f)]
    [SerializeField] private float _turnSpeed;

    private CharacterController _char;
    private Animator _animator;
    Vector3 _dir;

    public Vector3 Dir { get { return _dir; } }
    public bool IsMoving {  get { return _char.velocity.x > 0 || _char.velocity.z > 0;} }
    private void Awake()
    {
        _char = this.gameObject.GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        _dir = Vector3.forward * _joystick.Vertical + Vector3.right * _joystick.Horizontal;

        float localGravity = _char.isGrounded ? 0 : gravity;

        if(_dir != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir), _turnSpeed);
        _char.Move(new Vector3(_dir.x, localGravity * Time.deltaTime, _dir.z) * _moveSpeed * Time.deltaTime);  
        
        _animator.SetBool("isMoving", _dir != Vector3.zero);

        if(_animator.GetBool("isMoving")) _animator.speed = _dir.magnitude;
            else _animator.speed = 1;
    }
}
