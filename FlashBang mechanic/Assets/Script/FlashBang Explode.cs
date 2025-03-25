using Hertzole.GoldPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBangExplode : MonoBehaviour
{
    public float fuseTime = 2f;
    public GameObject explosionEffect;
    public AudioSource explosionSound;
    public float explosionRadius = 20f;
    public float blindDuration = 5f;
    

    private Camera cam;

    private bool isExploded = false;




    // Start is called before the first frame update
    void Start()
    {

        Invoke("Explode", fuseTime);
        cam = GameObject.Find("Player Camera").GetComponent<Camera>();
    }

    private void Explode()
    {
        explosionSound.Play();
        //Destroy after exploded
        Destroy(gameObject, 1f);

        //Explosion effect
        Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity), 5);


        if (isExploded) return; //prevent multiple explosions
        isExploded = true;

        //Detect all collider in the explosion range
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, explosionRadius);
        //run loop to check all the collider which one is the player
        foreach (Collider collider in hitCollider)
        {
            //check which one is tagged under Player
            if (collider.CompareTag("Player"))
            {
                //Get the player transform
                Transform playerTransform = collider.transform;

                //check if the player is looking at the flash bang
                if (PlayerIsLooking(playerTransform))
                {
                    //pull the blind effect script in the player
                    
                    PlayerBlinded.activeInstance.GoBlind();

                }
            }
        }

    }

    private bool PlayerIsLooking(Transform player)
    {
        Camera playerCam = player.GetComponentInChildren<Camera>();

        if (playerCam == null)
        {
            return false; //No Camera found, assume they are not looking
        }

        Vector3 cameraForward = playerCam.transform.forward;
        Vector3 toFlashbang = (transform.position - playerCam.transform.position).normalized; // get the direction vector from the camera to the flash bang
        //calculate the angle between 2 vector to determine is the flash bang is in the field of view of the player
        float dot = Vector3.Dot(cameraForward, toFlashbang);

        //check if the player is looking at the flash bang
        if (dot > 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCam.transform.position, toFlashbang, out hit, explosionRadius))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    //PlayerBlinded.activeInstance.GoBlind();
                    Debug.Log("isBlinded");
                    return true;// the player is looking with no object in between
                }
            }
        }
        Debug.Log("notBlinded");
        return false; //the player is not looking at the flash bang or being blocked
    }

}
