using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public partial class GameManager
{
    private State menu = new State();
    private bool inOption;
    [HideInInspector] public UnityEvent <bool>GoToMenuOption = new UnityEvent<bool>();
    private void onMenuEnter()
    {
        SceneManager.LoadScene("Menu");
    }
    private void onMenuUpdate()
    {
       
    }

    private void onMenuExit()
    {
        currentBalls = 0;
        SceneManager.LoadScene(levelList[0]);
    }

    public void GoOptionMenu()
    {
        GoToMenuOption.Invoke(true);
    }
    public void OutOptionMenu()
    {
        GoToMenuOption.Invoke(false);
    }

}
