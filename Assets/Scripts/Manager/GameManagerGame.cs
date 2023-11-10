using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public partial class GameManager
{
    State game = new State();

    [SerializeField]
    List<string> levelList;
    private int sceneNumber;
    [HideInInspector] public UnityEvent lvSuivant = new UnityEvent();
    [HideInInspector] public UnityEvent dead = new UnityEvent();
    private void onGameEnter()
    {
        PlayerPrefs.SetInt("nbAmmo", 0);
        SceneManager.LoadScene(levelList[0]);
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
