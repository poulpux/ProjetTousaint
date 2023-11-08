using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCapacity : MonoBehaviour
{
    public float dashCouldown;
    [SerializeField] private float dashDistance;

    [SerializeField] private int maxTimeStop = 3;
    [SerializeField] private float timeStopPower;
    public int currentTimeStopCharge = 1;
    public float timeStopDuration;

    [HideInInspector] public float timerDash;
    [HideInInspector] public float timerTimeStop;
    [HideInInspector] public bool inTimeStop;
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
    }

    void useDash()
    {
        if (timerDash > dashCouldown)
        {
            float radAngle = (transform.localRotation.eulerAngles.y+90f) * Mathf.Deg2Rad; // Convertir l'angle en radians

            float x = Mathf.Cos(radAngle); // Calculer la coordonnée x
            float y = Mathf.Sin(radAngle); // Calculer la coordonnée y

            Vector3 normalizedCoordinates = new Vector3(-x,0f, y); // Créer un vecteur 2D avec les coordonnées normalisées

            transform.position +=   normalizedCoordinates * dashDistance;
            timerDash = 0;
        }
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
            if(timerTimeStop > timeStopDuration)
            {
                TimeManager.Instance.setCurrentTimeScale(1f);
                timerTimeStop = 0;
                inTimeStop = false;
            }

        }
    }
}
