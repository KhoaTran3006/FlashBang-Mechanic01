using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerBlinded : MonoBehaviour
{
    [SerializeField]
    private Image img;

    private Animator animator;

    private int width, height;
    public static PlayerBlinded activeInstance;
    public AudioSource audioSource;

    void Start()
    {
        activeInstance = this;

        animator = GetComponent<Animator>();
        width = Screen.width;
        height = Screen.height;
    }

    public void GoBlind()
    {
        //FlashGrenade explode script trigger this
        StartCoroutine(goBlind());
    }

    private IEnumerator goBlind()
    {
        audioSource.Play();
        yield return new WaitForEndOfFrame();
        //Trigger aniamtor
        animator.SetTrigger("Go Blind");

        //Capture the Cam Image before WhiteScreen and re-play it on the Canvas for the AfterImage effect
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        img.sprite = Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100);

        WhiteScreen.activeInstance.FlashEffect();
    }

}
