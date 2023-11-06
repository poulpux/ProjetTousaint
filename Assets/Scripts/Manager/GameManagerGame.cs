using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager
{
    State game = new State();

    private void onGameEnter()
    {
        SceneManager.LoadScene("Game");
    }
    private void onGameUpdate()
    {

    }

    private void onGameExit()
    {

    }

    public void GoInGame()
    {
        ChangeState(game);
        SceneManager.LoadScene("Game");
    }

}
