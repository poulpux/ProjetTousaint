using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : StateManager
{
    State menu = new State();

    private void onMenuEnter()
    {
        SceneManager.LoadScene("Menu");
    }
    private void onMenuUpdate()
    {

    }

    private void onMenuExit()
    {

    }

}
