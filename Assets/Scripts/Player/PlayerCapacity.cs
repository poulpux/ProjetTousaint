using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCapacity : MonoBehaviour
{
    public float dashCouldown;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashTime;

    public int maxTimeStop = 3;
    public float timeStopPower;
    public int currentTimeStopCharge = 1;
    public float timeStopDuration;

    [HideInInspector] public float timerDash;
    [HideInInspector] public float timerTimeStop;
    [HideInInspector] public bool inTimeStop;

    [SerializeField] Rigidbody rb;

    public bool inDashInfo;

    private float timerInDash;
    private Vector3 targetDirection;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.tap.AddListener((touchPos) =>
        {
            if (cameraRaycast.Instance.detectTouch(touchPos) == "DashButton")
            {
                useDash();
            }

            if (cameraRaycast.Instance.detectTouch(touchPos) == "TimeButton")
            {
               
                useTimeStop();
            }
        });
    }

        // Update is called once per frame
    void Update()
    {
        timerDash += Time.deltaTime;
        inTimeStoping();
        inDash();
    }

    void useDash()
    {
        if (timerDash > dashCouldown)
        {
            timerInDash = 0f;
            float radAngle = (transform.localRotation.eulerAngles.y+90f) * Mathf.Deg2Rad; // Convertir l'angle en radians

            float x = Mathf.Cos(radAngle); // Calculer la coordonnée x
            float y = Mathf.Sin(radAngle); // Calculer la coordonnée y

            Vector3 normalizedCoordinates = new Vector3(-x,0f, y); // Créer un vecteur 2D avec les coordonnées normalisées

            targetDirection = normalizedCoordinates;
            timerDash = 0;
        }
    }

    void inDash()
    {
        if (timerInDash < dashTime)
        {
            inDashInfo = true;
            InputManager.Instance.canMove = false;
            timerInDash += Time.deltaTime;
            rb.velocity = targetDirection * dashDistance / dashTime;
        }
        else
            inDashInfo = false;
    }

    void useTimeStop()
    {
        if(currentTimeStopCharge >0 && !inTimeStop)
        {
            currentTimeStopCharge--;
            inTimeStop = true;
            TimeManager.Instance.setCurrentTimeScale(timeStopPower) ;
        }
    }

    private void inTimeStoping()
    {
        if(inTimeStop)
        {
            timerTimeStop += Time.unscaledDeltaTime;
            if(timerTimeStop > timeStopDuration/timeStopPower)
            {
                TimeManager.Instance.setCurrentTimeScale(1f);
                timerTimeStop = 0;
                inTimeStop = false;
            }

        }
    }
}
