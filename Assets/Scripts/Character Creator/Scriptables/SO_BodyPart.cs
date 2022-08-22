using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Body Part", menuName = "Body Part")]
public class SO_BodyPart : ScriptableObject
{
    public string bodyPartName;
    public int bodyPartId;

    public List<AnimationClip> allBodyPartsAnimations = new List<AnimationClip>();
}
