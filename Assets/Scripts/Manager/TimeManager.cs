using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    private float globalTimer;
    public float currentTimer;
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
        GameManager.Instance.lvSuivant.AddListener(() =>
        {
            if (SceneManager.GetActiveScene().name != "Tuto0" && SceneManager.GetActiveScene().name != "Tuto1")
            {
                globalTimer += currentTimer;
            }
            currentTimer = 0;
        });

        GameManager.Instance.dead.AddListener(() =>
        {
            currentTimer = 0;
        });
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
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
        Time.fixedDeltaTime = Time.timeScale * 0.01f;
    }
}
