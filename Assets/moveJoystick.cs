using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveJoystick : MonoBehaviour
{
    [SerializeField] PlayerControler player;
    Vector2 initPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        player.posJoystick.AddListener((name, pos) =>
        {
            if (name == gameObject.name)
            {
                transform.position = initPos;
                transform.position += (Vector3) pos;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = initPos;
    }
}
