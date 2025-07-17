using UnityEngine;
using UnityEngine.InputSystem;
public class InputManagerForSHGV01 : Singleton<InputManagerForSHGV01>
{

    [SerializeField] bool IsLeftClickPressed;
    [SerializeField] bool IsRightClickPressed;
    [SerializeField] Vector2 MouseScreenPositon;
    [SerializeField] Vector3 MouseWorldPosition;
    Plane HorizontalPlane = new Plane(Vector3.down, 0);
    //Plane verticalPlane;
    //Vector3 verticalPlaneDistFromCamera;

    [SerializeField] Vector2 RotationDirInput = Vector2.zero;
    [SerializeField] Vector2 MovementInput = Vector2.zero;
    [SerializeField] Vector2 ScrollWheelInput = Vector2.zero;


    InputMap _Input = null;

    private void OnEnable()
    {
        _Input = new InputMap();

        _Input.CameraMovement.Enable();

        _Input.CameraMovement.Movement.performed += SetMovementInput;
        _Input.CameraMovement.Movement.canceled += SetMovementInput;

        _Input.CameraMovement.Rotation.performed += SetRotationDirInput;
        _Input.CameraMovement.Rotation.canceled += SetRotationDirInput;

        _Input.CameraMovement.ScrollWheel.performed += SetScrollWheelInput;
        _Input.CameraMovement.ScrollWheel.canceled += SetScrollWheelInput;

        HorizontalPlane = new Plane(Vector3.down, 0);

        /* Possible vertical plane detection for building on walls.
        // This will take a little bit of redesign to implement wouldnt be difficult but at the current date it is not a priority 
        // If we dont end up using it take it out.
        verticalPlaneDistFromCamera = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z - GameManager.Instance.GetZMinMaxBoundsForLevel().y);
        verticalPlane = new Plane(Vector3.forward, verticalPlaneDistFromCamera);
        */
    }
    void Update()
    {
        CheckMouseInput();
    }
    private void OnDisable()
    {

        _Input.CameraMovement.Movement.performed -= SetMovementInput;
        _Input.CameraMovement.Movement.canceled -= SetMovementInput;

        _Input.CameraMovement.Rotation.performed -= SetRotationDirInput;
        _Input.CameraMovement.Rotation.canceled -= SetRotationDirInput;

        _Input.CameraMovement.ScrollWheel.performed -= SetScrollWheelInput;
        _Input.CameraMovement.ScrollWheel.canceled -= SetScrollWheelInput;
    }
    // Update is called once per frame
    public bool GetisLeftClickPressed() { return IsLeftClickPressed; }
    public bool GetisRightClickPressed() { return IsRightClickPressed; }
    public Vector3 GetMouseWorldPosition() { return MouseWorldPosition; }
    public Vector2 GetMovementInput() { return MovementInput; }
    public Vector2 GetRotationDirInput() { return RotationDirInput; }
    public Vector2 GetScrollWheelInput() { return ScrollWheelInput; }
    void CheckMouseInput()
    {
        if (Mouse.current != null)
        {
            IsLeftClickPressed = Mouse.current.leftButton.isPressed;
            IsRightClickPressed = Mouse.current.rightButton.isPressed;
            MouseScreenPositon = Mouse.current.position.ReadValue();
            MouseWorldPositionCalculations();

        }
    }
    void MouseWorldPositionCalculations()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(MouseScreenPositon);

        if (HorizontalPlane.Raycast(mouseRay, out float distance))
        {
            MouseWorldPosition = mouseRay.GetPoint(distance);
        }
    }
    void SetMovementInput(InputAction.CallbackContext ctx)
    {
        MovementInput = ctx.ReadValue<Vector2>();
    }
    void SetRotationDirInput(InputAction.CallbackContext ctx)
    {
        RotationDirInput = ctx.ReadValue<Vector2>();
    }
    void SetScrollWheelInput(InputAction.CallbackContext ctx)
    {
        ScrollWheelInput = ctx.ReadValue<Vector2>();
    }

}
