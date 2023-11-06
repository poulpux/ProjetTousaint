using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class GameManager : StateManager
{

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

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
        ForcedCurrentState(menu);
    }

    protected override void Update()
    {
        base.Update();
    }
}
