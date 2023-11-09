using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisionField : MonoBehaviour
{
    public Material VisionConeMaterial;
    public float VisionRange;
    public float VisionAngle;
    public LayerMask VisionObstructingLayer;//layer with objects that obstruct the enemy view, like walls, for example
    public int VisionConeResolution = 120;//the vision cone will be made up of triangles, the higher this value is the pretier the vision cone will be
    Mesh VisionConeMesh;
    MeshFilter MeshFilter_;
    //Create all of these variables, most of them are self explanatory, but for the ones that aren't i've added a comment to clue you in on what they do
    //for the ones that you dont understand dont worry, just follow along

    [SerializeField] private float timeToDetect;

    private float timerDetect;

    private Infiltration playerInfiltration;
    void Start()
    {
        transform.AddComponent<MeshRenderer>().material = VisionConeMaterial;
        MeshFilter_ = transform.AddComponent<MeshFilter>();
        VisionConeMesh = new Mesh();
        VisionAngle *= Mathf.Deg2Rad;

        VisionConeMaterial.color = new Color(1, 1, 0f, 166f/255f);

        playerInfiltration = FindFirstObjectByType<Infiltration>();
        playerInfiltration.Reperated.AddListener(() =>
        {
            VisionConeMesh.Clear();
            this.enabled = false;
        });
    }


    void Update()
    {
        ReperatPlayer();
        changeConeColor();
    }

    private void detectPlayer()
    {
        int[] triangles = new int[(VisionConeResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[VisionConeResolution + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -VisionAngle / 2;
        float angleIncrement = VisionAngle / (VisionConeResolution - 1);
        float Sine;
        float Cosine;
        bool playerDetected = false;

        for (int i = 0; i < VisionConeResolution; i++)
        {
            Sine = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);
            Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
            Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit1, VisionRange))
            {
                if(hit1.collider.CompareTag("Cadavre"))
                    playerDetected = true;
            }

            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, VisionRange, VisionObstructingLayer))
            {
                //timerDetect += Time.deltaTime;
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Cadavre"))
                {
                    playerDetected = true;
                }

                
                Vertices[i + 1] = VertForward * hit.distance;

                // Vérifier si l'objet touché est le joueur
            }
            else
            {
                Vertices[i + 1] = VertForward * VisionRange;
            }

            Currentangle += angleIncrement;
        }

        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }

        VisionConeMesh.Clear();
        VisionConeMesh.vertices = Vertices;
        VisionConeMesh.triangles = triangles;
        MeshFilter_.mesh = VisionConeMesh;

        if (playerDetected)
        {
            timerDetect += Time.deltaTime;
            // Faites quelque chose si le joueur est détecté
        }
        else
        {
            if(timerDetect >0)
                timerDetect -= Time.deltaTime;
        }

    }
        private void ReperatPlayer()
    {
        detectPlayer();
        if(timerDetect > timeToDetect)
        {
            playerInfiltration.Reperated.Invoke();
        }
    }

    private void changeConeColor()
    {
        VisionConeMaterial.color = new Color(1, (255f-(timerDetect/timeToDetect)*255f)/255f, 0f, 166f/255f);
    }

}
