using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 表情数据结构
/// </summary>
[CreateAssetMenu(fileName = "FacialExpressionData", menuName = "FacialExpression/FacialExpressionData")]
public class FacialExpressionData : ScriptableObject
{
    public string m_kEnmojeName;
    public List<BlendDataItemData> m_kBlendShapeDatas;
    private bool isFinish;
    public string Destriction;

    public bool Finish
    {
        get { return isFinish; }
    }


}
