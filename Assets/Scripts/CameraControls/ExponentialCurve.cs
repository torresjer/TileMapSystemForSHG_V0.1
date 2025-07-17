using UnityEngine;

[System.Serializable]
public class ExponentialCurve
{
    public float startValue;
    public float endValue;
    public float startZoomLevel;
    public float endZoomLevel;

    [Tooltip("Base of the exponential. 0.5 = steeper drop, 0.9 = flatter drop.")]
    public float curveBase = 0.5f;

    public float Evaluate(float zoomLevel)
    {
        // Clamp zoom level
        zoomLevel = Mathf.Clamp(zoomLevel, startZoomLevel, endZoomLevel);

        float x0 = startZoomLevel;
        float x1 = endZoomLevel;

        float B = curveBase;

        // Handle flat curves (no change)
        if (Mathf.Approximately(startValue, endValue))
        {
            return startValue;
        }

        float powStart = Mathf.Pow(B, x0);
        float powEnd = Mathf.Pow(B, x1);
        float powCurrent = Mathf.Pow(B, zoomLevel);

        float numerator = powCurrent - powEnd;
        float denominator = powStart - powEnd;

        // Avoid divide by zero
        if (Mathf.Abs(denominator) < 1e-5f)
        {
            return startValue;
        }

        float t = numerator / denominator;

        // Lerp from endValue to startValue
        float result = Mathf.Lerp(endValue, startValue, t);
        return result;
    }
}
