using UnityEngine;
using UnityEngine.InputSystem;
public class InputManagerForSHGV01 : MonoBehaviour
{

    public bool isLeftClickPressed;
    public Vector2 mouseScreenPositon;
    public Vector3 mouseWorldPosition;
    Plane horizontalPlane = new Plane(Vector3.down, 0);
    Plane verticalPlane;

    public Vector2 Movement = Vector2.zero;
    public Vector2 RotationDir = Vector2.zero;
    public Vector2 Zoom = Vector2.zero;


     InputMap _Input = null;

    private void OnEnable()
    {
        _Input = new InputMap();

        _Input.CameraMovement.Enable();

        _Input.CameraMovement.Movement.performed += SetMovement;
        _Input.CameraMovement.Movement.canceled += SetMovement;

        _Input.CameraMovement.Rotation.performed += SetRotationDir;
        _Input.CameraMovement.Rotation.canceled += SetRotationDir;

        _Input.CameraMovement.ScrollWheel.performed += SetScrollWheel;
        _Input.CameraMovement.ScrollWheel.canceled += SetScrollWheel;
    }
    void Update()
    {
        CheckMouseInput();
    }
    private void OnDisable()
    {

        _Input.CameraMovement.Movement.performed -= SetMovement;
        _Input.CameraMovement.Movement.canceled -= SetMovement;

        _Input.CameraMovement.Rotation.performed -= SetRotationDir;
        _Input.CameraMovement.Rotation.canceled -= SetRotationDir;

        _Input.CameraMovement.ScrollWheel.performed -= SetScrollWheel;
        _Input.CameraMovement.ScrollWheel.canceled -= SetScrollWheel;
    }
    // Update is called once per frame
   
    void SetMovement(InputAction.CallbackContext ctx)
    {
        Movement = ctx.ReadValue<Vector2>();
    }
    void SetRotationDir(InputAction.CallbackContext ctx)
    {
        RotationDir = ctx.ReadValue<Vector2>();
    }
    void SetScrollWheel(InputAction.CallbackContext ctx)
    {
        Zoom = ctx.ReadValue<Vector2>();
    }
    void CheckMouseInput()
    {
        if (Mouse.current != null)
        {
            isLeftClickPressed = Mouse.current.leftButton.isPressed;
            mouseScreenPositon = Mouse.current.position.ReadValue();

        }
    }
}
