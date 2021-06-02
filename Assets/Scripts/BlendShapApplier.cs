using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BlendShape 解析器
/// </summary>
public class BlendShapApplier : MonoBehaviour
{
    public float mRecoverSmoothness;

    private Dictionary<BlendShapeID, float> mApplyData = new Dictionary<BlendShapeID, float>();
    private HashSet<BlendShapeID> mModifiedIds = new HashSet<BlendShapeID>();

    private void Update()
    {
        Apply();
    }

    public void ApplyBlendData(MouthAudioData data, float time)
    {
        for (int i = 0; i < data.mMouthAudioFrame.Count; ++i)
        {
            MouthAudioKeyFrame keyframe = data.mMouthAudioFrame[i];
            float keyframeDuration = Mathf.Max(keyframe.mData.mDuration, keyframe.mTime);
            if (time > keyframe.mSTime && time < (keyframe.mSTime + keyframeDuration))
            {
                ApplyBlendSet(keyframe.mData);
            }
        }
    }

    public void ApplyBlendSet(BlendShapeMouthItemData data)
    {
        for (int i = 0; i < data.BlendShapes.Count; ++i)
        {
            if (!mApplyData.ContainsKey(data.BlendShapes[i].BlendShape))
                mApplyData.Add(data.BlendShapes[i].BlendShape, 0f);

            mApplyData[data.BlendShapes[i].BlendShape] = Mathf.Lerp(mApplyData[data.BlendShapes[i].BlendShape], data.BlendShapes[i].value, data.BlendShapes[i].BlendStrength * Time.deltaTime);
        }
    }


    private void Apply()
    {
        if (mApplyData != null && mApplyData.Count > 0)
        {
            foreach (var data in mApplyData)
            {
                ApplyBlendShape(data.Key, data.Value);
            }
        }


        ApplyBlendShapeDrag();
    }

    private SkinnedMeshRenderer mMesh;
    public void ApplyBlendShape(BlendShapeID id, float value)
    {
        if (!mMesh)
             mMesh = GetComponent<SkinnedMeshRenderer>();

        int index = mMesh.sharedMesh.GetBlendShapeIndex(id.mBlendShapeNodeName);
        mMesh.SetBlendShapeWeight(index, value);
        mModifiedIds.Add(id);
    }
    private void ApplyBlendShapeDrag()
    {
        if(mModifiedIds!=null && mModifiedIds.Count>0)
        {
            foreach (var id in mModifiedIds)
            {
                mApplyData[id] = Mathf.Lerp(mApplyData[id], 0, mRecoverSmoothness * Time.deltaTime);
            }
        }

    }

    public void RemoveBlendShape(MouthAudioData data)
    {
        if (data != null)
        {
            for(int i = 0;i<data.mMouthAudioFrame.Count;i++)
            {
               for(int j =0;j<data.mMouthAudioFrame[i].mData.BlendShapes.Count;j++)
                {
                     if(mApplyData.ContainsKey(data.mMouthAudioFrame[i].mData.BlendShapes[j].BlendShape))
                    {
                        ApplyBlendShape(data.mMouthAudioFrame[i].mData.BlendShapes[j].BlendShape,0);
                        mApplyData.Remove(data.mMouthAudioFrame[i].mData.BlendShapes[j].BlendShape);
                        mModifiedIds.Remove(data.mMouthAudioFrame[i].mData.BlendShapes[j].BlendShape);
                    }
                }
            }
        }

    }
}
