using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class FlashBangThrowing : MonoBehaviour
{
    public GameObject flashbangPrefab;
    public Transform throwPoint;
    public float throwingForce;

    //On Hand Equip
    public GameObject flashbangPos;
    public Transform fpsCam, player;
    public Animator animator;
    //public Camera cam;

    //[Header("Display Control")]
    //[SerializeField]
    //[Range(10f, 100f)]
    //private int LinePoints = 25;
    //[SerializeField]
    //[Range(0.01f, 0.25f)]
    //private float TimeBetweenPoints = 0.1f;


    //[SerializeField] private LineRenderer lineRenderer;
    [SerializeField]
    private bool isAiming = false;
    [SerializeField]
    private bool isEquiped = false;




    // Start is called before the first frame update
    void Start()
    {
        flashbangPos.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E)) //press E to toggle equip/unequip
        {
            isEquiped = !isEquiped;
        }
        if (Input.GetMouseButton(0) && isEquiped) // hold down right mouse button to aim
        {
            isAiming = true;
            //DrawProjection(); //draw projection when aiming
        }
        /*else
        {
            lineRenderer.enabled = false;
        }*/


        if (Input.GetMouseButtonUp(0) && isAiming && isEquiped) //release right mouse button to throw
        {
            ThrowBlind();
            isAiming = false;
            isEquiped = false;

        }

        //Equip Flashbang on Hand
        if (isEquiped == true)
        {
            flashbangPos.SetActive(true);
            animator.SetBool("onHand", true);
        }
        else if (isEquiped == false)
        {
            flashbangPos.SetActive(false);
        }
        if (isAiming == true)
        {
            animator.SetBool("aboutToThrow", true);
        }

    }

    private void ThrowBlind()
    {
        GameObject fb = Instantiate(flashbangPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody rb = fb.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(throwPoint.forward * throwingForce, ForceMode.VelocityChange);
        }
    }

    /*private void DrawProjection()
    {
        float mass = flashbangPrefab.GetComponent<Rigidbody>().mass; // get the flash grenade mass
        //we will implement kinematic equation d = V0*t + 1/2*a*t^2
        Debug.Log(mass);
        lineRenderer.enabled = true;
        lineRenderer.positionCount = Mathf.CeilToInt(LinePoints / TimeBetweenPoints) + 1;
        Vector3 startPosition = throwPoint.position;
        Vector3 startVelocity = throwingForce * cam.transform.forward / mass;

        int i = 0;
        for (float time = 0f; time < LinePoints; time += TimeBetweenPoints)
        {
            i++;
            Vector3 point = startPosition + time * startVelocity;
            //now implement the equation
            point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time); //new point.y = startPosition.y, V0 =startVelocity.y, t = time, a = Physics.gravity, t^2 = time *time

            lineRenderer.SetPosition(i, point);
        }
    }*/
}
