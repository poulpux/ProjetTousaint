using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    OVERWATCH,
    RONDE
}

public partial class BasicsEnnemy : StateManager
{
    [SerializeField] Type type;

    private Infiltration playerInfiltration;
    [SerializeField] private GameObject CadavrePref;

    [SerializeField] GameObject EnnemyShotPrefab;

    [SerializeField] float timeToReact;
    [SerializeField] float delayEntreRafale;
    [SerializeField] float Occurency;
    [SerializeField] int DelayShoot;
    [SerializeField] int nbBalleParRafale;
    [SerializeField] float bulletSpeed;
    [SerializeField] float tpsAvantNouvelleRafale;
    private GameObject cadavreInstance = null;

    private bool aggressive;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        idle.InitState(OnIdleEnter, onIdleUpdate,OnIdleExit);
        ronde.InitState(OnRondeEnter, onRondeUpdate, OnRondeExit);
        shot.InitState(OnShotEnter, onShotUpdate, OnShotExit);
        takeCover.InitState(OnTakeCoverEnter, onTakeCoverUpdate, OnTakeCoverExit);
        overwatch.InitState(OnOverwatchEnter, onOverwatchUpdate, OnOverwatchExit);

        choseComportement();
        
        SomeOtherMethod();

        playerInfiltration = FindFirstObjectByType<Infiltration>();
        playerInfiltration.Reperated.AddListener(()=> aggressive = true);
        agent.speed = speedWalk;
    }

    // Update is called once per frame
    protected override void Update()
    {
       base.Update();
    }

    private void OnDestroy()
    {
        if(cadavreInstance != null)
              cadavreInstance.transform.position = transform.position;
    }

    private void SomeOtherMethod()
    {
        // Appeler cette méthode à partir d'un endroit approprié
        cadavreInstance = Instantiate(CadavrePref, new Vector3(-5000,-5000), Quaternion.identity);
    }

    private void choseComportement()
    {
        if (type == Type.RONDE)
            ForcedCurrentState(ronde);
        else
            ForcedCurrentState(overwatch);
    }

    private void PlayerReperated()
    {
        if (aggressive)
        {
            agent.isStopped = true;
            ChangeState(shot);
        }
    }
}
