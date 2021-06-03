using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialShapeApplier : MonoBehaviour
{
    public Animator mFacialShapeAnimator;

    void Awake()
    {
        if (!mFacialShapeAnimator)
            mFacialShapeAnimator = transform.parent.GetComponent<Animator>();
    }


    void OnEnable()
    {

    }

    public void PlayCryShape()
    {
        if (mFacialShapeAnimator)
            mFacialShapeAnimator.SetTrigger("Cry");
    }

    public void PlaySmile()
    {
        if (mFacialShapeAnimator)
            mFacialShapeAnimator.SetTrigger("Smile");
    }
}
