using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public partial class BasicsEnnemy
{
    State overwatch = new State();
    [SerializeField] private float timeToOverwatch;
    [SerializeField] private List<float> listRotationOverwatch;
    [SerializeField] private float rotationSpeed;

    private float timerRotate;
    private float targetRotation;

    private bool goToGauche;
    private void OnOverwatchEnter()
    {
        targetRotation = listRotationOverwatch[0];
    }

    private void onOverwatchUpdate()
    {
        smoothRotate();
        PlayerReperated();
        otherRotation();
    }

    private void OnOverwatchExit() 
    {
    
    }

    private void otherRotation()
    {
        timerRotate += Time.deltaTime;
        if(timerRotate > timeToOverwatch)
        {
            timerRotate = 0;
            float a = targetRotation;
            while(Mathf.Abs( targetRotation - a) < 3f )
            {
                a = listRotationOverwatch[Random.Range(0, listRotationOverwatch.Count)];
            }
            targetRotation = a;
        }
    }

    private void smoothRotate()
    {
        float currentAngleY = transform.localEulerAngles.y;
        currentAngleY = Mathf.LerpAngle(currentAngleY, targetRotation, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(0, currentAngleY, 0);
    }
}
