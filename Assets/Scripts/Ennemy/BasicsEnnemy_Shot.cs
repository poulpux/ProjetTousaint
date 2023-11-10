using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BasicsEnnemy
{
    State shot = new State();

    private float timerShoot;
    private int nbBalleTir�s;

    private void OnShotEnter()
    {

    }

    private void onShotUpdate()
    {
        GoToRotation();
        shoot();
    }

    private void OnShotExit()
    {

    }

    private void GoToRotation()
    {
        if (ThrowRayOnPlayer() == true)
        {
            if (playerInfiltration.gameObject != null)
            {
                Vector3 direction = playerInfiltration.transform.position - transform.position;
                direction.y = 0;

                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * 1000f);
                }
            }
        }
    }

    private bool ThrowRayOnPlayer()
    {
        if ( playerInfiltration.gameObject != null)
        {
            Vector3 rayDirection = playerInfiltration.gameObject.transform.position - this.transform.position;

            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, rayDirection, out hit))
            {
                if(hit.collider.CompareTag("Player"))
                    return true;
            }
        }

        return false;
    }

    private void shoot()
    {

        if (timerShoot < (float)nbBalleParRafale / (float)DelayShoot + timeToReact + delayEntreRafale)
        {
            if (timerShoot > (float)nbBalleTir�s / (float)DelayShoot + timeToReact && nbBalleTir�s <= nbBalleParRafale)
            {
                nbBalleTir�s++;
                GameObject shot = Instantiate(EnnemyShotPrefab, transform.position, Quaternion.identity);

                float rotationY = transform.eulerAngles.y + Random.Range(-Occurency, Occurency);  // Obtenez l'angle en degr�s
                float angleInRadians = rotationY * Mathf.Deg2Rad;

                Vector3 direction = new Vector3(Mathf.Sin(angleInRadians), 0f, Mathf.Cos(angleInRadians));
                shot.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
            }
        }
        else
        {
            nbBalleTir�s = 0;
            timerShoot = 0;
        }


        if(timerShoot < timeToReact)
        {
            if (ThrowRayOnPlayer() == true)
                timerShoot += Time.deltaTime;
            else
                timerShoot = 0;
        }
        else
        {
            timerShoot += Time.deltaTime;
        }
    }
}
