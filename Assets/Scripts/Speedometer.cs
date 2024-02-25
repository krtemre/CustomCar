using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private const float MAX_ANGLE = -20;
    [SerializeField] private const float MIN_ANGLE = 200;

    private float totalAngleSize = MIN_ANGLE - MAX_ANGLE;

    [SerializeField] private Transform needleTransform;
    [SerializeField] private TextMeshProUGUI shiftText;
    [SerializeField] private TextMeshProUGUI digitialSpeedText;

    [SerializeField] private float needleUpdateSpeed = 3f;
    private float targetZ;

    private void Start()
    {
        targetZ = MIN_ANGLE;
    }

    private void Update()
    {
        float currentZ = needleTransform.eulerAngles.z;

        currentZ = currentZ > MIN_ANGLE ? currentZ - 360f : currentZ;

        if (Mathf.Abs(currentZ - targetZ) > 1f)
        {
            Vector3 updatedRotation = new Vector3(0, 0, currentZ + ((targetZ - currentZ) * Time.deltaTime * needleUpdateSpeed));

            if (Mathf.Abs(updatedRotation.z - targetZ) < 10f)
            {
                updatedRotation.z = targetZ;
            }

            needleTransform.eulerAngles = updatedRotation;
        }
    }

    public void UpdateSpeedometer(float speedNormalized)
    {
        if (needleTransform == null)
        {
            Debug.LogWarning("The needle cannot be null");
            return;
        }

        targetZ = MIN_ANGLE - speedNormalized * totalAngleSize;
    }

    public void TryUpdateDigitalSpeed(int speed)
    {
        if (digitialSpeedText == null)
        {
            Debug.LogWarning("The Digitial Speed Text text cannot be null");
            return;
        }

        digitialSpeedText.text = speed.ToString();
    }

    public void TryUpdateShift(int shift)
    {
        if (shiftText == null)
        {
            Debug.LogWarning("The shift text cannot be null");
            return;
        }

        shiftText.text = shift.ToString();
    }
}
