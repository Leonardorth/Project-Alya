using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioClip footStepClip;

    public AudioClip jumpUp;
    public AudioClip landJump;

    private AudioSource audioSource;
    private Animator animator;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void Step()
    {
        if (animator.GetFloat("speedPercent") > 0.05f && animator.GetFloat("speedPercent") < 0.6f)
        {
            audioSource.PlayOneShot(footStepClip);
        }
    }

    private void StepRun()
    {
        if (animator.GetFloat("speedPercent") >= 0.6f)
        {
            audioSource.PlayOneShot(footStepClip);
        }
    }

    private void JumpUp()
    {
        if (animator.GetBool("isJumping") == true)
        {
            audioSource.PlayOneShot(jumpUp);
        }
    }

    private void LandJump()
    {
        if (animator.GetBool("isJumping") == false)
        {
            audioSource.PlayOneShot(landJump);
        }
    }

    
}
