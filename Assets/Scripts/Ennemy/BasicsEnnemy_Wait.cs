using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class BasicsEnnemy
{
    State overwatch = new State();

    private void OnOverwatchEnter()
    {

    }

    private void onOverwatchUpdate()
    {
        PlayerReperated();
    }

    private void OnOverwatchExit() 
    {
    
    }
}
