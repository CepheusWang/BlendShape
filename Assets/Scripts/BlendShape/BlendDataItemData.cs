using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlendDataItemData", menuName = "FacialExpression/BlendDataItemData")]
public class BlendDataItemData : ScriptableObject
{
    public string mSkinnerRenderName;
    public string mBlendShapeNodeName;
    public float mMinWeightValue;
    public float mMaxWeightValue;
    public float mBlendShapeLerpWeightValue;
    public float mDuration;
    private bool mFinish;
}
