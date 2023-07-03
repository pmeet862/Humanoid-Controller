using UnityEngine;
using UnityEngine.InputSystem;


public class HumanoidLandInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public Vector2 LookInput { get; private set; } = Vector2.zero;
    public bool MoveIsPressed = false;
    public bool InvertMouseY { get; private set; } = true;
    public bool InvertScroll { get; private set; } = true;
    public bool WalkIsPressed { get; private set; } = false;
    public bool JumpIsPressed { get; private set; } = false;
    public bool CrouchIsPressed { get; private set; } = false;
    public bool TeleportIsPressed { get; private set; } = false;
    public bool OnOffWasPressedThisFrame { get; private set; } = false;
    public bool ModeWasPressedThisFrame { get; private set; } = false;
    public bool ChangeCameraWasPressedThisFrame { get; private set; } = false;
    public bool SwitchCharacterWasPressedThisFrame { get; private set; } = false;
    public bool ShootWasPressedThisFrame { get; private set; } = false;
    public float ZoomCameraInput { get; private set; } = 0.0f;
    public float ActivateInput { get; private set; } = 0.0f;
    InputActions _input;

    private void OnEnable()
    {
        _input = new InputActions();
        _input.HumanoidLand.Enable();

        _input.HumanoidLand.Move.performed += SetMove;
        _input.HumanoidLand.Move.canceled += SetMove;

        _input.HumanoidLand.Look.performed += SetLook;
        _input.HumanoidLand.Look.canceled += SetLook;

        _input.HumanoidLand.Walk.started += SetWalk;
        _input.HumanoidLand.Walk.canceled += SetWalk;

        _input.HumanoidLand.Jump.started += SetJump;
        _input.HumanoidLand.Jump.canceled += SetJump;

        _input.HumanoidLand.Crouch.started += SetCrouch;
        _input.HumanoidLand.Crouch.canceled += SetCrouch;

        _input.HumanoidLand.ZoomCamera.started += SetZoomCamera;
        _input.HumanoidLand.ZoomCamera.canceled += SetZoomCamera;

        _input.HumanoidLand.Activate.performed += SetActivate;
        _input.HumanoidLand.Activate.canceled += SetActivate;

        _input.HumanoidLand.Teleport.started += SetTeleport;
        _input.HumanoidLand.Teleport.canceled += SetTeleport;
    }

    private void OnDisable()
    {
        _input.HumanoidLand.Move.performed -= SetMove;
        _input.HumanoidLand.Move.canceled -= SetMove;

        _input.HumanoidLand.Look.performed -= SetLook;
        _input.HumanoidLand.Look.canceled -= SetLook;

        _input.HumanoidLand.Walk.started -= SetWalk;
        _input.HumanoidLand.Walk.canceled -= SetWalk;

        _input.HumanoidLand.Jump.started -= SetJump;
        _input.HumanoidLand.Jump.canceled -= SetJump;

        _input.HumanoidLand.Crouch.started -= SetCrouch;
        _input.HumanoidLand.Crouch.canceled -= SetCrouch;

        _input.HumanoidLand.ZoomCamera.started -= SetZoomCamera;
        _input.HumanoidLand.ZoomCamera.canceled -= SetZoomCamera;

        _input.HumanoidLand.Activate.performed -= SetActivate;
        _input.HumanoidLand.Activate.canceled -= SetActivate;

        _input.HumanoidLand.Teleport.started -= SetTeleport;
        _input.HumanoidLand.Teleport.canceled -= SetTeleport;

        _input.HumanoidLand.Disable();
    }

    private void Update()
    {
        OnOffWasPressedThisFrame = _input.HumanoidLand.OnOff.WasPressedThisFrame();
        ModeWasPressedThisFrame = _input.HumanoidLand.Mode.WasPressedThisFrame();
        ChangeCameraWasPressedThisFrame = _input.HumanoidLand.ChangeCamera.WasPressedThisFrame();
        SwitchCharacterWasPressedThisFrame = _input.HumanoidLand.SwitchCharacter.WasPressedThisFrame();
        ShootWasPressedThisFrame = _input.HumanoidLand.Shoot.WasPressedThisFrame();
    }

    private void SetMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
        MoveIsPressed = !(MoveInput == Vector2.zero);
    }

    private void SetLook(InputAction.CallbackContext ctx)
    {
        LookInput = ctx.ReadValue<Vector2>();
    }

    private void SetWalk(InputAction.CallbackContext ctx)
    {
        WalkIsPressed = ctx.started;
    }
    private void SetJump(InputAction.CallbackContext ctx)
    {
        JumpIsPressed = ctx.started;
    }

    private void SetCrouch(InputAction.CallbackContext ctx)
    {
        CrouchIsPressed = ctx.started;
    }

    private void SetZoomCamera(InputAction.CallbackContext ctx)
    {
        ZoomCameraInput = ctx.ReadValue<float>();
    }

    private void SetActivate(InputAction.CallbackContext ctx)
    {
        ActivateInput = ctx.ReadValue<float>();
    }

    private void SetTeleport(InputAction.CallbackContext ctx)
    {
        TeleportIsPressed = ctx.started;
    }
}
