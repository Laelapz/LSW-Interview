using UnityEngine;

[CreateAssetMenu(fileName = "New Character Body", menuName = "Character Body")]
public class SO_CharacterBody : ScriptableObject
{
    public BodyPart[] characterBodyParts;

    public void Init(SO_BodyPart bodyPart, SO_BodyPart topPart, SO_BodyPart bottomPart)
    {
        characterBodyParts = new BodyPart[3];
        characterBodyParts[0] = new BodyPart();
        characterBodyParts[1] = new BodyPart();
        characterBodyParts[2] = new BodyPart();

        characterBodyParts[0].bodyPartName = "Body";
        characterBodyParts[0].bodyPart = bodyPart;

        characterBodyParts[1].bodyPartName = "Top";
        characterBodyParts[1].bodyPart = topPart;

        characterBodyParts[2].bodyPartName = "Bottom";
        characterBodyParts[2].bodyPart = bottomPart;
    }
}

[System.Serializable]
public class BodyPart
{
    public string bodyPartName;
    public SO_BodyPart bodyPart;
}