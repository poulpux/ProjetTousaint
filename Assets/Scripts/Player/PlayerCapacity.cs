using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCapacity : MonoBehaviour
{
    [SerializeField] private float dashCouldown;
    [SerializeField] private float dashDistance;

    private float timerDash;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.tap.AddListener((touchPos) =>
        {
            if (cameraRaycast.Instance.detectTouch(touchPos) == "DashButton")
            {
                Debug.Log("Dash");
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
    }

    void useDash()
    {
        if (timerDash > dashCouldown)
        {
            Debug.Log("Fonction dash");

            float radAngle = (transform.localRotation.eulerAngles.y-90f) * Mathf.Deg2Rad; // Convertir l'angle en radians

            float x = Mathf.Cos(radAngle); // Calculer la coordonnée x
            float y = Mathf.Sin(radAngle); // Calculer la coordonnée y

            Vector3 normalizedCoordinates = new Vector3(x,0f, y); // Créer un vecteur 2D avec les coordonnées normalisées

            transform.position += normalizedCoordinates * -dashDistance;
            timerDash = 0;
        }
    }

    void useTimeStop()
    {

    }
}
