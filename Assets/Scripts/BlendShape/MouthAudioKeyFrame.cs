using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MouthAudioKeyFrame
{
    /// <summary>
    /// 持续时间
    /// </summary>
    public float mTime;
    /// <summary>
    /// 开始时间
    /// </summary>
    public float mSTime;
    public string mVoiceKeyText;
    private BlendShapeMouthItemData mData;
    public MouthBlendShapeType mMouthType;

    public BlendShapeMouthItemData MouthItemData
    {
        get { return mData; }
        set { mData = value; }
    }
}
