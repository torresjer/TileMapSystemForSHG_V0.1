using UnityEngine;

public class ExpontiallCurveZoom : MonoBehaviour
{
    [SerializeField] InputManagerForSHGV01 _input;
    [Header("---- ScrollWheelInput Settings ----")]
    [SerializeField] float zoomSpeed = 2.0f;
    [SerializeField] float minZoomLevel = 0.0f;
    [SerializeField] float maxZoomLevel = 5.0f;

    [Header("---- Exponential Curve Y ----")]
    [SerializeField] ExponentialCurve yCurve;


    [Header("---- Exponential Curve Z ----")]
    [SerializeField] ExponentialCurve zCurve;
    [SerializeField] bool isZNegative;

    [Header("---- Start of ScrollWheelInput Camera Angle - End of ScrollWheelInput Camera Angle ----")]
    [SerializeField] float startOfZoomAngle = 10f;
    [SerializeField] float endOfZoomAngle = 80f;

    [Header("---- Debug Options ----")]
    [SerializeField] bool debugDrawCurve = true;
    [SerializeField] float debugExpentionalStep = .5f;
    [SerializeField] float debugDuration = 5f;
    [SerializeField] Color yCurveColor = Color.red;
    [SerializeField] Color zCurveColor = Color.blue;
    [SerializeField] float debugLineHeight = 1f;

    //Inputs to gather
    //you will need a scroll wheel input one for going up and one for going down,
    //you will need a the key/button that is effecting the parents movement rotation. 
    Vector2 scrollInput = Vector2.zero;
    Vector2 rotationInput = Vector2.zero;

    //Internal Tracker
    float currentZoomLevel = 0f;

    //NonSerilzed Refrences
    Movement parentMovementScript;

    private void Awake()
    {
        //Checks to see if parent object has player movement script
        parentMovementScript = GetComponentInParent<Movement>();
        MoveCameraBasedOnScrollInput();
    }

    void Update()
    {
        scrollInput = new Vector2(_input.GetScrollWheelInput().x, _input.GetScrollWheelInput().y);
        rotationInput = new Vector2(_input.GetRotationDirInput().x, _input.GetRotationDirInput().y);

        if (Mathf.Abs(scrollInput.y) > 0.01f)
            MoveCameraBasedOnScrollInput();

        if (Mathf.Abs(rotationInput.x) > 0.01f)
            ResetZoom();

        DrawExponentialCurves();
    }
    private void MoveCameraBasedOnScrollInput()
    {
        //Calculates the current zoom level based on our scroll input and our zoom speed.
        currentZoomLevel += scrollInput.y * zoomSpeed * Time.deltaTime;
        //insures it stays within a certian range allowing us to creat bounds
        currentZoomLevel = Mathf.Clamp(currentZoomLevel, minZoomLevel, maxZoomLevel);

        float yOffset = yCurve.Evaluate(currentZoomLevel);
        float zOffset = zCurve.Evaluate(currentZoomLevel);

        if (isZNegative)
        {
            zOffset *= -1;

        }


        float t = Mathf.InverseLerp(minZoomLevel, maxZoomLevel, currentZoomLevel);
        float tiltAngle = Mathf.Lerp(endOfZoomAngle, startOfZoomAngle, t);
        //Decrease playerspeed from both the side and forward direction based on the mins and maxs of each direction and using the zoomlevel to lerp in between those values.
        parentMovementScript.SetCurrentForwardMovementSpeed(Mathf.Lerp(parentMovementScript.GetForwardMovementSpeedMinMax().y, parentMovementScript.GetForwardMovementSpeedMinMax().x, t));
        parentMovementScript.SetCurrentSideMovementSpeed(Mathf.Lerp(parentMovementScript.GetSideMovementSpeedMinMax().y, parentMovementScript.GetSideMovementSpeedMinMax().x, t));
        transform.localPosition = new Vector3(0, yOffset, zOffset);
        transform.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
    }
    private void ResetZoom()
    {
        currentZoomLevel = minZoomLevel;
        MoveCameraBasedOnScrollInput();
    }
    private void DrawExponentialCurves()
    {
        if (!debugDrawCurve) return;

        Vector3 prevYPoint = Vector3.zero;
        Vector3 prevZPoint = Vector3.zero;
        bool firstPoint = true;

        for (float zoom = minZoomLevel; zoom <= maxZoomLevel; zoom += debugExpentionalStep)
        {
            float yOffset = yCurve.Evaluate(zoom);
            float zOffset = zCurve.Evaluate(zoom);
            if (isZNegative)
            {
                zOffset *= -1;

            }
            Vector3 yPoint = new Vector3(zoom, yOffset, 0);
            Vector3 zPoint = new Vector3(zoom, zOffset, debugLineHeight);
           
            if (!firstPoint)
            {
                Debug.DrawLine(prevYPoint, yPoint, yCurveColor, debugDuration);
                Debug.DrawLine(prevZPoint, zPoint, zCurveColor, debugDuration);
            }
            else
            {
                firstPoint = false;
            }

            prevYPoint = yPoint;
            prevZPoint = zPoint;
        }
    }

}
    


