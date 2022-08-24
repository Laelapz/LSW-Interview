using UnityEngine;

[CreateAssetMenu(fileName = "New Character Body", menuName = "Character Body")]
public class SO_CharacterBody : ScriptableObject
{
    public BodyPart[] characterBodyParts;

    public void Init(SO_BodyPart hairPart, SO_BodyPart bodyPart, SO_BodyPart topPart, SO_BodyPart bottomPart)
    {
        characterBodyParts = new BodyPart[4];
        characterBodyParts[0] = new BodyPart();
        characterBodyParts[1] = new BodyPart();
        characterBodyParts[2] = new BodyPart();
        characterBodyParts[3] = new BodyPart();

        characterBodyParts[0].bodyPartName = "Hair";
        characterBodyParts[0].bodyPart = hairPart;

        characterBodyParts[1].bodyPartName = "Body";
        characterBodyParts[1].bodyPart = bodyPart;


        characterBodyParts[2].bodyPartName = "Top";
        characterBodyParts[2].bodyPart = topPart;

        characterBodyParts[3].bodyPartName = "Bottom";
        characterBodyParts[3].bodyPart = bottomPart;
    }
}

[System.Serializable]
public class BodyPart
{
    public string bodyPartName;
    public SO_BodyPart bodyPart;
}