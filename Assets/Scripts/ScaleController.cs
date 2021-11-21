using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour
{
    public TextMesh textObject;

    private float weightOnScale = 0.0f;
    private string screenText;
    private float screenValue;
    [Tooltip("Measurement time given in frames")]
    public int MeasurementTime = 20;

    private int remainingWeighingFrames = 0;

    private void OnCollisionEnter(Collision collision)
    {
        weightOnScale += collision.rigidbody.mass;
        remainingWeighingFrames = MeasurementTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (remainingWeighingFrames > 0)
        {
            screenValue += (weightOnScale - screenValue) * 0.3f;
            remainingWeighingFrames--;
            screenText = screenValue.ToString("F2");
            textObject.text = screenText;
        }
        else if (remainingWeighingFrames == 0)
        {
            screenValue = weightOnScale;
            remainingWeighingFrames--;
            screenText = screenValue.ToString("F2");
            textObject.text = screenText;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        weightOnScale -= collision.rigidbody.mass;
        screenValue = weightOnScale;
        screenText = screenValue.ToString("F2");
        textObject.text = screenText;
    }
}
