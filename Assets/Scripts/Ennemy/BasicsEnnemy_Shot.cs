using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BasicsEnnemy
{
    State shot = new State();
    [SerializeField] GameObject EnnemyShotPrefab;

    [SerializeField] float timeToReact;
    [SerializeField] float Occurency;
    [SerializeField] int DelayShoot;
    [SerializeField] int nbBalleParRafale;
    [SerializeField] float tpsAvantNouvelleRafale;


    private void OnShotEnter()
    {

    }

    private void onShotUpdate()
    {
        GoToRotation();
    }

    private void OnShotExit()
    {

    }

    private void GoToRotation()
    {
        if (ThrowRayOnPlayer() == true)
        {
            Vector3 direction = playerInfiltration.transform.position - transform.position;
            direction.y = 0; // Garder l'objet à plat

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * 1000f);
            }
        }
    }

    private bool ThrowRayOnPlayer()
    {
        if ( playerInfiltration.gameObject != null)
        {
            // Obtenez la direction du rayon
            Vector3 rayDirection = playerInfiltration.gameObject.transform.position - this.transform.position;

            // Effectuez le raycast
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, rayDirection, out hit))
            {
                if(hit.collider.CompareTag("Player"))
                    return true;
            }
        }

        return false;
    }
}
