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
    public AudioSource flashbangAudioSource;
    public List<AudioClip> flashbangSounds;

    [SerializeField]
    private bool isAiming = false;
    [SerializeField]
    private bool isEquiped = false;
    private int currentEffects;



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
            if (isEquiped)
            {
                flashbangAudioSource.clip = flashbangSounds[0];
                flashbangAudioSource.Play();
            }

        }
        if (Input.GetMouseButton(0) && isEquiped) // hold down right mouse button to aim
        {
            isAiming = true;

        }



        if (Input.GetMouseButtonUp(0) && isAiming && isEquiped) //release right mouse button to throw
        {
            ThrowBlind();
            isAiming = false;
            isEquiped = false;
            flashbangAudioSource.clip = flashbangSounds[1];
            flashbangAudioSource.Play();

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
}
