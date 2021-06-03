using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MouthAudioData", menuName = "FacialExpression/MouthAudioData")]
public class MouthAudioData : ScriptableObject
{
    public float mTime;
    public AudioClip Sound;
    public string ReferenceText;

    public List<MouthAudioKeyFrame> mMouthAudioFrame = new List<MouthAudioKeyFrame>();
  
    public MouthAudioData(float time)
    {
        mTime = time;
    }


    public float GetDuration()
    {
        float duration = Sound ? Sound.length : 0f;

        for(int i = 0;i<mMouthAudioFrame.Count;i++)
        {
            MouthAudioKeyFrame frame = mMouthAudioFrame[i];
            if (frame.MouthItemData)
            {
                duration = Mathf.Max(frame.mTime+frame.MouthItemData.mDuration,duration);
            }
        }

        return duration;
    }

   
}
