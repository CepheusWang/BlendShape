using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 脸部表情入口
/// </summary>
public class BlendShapeEntry
{
    public float Time;
    private BlendDataItemData mData;
    private SkinnedMeshRenderer mSkinnedMeshRenderer;
    private int mBlendShapeIndex;
    private float mBlendShapeWeight;
    public void OnInitComponent(Transform root)
    {
        Transform t = root.Find(Data.mSkinnerRenderName);
        if (t != null)
        {
            mSkinnedMeshRenderer = t.GetComponent<SkinnedMeshRenderer>();
            if (mSkinnedMeshRenderer != null)
                mBlendShapeIndex = mSkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(Data.mBlendShapeNodeName);
        }
        InitWeight();
    }

    private BlendDataItemData Data
    {
        get { return mData; }
        set
        {
            mData = value;
        }
    }

    public void SetBlendShapeData(BlendDataItemData data)
    {
        Data = data;
    }

    private void InitWeight()
    {
        mBlendShapeWeight = Data.mMinWeightValue;
    }

    private void SetBlendShapeWeigth(float _weight)
    {
        mSkinnedMeshRenderer.SetBlendShapeWeight(mBlendShapeIndex, _weight);
    }
    public void OnPlay()
    {
        if(Data!=null)
        {
            mBlendShapeWeight = Mathf.Lerp(mBlendShapeWeight, Data.mMaxWeightValue, Data.mBlendShapeLerpWeightValue * UnityEngine.Time.deltaTime);
            SetBlendShapeWeigth(mBlendShapeWeight);
            Time += UnityEngine.Time.deltaTime;

        }
    }

    public void OnReverse()
    {
        if (Data != null)
        {
            mBlendShapeWeight = Mathf.Lerp(mBlendShapeWeight, 0, Data.mBlendShapeLerpWeightValue * UnityEngine.Time.deltaTime);
            SetBlendShapeWeigth(mBlendShapeWeight);
        }
    }

    public bool Finish()
    {
        if (Data != null)
        {
            if (Time < Data.mDuration)
                return false;
        }
        Time = 0;
        return true;
    }


    public bool Reverst()
    {

        if (Data != null)
        {
            if (Time < Data.mDuration)
                return false;
        }

        return true;
    }
}

/// <summary>
/// 表情数据管理中心
/// 管理表情切换，播放，以及回溯idle表情
/// </summary>
[ExecuteInEditMode]
[RequireComponent(typeof(BlendShapeMixPlay))]
public class BlendShapeController : MonoBehaviour
{

    /// <summary>
    /// 一组表情最大配置数
    /// </summary>
    private static readonly int m_iBlendShapeDataMax = 5;

    private BlendShapeMixPlay mBlendShapePlay;

    /// <summary>
    /// 数据播放池子
    /// </summary>
    private Stack<BlendShapeEntry> mDataPool = new Stack<BlendShapeEntry>(m_iBlendShapeDataMax);


    private void Awake()
    {
        IniMonoCompent();
        InitPool();
    }

    private void Update()
    {

    }



    private void InitPool()
    {
        for (int i = 0; i < m_iBlendShapeDataMax; ++i)
        {
            mDataPool.Push(new BlendShapeEntry());
        }
    }

    private void IniMonoCompent()
    {
        if (!mBlendShapePlay) mBlendShapePlay = GetComponent<BlendShapeMixPlay>();
    }

    private BlendShapeEntry GetEntry(BlendDataItemData data)
    {
        BlendShapeEntry entry = mDataPool.Count > 0 ? mDataPool.Pop() : new BlendShapeEntry();
        entry.Time = 0;
        //entry.Data = data;
        return entry;
    }

    public void SetBlendShapeData(FacialExpressionData data)
    {
        if (data != null)
        {
            for (int i = 0; i < data.m_kBlendShapeDatas.Count; i++)
            {

            }
        }
        else
        {
            Debug.LogError("This Face Blendshape data is null. Please Chect it");
        }
    }


}
