using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public partial class GameManager : StateManager
{
    private float timerALaCon;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [HideInInspector] public UnityEvent <Shoot> pickMeBoy = new UnityEvent <Shoot> ();
    Shoot player;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    protected override void Start()
    {
        base.Start();
        menu.InitState(onMenuEnter, onMenuUpdate, onMenuExit);
        game.InitState(onGameEnter, onGameUpdate, onGameExit);
        pause.InitState(onPauseEnter, onPauseUpdate, onPauseExit);
        replay.InitState(onReplayEnter, onReplayUpdate, onReplayExit);
        quit.InitState(onQuitEnter,null,null);
        ForcedCurrentState(menu);

        GoToMenuOption.AddListener((value)=>inOption =  value);

        lvSuivant.AddListener(() =>
        {
            if (timerALaCon > 1f)
            {
                PlayerPrefs.SetInt("nbAmmo", player.currentBullet);
                PlayerPrefs.Save();
                sceneNumber++;
                SceneManager.LoadScene(levelList[sceneNumber]);
                timerALaCon = 0f;
            }
        });

        dead.AddListener(() =>
        {
            SceneManager.LoadScene(levelList[sceneNumber]);
        });

        pickMeBoy.AddListener((playerr) => player = playerr);
    }

    protected override void Update()
    {
        base.Update();
        timerALaCon += Time.deltaTime;
    }

    public void BackToMenu()
    {
        sceneNumber = 0;
        ChangeState(menu);
    }
}
