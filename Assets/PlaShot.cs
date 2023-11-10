using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("EnnemyBody") == true || collision.gameObject.CompareTag("Ennemy") == true)
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Enviro") == true)
        {
            Destroy(this.gameObject);
        }
    }
}
