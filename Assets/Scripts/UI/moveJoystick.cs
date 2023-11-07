using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveJoystick : MonoBehaviour
{
    Vector2 initPos;

    bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        InputManager.Instance.posJoystick.AddListener((name, pos) =>
        {
            if (name == gameObject.name)
            {
                transform.position = initPos;
                transform.position += (Vector3) pos;
                isMoving = true;
            }
        });
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if(!isMoving)
            transform.position = initPos;
        isMoving = false;
    }
}
