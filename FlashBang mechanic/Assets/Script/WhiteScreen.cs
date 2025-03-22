using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteScreen : MonoBehaviour
{
    [SerializeField]
    private Image img;
    private Animator animator;

    public static WhiteScreen activeInstance;

    // Start is called before the first frame update
    void Start()
    {
        activeInstance = this;

        animator = GetComponent<Animator>();
    }

    public void FlashEffect()
    {
        animator.SetTrigger("Go Blind");
    }



}
