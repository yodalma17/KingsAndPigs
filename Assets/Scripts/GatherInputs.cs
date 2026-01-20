using UnityEngine;
using UnityEngine.InputSystem;


public class GatherInputs : MonoBehaviour
{
    private Controls controls;

    [SerializeField] private float _valueX;
    public float ValueX { get => _valueX; }

    [SerializeField] private bool _isJumping;
    public bool IsJumping { get => _isJumping; set => _isJumping = value; }

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Player.Move.performed += ctx => startMove(ctx);
        controls.Player.Move.canceled += ctx => stopMove(ctx);
        controls.Player.Jump.performed += ctx => startJump(ctx);
        controls.Player.Jump.canceled += ctx => stopJump(ctx);
        controls.Player.Enable();
    }

    private void startMove(InputAction.CallbackContext context)
    {
        _valueX = context.ReadValue<Vector2>().x;
    }

    private void stopMove(InputAction.CallbackContext context)
    {
        _valueX = 0;
    }

    private void startJump(InputAction.CallbackContext context)
    {
        _isJumping = true;
    }

    private void stopJump(InputAction.CallbackContext context)
    {
        _isJumping = false;
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= ctx => startMove(ctx);
        controls.Player.Move.canceled -= ctx => stopMove(ctx);
        controls.Player.Jump.performed -= ctx => startJump(ctx);
        controls.Player.Jump.canceled -= ctx => stopJump(ctx);
        controls.Player.Disable();
    }
}
