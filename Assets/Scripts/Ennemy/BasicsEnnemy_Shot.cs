using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BasicsEnnemy
{
    State shot = new State();

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
        Vector3 direction = playerInfiltration.transform.position - transform.position;
        direction.y = 0; // Garder l'objet à plat

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * 1000f);
        }
        
    }
}
