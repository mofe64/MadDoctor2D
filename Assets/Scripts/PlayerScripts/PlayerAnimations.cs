using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private Vector3 temporaryScale;
    private int currentAnimation;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayAnimation(string animationName)
    {
        if (currentAnimation == Animator.StringToHash(animationName)) { return; }
        animator.Play(animationName);
        currentAnimation = Animator.StringToHash(animationName);
    }

    public void setFacingDirection(bool faceRight)
    {
        //note using scale to change the obj orientation affects the gameobj as well as children game obj, using flipx does not
        temporaryScale = transform.localScale;
        if (faceRight)
        {
            temporaryScale.x = 1f;
        }
        else
        {
            temporaryScale.x = -1f;
        }
        transform.localScale = temporaryScale;
    }
}
