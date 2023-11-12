using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAreanScript : MonoBehaviour
{
    private Infiltration infiltration;
    bool repered;
    private MeshRenderer rendererr;
    private BasicsEnnemy[] listEnnemy; 
    // Start is called before the first frame update
    void Start()
    {
        rendererr = GetComponent<MeshRenderer>();
        infiltration = FindFirstObjectByType<Infiltration>();
        infiltration.Reperated.AddListener(() => repered = true);
    }

    // Update is called once per frame
    void Update()
    {
        if (repered && rendererr.material.color != Color.red)
        {
            rendererr.material.color = Color.red;
        }

        if(repered)
        {
            listEnnemy = FindObjectsOfType<BasicsEnnemy>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && (!repered || listEnnemy.Length == 0))
        {
            GameManager.Instance.lvSuivant.Invoke();
        }
    }
}
