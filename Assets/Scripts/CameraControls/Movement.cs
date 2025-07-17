using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] InputManagerForSHGV01 _input;

    [Header("MovementInput Variables")]
    [SerializeField] Vector3 startPosForCamera;
    [SerializeField] Vector2 XPosBounds;
    [SerializeField] Vector2 ZPosBounds;
    [SerializeField] float yPosOffset = -0.56f;
    [SerializeField][Range(1.0f, 10.0f)] float degreesPerFrame;
    [SerializeField][Range(1.0f, 50.0f)] float rotationSpeed;
    [SerializeField] Vector2 forwardMovementSpeedMinMax;
    float currentForwardMovementSpeed;
    [SerializeField] Vector2 sideMovementSpeedMinMax;
    float currentSideMovementSpeed;

    //MovementVariables
    Vector2 currentMovement = Vector2.zero;
    float currentXPos = 0.0f;
    float currentZPos = 0.0f;

    //Rotation Variables
    Vector2 currentRotation = Vector2.zero;
    float currentYAngle = 0.0f;
    Quaternion currentYRotation;
    Quaternion orignalRotation = Quaternion.identity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //currentForwardMovementSpeed = forwardMovementSpeedMinMax.y;
        //currentSideMovementSpeed = sideMovementSpeedMinMax.y;
        orignalRotation = transform.rotation;
        transform.position = startPosForCamera;
    }

    // Update is called once per frame
    void Update()
    {
        
        CheckForInput();
        UpdateMovement();
    }
    void CheckForInput()
    {
        currentMovement = new Vector2(_input.GetMovementInput().x, _input.GetMovementInput().y);
        currentRotation = new Vector2(_input.GetRotationDirInput().x, _input.GetRotationDirInput().y);
        
        if (Mathf.Abs(currentRotation.y) > 0.01f)
        {
            RotateOnYAxis();
        }
        if (Mathf.Abs(currentRotation.x) > 0.01f)
        {
            ResetRotation();
        }
    }
    private void ResetRotation()
    {
        Debug.Log("Triggerd RestRotation");
        currentYAngle = 0f;
        currentYRotation = orignalRotation;
        transform.rotation = orignalRotation;
    }

    void RotateOnYAxis()
    {
        if (currentRotation.y < 0.0f)
        {
            currentYAngle -= degreesPerFrame * rotationSpeed * Time.deltaTime;
        }


        else
        {
            currentYAngle += degreesPerFrame * rotationSpeed * Time.deltaTime;
        }

        currentYAngle = Mathf.Repeat(currentYAngle, 360f);

        Quaternion targetRotation = Quaternion.AngleAxis(currentYAngle, Vector3.up);
        currentYRotation = targetRotation;//Quaternion.Slerp(currentYRotation, targetRotation, (rotationSpeed * Time.deltaTime));
    }
    void ClampPosToBounds(Vector3 nextPos)
    {
        nextPos.x = Mathf.Clamp(nextPos.x, XPosBounds.x, XPosBounds.y);
        nextPos.z = Mathf.Clamp(nextPos.z, ZPosBounds.x, ZPosBounds.y);

        currentXPos = nextPos.x;
        currentZPos = nextPos.z;
    }
    void UpdateMovement()
    {
        Vector3 inputDir = new Vector3(currentMovement.x, 0, currentMovement.y);
        if(inputDir.magnitude > 1.0f)
            inputDir.Normalize();

        Vector3 deltaMovement = (transform.right * inputDir.x * currentForwardMovementSpeed + transform.forward * inputDir.z * currentSideMovementSpeed) * Time.deltaTime;

        Vector3 nextPos = transform.position + deltaMovement;
        ClampPosToBounds(nextPos);

        transform.position = new Vector3(currentXPos, yPosOffset, currentZPos);
        transform.rotation = currentYRotation;
    }
    public void SetCurrentForwardMovementSpeed(float newMovementSpeed) { currentForwardMovementSpeed = newMovementSpeed; }
    public void SetCurrentSideMovementSpeed(float newMovementSpeed) {currentSideMovementSpeed = newMovementSpeed; }
    public Vector2 GetForwardMovementSpeedMinMax() {  return forwardMovementSpeedMinMax; }
    public Vector2 GetSideMovementSpeedMinMax() { return sideMovementSpeedMinMax; }
}
