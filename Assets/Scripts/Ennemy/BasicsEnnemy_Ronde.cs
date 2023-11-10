using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class BasicsEnnemy
{
    State ronde = new State();
    [SerializeField] private List<GameObject> listObj;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float timeWait;
    [SerializeField] private float speedWalk;
    private int objToGo = 0;

    private float timerRonde;
    private void OnRondeEnter()
    {
        
    }

    private void onRondeUpdate()
    {
        PlayerReperated();
        walkGestion();
    }

    private void OnRondeExit()
    {

    }

    private void assignNewObjecj()
    {
        timerRonde = 0;
        agent.SetDestination(listObj[objToGo%listObj.Count].transform.position);
        objToGo++;
        //objToGo = objToGo == listObj.Count+1 ? 0 : objToGo ++;
    }

    private void walkGestion()
    {
        if(agent.remainingDistance < agent.radius)
        {
            if (timerRonde > timeWait)
                assignNewObjecj();
            else
                timerRonde += Time.deltaTime;
        }
    }
}
