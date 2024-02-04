using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    COMP398LAB _inputs;

    Vector2 _move;

    [Header("Character Controller")]
    [SerializeField] CharacterController _controller;

    [Header("Movements")]
    [SerializeField] float _speed;
    [SerializeField] float _gravity = -30.0f;
    [SerializeField] float _jumpHeight = 3.0f;
    [SerializeField] Vector3 _velocity;

    [Header("Ground Detection")]
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundRadius = 0.5f;
    [SerializeField] LayerMask _groundMask;
    [SerializeField] bool _isGrounded;

    [Header("Respawn")]
    [SerializeField] Transform _respawnPosition;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _inputs = new COMP398LAB();
        _inputs.Enable();
        _inputs.Player.Move.performed   += context => _move = context.ReadValue<Vector2>();
        _inputs.Player.Move.canceled += context => _move = Vector2.zero;
        _inputs.Player.Jump.performed += context => Jump();
    }

    private void SendMessage(InputAction.CallbackContext context)
    {
        Debug.Log($"Move Performed(x = {context.ReadValue<Vector2>().x}, y= {context.ReadValue<Vector2>().y} ");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundRadius);
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundRadius, _groundMask);
        if(_isGrounded && _velocity.y < 0.0f)
        {
            _velocity.y = -2.0f;
        }
        Vector3 movement = new Vector3(_move.x, 0, _move.y) * _speed * Time.deltaTime;
        _controller.Move(movement);
        _velocity.y += _gravity * Time.fixedDeltaTime;
        _controller.Move(_velocity * Time.fixedDeltaTime);
    }

    void Jump()
    {
        if (_isGrounded)
        {
            Debug.Log("Aie");
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2.0f * _gravity);
        }
    }

    private void OnEnable() => _inputs.Enable();

    private void OnDisable() => _inputs.Disable();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            _controller.enabled = false;
            transform.position = _respawnPosition.transform.position;
            _controller.enabled = true;
        }
    }
}
