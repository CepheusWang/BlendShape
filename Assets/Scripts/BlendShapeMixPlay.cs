using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeMixPlay : MonoBehaviour
{



    private BlendShapeController mBlendShapeController;
    [SerializeField]
    public GameObject m_kBlendShapeMeshRoot;
    [SerializeField]
    public FacialExpressionData[] mFacialExpressionData;

    private FacialExpressionData mPlayFaceEData;

    public List<BlendShapeEntry> mBlendShapeEntrys ;
    private string mCurrentFaceName = string.Empty;

    private void Awake()
    {
        mBlendShapeEntrys = new List<BlendShapeEntry>();
    }
    private void Update()
    {
       if(mBlendShapeEntrys != null&& mBlendShapeEntrys.Count>0)
        {
            for(int i = 0;i< mBlendShapeEntrys.Count;++i)
            {
                if (!mBlendShapeEntrys[i].Finish())
                {
                    mBlendShapeEntrys[i].OnPlay();
                }
                else
                {
                    mBlendShapeEntrys[i].OnReverse();
                }
            }
        }
    }


    public void PlayHappy()
    {
        FacialExpressionData data = GetFaceEmojData("Smile_01");
        SetBlendShapeEntry(data);
    }

    public void PlayAngry()
    {
        FacialExpressionData data = GetFaceEmojData("Angry_01");
        SetBlendShapeEntry(data);
    }

    public void PlayTalk()
    {
        FacialExpressionData data = GetFaceEmojData("Talk");
        SetBlendShapeEntry(data);
    }

    private FacialExpressionData GetFaceEmojData(string faceEmojName)
    {
        if(!mCurrentFaceName.Equals(faceEmojName))
        {
            mBlendShapeEntrys.Clear();
        }
        if(mFacialExpressionData != null && mFacialExpressionData.Length>0)
        {
            for(int i = 0;i< mFacialExpressionData.Length;i++)
            {
                if (mFacialExpressionData[i].m_kEnmojeName == faceEmojName)
                    return mFacialExpressionData[i];
            }
        }

        return null;
    }

    
    private void SetBlendShapeEntry(FacialExpressionData data)
    {
        if(data!=null)
        {
            if(data.m_kBlendShapeDatas!=null)
            {
                for(int i = 0;i<data.m_kBlendShapeDatas.Count;i++)
                {
                    BlendShapeEntry entry = new BlendShapeEntry();
                    InitEntry(entry, data.m_kBlendShapeDatas[i]);
                    mBlendShapeEntrys.Add(entry);
                }
            }
        }
    }
    
    private void InitEntry(BlendShapeEntry entry,BlendDataItemData data)
    {
        entry.Time = 0;
        entry.SetBlendShapeData(data);
        entry.OnInitComponent(m_kBlendShapeMeshRoot.transform);
    }
}
