using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimationManager 
{
    [SerializeField] protected Animator animator;
    [SerializeField] string curAnim;

    public void Play(string newAnim)
    {
        if (curAnim == newAnim)
            return;

        curAnim = newAnim;
        animator.Play(curAnim);
    }

    public void SetAnimator(Animator _animator)
    {
        animator = _animator;
    }
}
