using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Body Part", menuName = "Body Part")]
public class SO_BodyPart : ScriptableObject
{
    public string bodyPartName;
    public int bodyPartId;
    public Sprite icon;

    public List<AnimationClip> allBodyPartsAnimations = new List<AnimationClip>();
}
