using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Android;

public partial class GameManager : StateManager
{
    public int currentBalls;

    private float timerTropBien;

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

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }

        menu.InitState(onMenuEnter, onMenuUpdate, onMenuExit);
        game.InitState(onGameEnter, onGameUpdate, onGameExit);
        pause.InitState(onPauseEnter, onPauseUpdate, onPauseExit);
        replay.InitState(onReplayEnter, onReplayUpdate, onReplayExit);
        quit.InitState(onQuitEnter,null,null);
        ForcedCurrentState(menu);

        GoToMenuOption.AddListener((value)=>inOption =  value);

        lvSuivant.AddListener(() =>
        {
            if (timerTropBien > 1f)
            {
                currentBalls = player.currentBullet;
                sceneNumber++;
                SceneManager.LoadScene(levelList[sceneNumber]);
                timerTropBien = 0f;
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
        timerTropBien += Time.deltaTime;
    }

    public void BackToMenu()
    {
        sceneNumber = 0;
        GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroyOnLoad");
        foreach (GameObject obj in dontDestroyObjects)
        {
            Destroy(obj);
        }
        ChangeState(menu);
    }
}
