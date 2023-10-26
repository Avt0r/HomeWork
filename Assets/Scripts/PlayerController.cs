using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : Initializable
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Vector2 _gazeDirection;

    [SerializeField] private Transform _camera;
    [SerializeField] private WeaponController _weapon;

    [SerializeField, Min(0)] private float _sensitivityX;
    [SerializeField, Min(0)] private float _sensitivityY;
    [SerializeField] private float _maxGaze;
    [SerializeField] private float _minGaze;

    [SerializeField, Min(0)] private float _jumpPower;

    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(1)] private float _speedBoost;

    private float _speedCurrent = 0f;
    private bool _canJump = true;

    private Bootstrap _bootstrap;

    private void OnValidate()
    {
        _rb = _rb != null ? _rb : GetComponent<Rigidbody>();
        _maxGaze = _maxGaze < 0 ? 0 : _maxGaze;
        _minGaze = _minGaze >= _maxGaze ? _maxGaze : _minGaze;
    }

    public override void Init()
    {
        if (_inited) return;
        _inited = true;

        _bootstrap = Bootstrap.Instance;

        _speedCurrent = _speed;

        _gazeDirection = Vector2.zero;

        Subscribe();
    }

    public override void Finish()
    {
        Unsubscribe();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Rotate();   
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        _bootstrap.InputHandler.RunAction.performed += RunPerformed;
        _bootstrap.InputHandler.RunAction.canceled += RunCanceled;
        _bootstrap.InputHandler.JumpAction.performed += Jump;
        _bootstrap.InputHandler.RightClickAction.performed += _weapon.ShootStart;
        _bootstrap.InputHandler.RightClickAction.canceled += _weapon.ShootStop;
    }

    private void Unsubscribe()
    {
        _bootstrap.InputHandler.RunAction.performed -= RunPerformed;
        _bootstrap.InputHandler.RunAction.canceled -= RunCanceled;
        _bootstrap.InputHandler.JumpAction.performed -= Jump;
        _bootstrap.InputHandler.RightClickAction.performed -= _weapon.ShootStart;
        _bootstrap.InputHandler.RightClickAction.canceled -= _weapon.ShootStop;
    }

    private void Move()
    {
        Vector2 inputVector = _bootstrap.InputHandler.GetPlayerMoveInput();

        Vector3 moveVector = (transform.forward * inputVector.y + transform.right * inputVector.x).normalized * _speedCurrent;

        _rb.velocity = new(moveVector.x, _rb.velocity.y, moveVector.z);
    }

    private void Rotate()
    {
        Vector2 input = _bootstrap.InputHandler.GetMouseMoveInput();

        _gazeDirection.x += input.x * _sensitivityX;
        _gazeDirection.y -= input.y * _sensitivityY;

        _gazeDirection.y = Mathf.Clamp(_gazeDirection.y, _minGaze, _maxGaze);

        _camera.localEulerAngles = new Vector3(_gazeDirection.y, 0, 0);
        transform.localEulerAngles = new Vector3(0,_gazeDirection.x, 0);
    }

    private void RunPerformed(InputAction.CallbackContext obj)
    {
        _speedCurrent = _speed * _speedBoost;
    }

    private void RunCanceled(InputAction.CallbackContext obj)
    {
        _speedCurrent = _speed;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (_canJump)
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode.VelocityChange);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            _canJump = true;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Coin>(out var coin))
        {
            _bootstrap.Wallet.AddCoin(coin);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            _canJump = false;
        }
    }

    public Transform Camera => _camera;
}
