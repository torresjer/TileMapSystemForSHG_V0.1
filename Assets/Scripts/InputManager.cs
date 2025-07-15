using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public bool isLeftClickPressed;
    public Vector2 mousePositon;

    InputSystem _Input = null;

    private void OnEnable()
    {
        _Input = new InputSystem();

        _Input.PlayerInput.MouseButtons.performed += SetMouseButtons;
        _Input.PlayerInput.MouseButtons.canceled += SetMouseButtons;
    }

    private void Update()
    {
        if(Mouse.current != null)
        {
             mousePositon = Mouse.current.position.value;
        }
    }
    private void OnDisable()
    {
        _Input.PlayerInput.MouseButtons.performed -= SetMouseButtons;
        _Input.PlayerInput.MouseButtons.canceled -= SetMouseButtons;
    }
    void OnClick()
    {

    }
    void SetMouseButtons(InputAction.CallbackContext ctx)
    {
        isLeftClickPressed = ctx.ReadValue<bool>();
    }
}
