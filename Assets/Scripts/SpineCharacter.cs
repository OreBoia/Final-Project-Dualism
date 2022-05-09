using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using System;

public class SpineCharacter : MonoBehaviour
{
    SkeletonAnimation spineAnim;
    void Start()
    {
        spineAnim = GetComponent<SkeletonAnimation>();
        //spineAnim.state.Complete += AnimComplete;
        
    }

    void Update()
    {
        //spineAnim.state.SetAnimation(0, "run", true);
        
        //spineAnim.AnimationState.Event += AnimEvent;
        //spineAnim.initialFlipX = true;
        //spineAnim.Skeleton.ScaleX = -1;
        //spineAnim.Skeleton.SetSkin("goblingirl");
    }

    private void AnimEvent(TrackEntry trackEntry, Spine.Event e)
    {
        Debug.Log("EVENT");
    }

    private void AnimComplete(TrackEntry trackEntry)
    {
        Debug.Log("Complete");
    }
}
