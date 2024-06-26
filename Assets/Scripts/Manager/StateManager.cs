using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class StateManager : MonoBehaviour
{
    protected class State
    {
        public UnityEvent onStateEnter = new UnityEvent();
        public UnityEvent onStateUpdate = new UnityEvent();
        public UnityEvent onStateExit = new UnityEvent();

        public void InitState(UnityAction enter, UnityAction update, UnityAction exit)
        {
            onStateEnter.AddListener(enter);
            onStateUpdate.AddListener(update);
            onStateExit.AddListener(exit);
        }
    }

    private State currentState;
    private State nextState;

    private bool isChangingState = false;
    private bool shouldEnterState = true;


    protected virtual void Start()
    {
        // Votre code actuel
    }


    protected virtual void Update()
    {
        if (shouldEnterState)
        {
            currentState?.onStateEnter.Invoke();
            shouldEnterState = false;
            return;
        }

        currentState?.onStateUpdate.Invoke();

        if (isChangingState)
        {
            currentState?.onStateExit.Invoke();
            isChangingState = false;
            shouldEnterState = true;
            SetLatenState();
        }
    }

    protected void ChangeState(State _nextState)
    {
        nextState = _nextState;
        isChangingState = true;
    }

    private void SetLatenState()
    {
        currentState = nextState;
    }

    protected void ForcedCurrentState(State next)
    {
        currentState = next;
    }
}
