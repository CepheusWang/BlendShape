using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BlendShapeMouthItemData", menuName = "FacialExpression/BlendShapeMouthItemData")]
public class BlendShapeMouthItemData : ScriptableObject
{
    [Serializable]
    public class MouthBlendShapeEntry
    {
        public BlendShapeID BlendShape;
        public float value;
        public float BlendStrength = 10f; 
    }

    public string Destrition;
    public List<MouthBlendShapeEntry> BlendShapes = new List<MouthBlendShapeEntry>();
    public float mDuration = 0.1f;

}
