using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float currentTimeScale = 1f;

    private static TimeManager instance;
    public static TimeManager Instance { get { return instance; } }

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
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public float getCurrentTimeScale()
    {
        return currentTimeScale;
    }
    public void setCurrentTimeScale(float timeScale)
    {
        currentTimeScale = timeScale;
        Time.timeScale = currentTimeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void DoSlow()
    {
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
