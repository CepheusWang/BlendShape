using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 嘴型发音口型匹配
/// </summary>
public class MouthShapeApplier : MonoBehaviour
{
    private static readonly int sDefaultDataSize = 5;


    public Transform root;

    public MouthAudioData mMouthAudioData;
    public AudioSource mAudioSource;
    public BlendShapApplier mBpApplier;



    private List<DataEntry> mDataToApply = new List<DataEntry>(sDefaultDataSize);
    private Stack<DataEntry> mDataPool = new Stack<DataEntry>(sDefaultDataSize);


    public struct DataEntry
    {
        public float Time;
        public MouthAudioData Data;
        public float Duration;

        public bool HasFinished { get { return Time > Duration; } }
    }



    public Text m_keyText;
    public float mPlayTime = 0;
    private void Awake()
    {
        if (!mBpApplier)
            mBpApplier = GetComponent<BlendShapApplier>();
        if (!mAudioSource)
            mAudioSource = GetComponent<AudioSource>();

        for (int i = 0; i < sDefaultDataSize; ++i)
        {
            mDataPool.Push(new DataEntry());
        }
    }

    public void TalkTest()
    {
        if (mMouthAudioData == null || mMouthAudioData.Sound == null)
        {
            Debug.LogError("This MouthAudio  is nil,Please Check it and Write It in config");
            return;
        }
        DataEntry entry = GetDataEntry(mMouthAudioData);
        mDataToApply.Add(entry);
        if(mMouthAudioData.Sound)
        {
            mAudioSource.PlayOneShot(mMouthAudioData.Sound);
        }
    }

    private DataEntry GetDataEntry(MouthAudioData _data )
    {
        DataEntry entry = mDataPool.Count > 0 ? mDataPool.Pop() : new DataEntry();
        entry.Time = 0f;
        entry.Data = _data;
        entry.Duration = _data.GetDuration();

        return entry;
    }



    private void Update()
    {
        if(mDataToApply!=null && mDataToApply.Count>0)
        {
            for(int i = 0;i<mDataToApply.Count;++i)
            {
                DataEntry entry = mDataToApply[i];
                entry.Time += Time.deltaTime;
                mBpApplier.ApplyBlendData(entry.Data, entry.Time);
                if (entry.HasFinished)
                {
                    RemoveEntry(i);
                    i--;
                }
                else
                {
                    mDataToApply[i] = entry;
                    mPlayTime += Time.deltaTime;
                }
            }
        }
    }
    private void RemoveEntry(int index)
    {
        DataEntry entry = mDataToApply[index];
        mDataPool.Push(entry);
        mBpApplier.RemoveBlendShape(entry.Data);
        mDataToApply.RemoveAt(index);
    }
}
