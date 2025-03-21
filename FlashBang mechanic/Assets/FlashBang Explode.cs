using Hertzole.GoldPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBangExplode : MonoBehaviour
{
    public float fuseTime = 2f;
    public float explosionRadius = 10f;
    public float blindDuration = 5f;
    public GameObject explosionEffect;

    private bool isExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", fuseTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Explode()
    {
        if (isExploded) return; //prevent multiple explosions
        isExploded = true;

        //Explosion effect
        /* if (explosionEffect =! null) { Instantiate(explosionEffect, transform.position, quaternion.identity*/

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
                    PlayerBlinded blindEffect = collider.GetComponent<PlayerBlinded>();

                    //if the player has the script trigger the ffect
                    if (blindEffect != null)
                    {
                        blindEffect.TriggerBlindEffect(blindDuration);
                    }
                }
            }
        }
        //Destroy after exploded
        Destroy(gameObject, 1f);
    }

    private bool PlayerIsLooking(Transform player)
    {
        Camera playerCam = player.GetComponentInChildren<Camera>();

        if (playerCam == null)
        {
            return false; //No Camera found, assumethey are not looking
        }

        Vector3 cameraForward = playerCam.transform.forward;
        Vector3 toFlashbang = (transform.position - playerCam.transform.position).normalized; // get the direction vector form the camera to the flash bang

        float dot = Vector3.Dot(cameraForward, toFlashbang);

        //chec if the player is looking at the flash bang
        if (dot > 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCam.transform.position, toFlashbang, out hit, explosionRadius))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    return true;// the player is looking with no object in between
                }
            }
        }
        return false; //the player is not looking at the flash bang or being blocked
    }
}
