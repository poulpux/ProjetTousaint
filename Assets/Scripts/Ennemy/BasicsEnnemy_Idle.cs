using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BasicsEnnemy
{
    State idle = new State();

    private void OnIdleEnter()
    {
        if(agent!=null && type != Type.PLOT)
        {
            agent.SetDestination(transform.position);
        }
    }

    private void onIdleUpdate()
    {

    }

    private void OnIdleExit()
    {

    }
}
