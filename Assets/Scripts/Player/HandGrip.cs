using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HandGrip : MonoBehaviour
{
    [SerializeField] private InputActionProperty actionLeftValue;
    [SerializeField] private InputActionProperty actionRightValue;

    [Header("Spheres")]
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    [Header("Open Positions")]
    [SerializeField] private Vector3 sphereLeftOpenPos;
    [SerializeField] private Vector3 sphereRightOpenPos;

    [Header("Grip Positions")]
    [SerializeField] private Vector3 sphereLeftGripPos;
    [SerializeField] private Vector3 sphereRightGripPos;

    [Header("Settings")]
    [SerializeField] private float smoothSpeed = 10f;

    [SerializeField] private TextMeshPro splurtIndicator;

    private bool isGrippedLeft;
    private bool isGrippedRight;

    void Update()
    {
        isGrippedLeft = actionLeftValue.action.ReadValue<float>() > 0.5f;
        isGrippedRight = actionRightValue.action.ReadValue<float>() > 0.5f;

        Vector3 target1 = isGrippedLeft ? sphereLeftGripPos : sphereLeftOpenPos;
        Vector3 target2 = isGrippedRight ? sphereRightGripPos : sphereRightOpenPos;

        leftHand.localPosition = Vector3.Lerp(leftHand.localPosition, target1, smoothSpeed * Time.deltaTime);
        rightHand.localPosition = Vector3.Lerp(rightHand.localPosition, target2, smoothSpeed * Time.deltaTime);

        //splurtIndicator.text = $"{Mathf.RoundToInt((float)MakeSplurt.splurtCount / MakeSplurt.splurtsCreated * 100f)} %";
        if (splurtIndicator != null)
        {
            splurtIndicator.text = $"{MakeSplurt.splurtCount}";
        }


    }
}
