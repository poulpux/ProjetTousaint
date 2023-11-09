using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BasicsEnnemy
{
    State ronde = new State();

    private void OnRondeEnter()
    {

    }

    private void onRondeUpdate()
    {
        PlayerReperated();
    }

    private void OnRondeExit()
    {

    }

    
}
