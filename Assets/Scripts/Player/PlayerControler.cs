using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] float speedWalk;
    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerCapacity capa;
    private void Start()
    {
        capa = FindFirstObjectByType<PlayerCapacity>(); 
    }

    private void Update()
    {
        if ((InputManager.Instance.right != null || InputManager.Instance.left !=null) && Time.timeScale != 0f && InputManager.Instance.canMove)
        {
            Rotation();
        }

        if(InputManager.Instance.left != null && InputManager.Instance.canMove)
        {
            Movement();
        }
        else if(InputManager.Instance.canMoveDash)
        {
            rb.Sleep();
        }
    }

    private void Rotation()
    {
        if (InputManager.Instance.right != null)
        {
            float angle = Mathf.Atan2(InputManager.Instance.rJoystickValue.y, InputManager.Instance.rJoystickValue.x) * Mathf.Rad2Deg;
            Vector3 vec3 = new Vector3(0, -angle - 90, 0f);
            transform.localRotation = Quaternion.Euler(vec3);
        }
        else if(InputManager.Instance.left != null)
        {
            float angle = Mathf.Atan2(InputManager.Instance.lJoystickValue.y, InputManager.Instance.lJoystickValue.x) * Mathf.Rad2Deg;
            Vector3 vec3 = new Vector3(0, -angle - 90, 0f);
            transform.localRotation = Quaternion.Euler(vec3);
        }
    }
    

    private void Movement()
    {
        rb.velocity = new Vector3(InputManager.Instance.lJoystickValue.x,0f, InputManager.Instance.lJoystickValue.y) * -speedWalk;
    }    
}
