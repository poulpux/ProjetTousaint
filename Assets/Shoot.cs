using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject ShootPrefab;
    [SerializeField] int delayShoot;

    public int maxBullet;
    public int currentBullet;

    private float timerShoot;
    private Vector3 posTir;
    // Start is called before the first frame update
    void Start()
    {
        currentBullet = PlayerPrefs.GetInt("nbAmmo");
        InputManager.Instance.posJoystick.AddListener((name, pos) =>
        {
            if (name == "CircleMoveR")
            {
                posTir =pos;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.Instance.right != null)
        {
            ThrowShooot();
        }
        else if(InputManager.Instance.right == null && timerShoot > (1f / delayShoot) / 2f)
        {
            timerShoot = (1f / delayShoot)/2f;
        }
        else
        {
            timerShoot += Time.deltaTime;
        }
    }

    private void ThrowShooot()
    {
        timerShoot += Time.deltaTime;
        if (timerShoot > 1f / delayShoot && currentBullet>0)
        {
            currentBullet--;
            timerShoot = 0;
            GameObject a = Instantiate(ShootPrefab, transform.position, Quaternion.identity);
            Vector3 vec3 = new Vector3(posTir.x, 0f, posTir.y);
            vec3.Normalize();
            a.GetComponent<Rigidbody>().AddForce(vec3 * 30, ForceMode.Impulse);
            Destroy(a, 3f);
        }
    }

    public void addAmmo(int ammo)
    {
        currentBullet = currentBullet + ammo > maxBullet ? maxBullet : currentBullet + ammo;
    }
}
