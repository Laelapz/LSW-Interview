using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BodyPartsSelector : MonoBehaviour
{
    [SerializeField] private SO_CharacterBody characterBody;
    [SerializeField] private BodyPartSelection[] bodyPartSelections;

    void Start()
    {
        for(int i = 0; i < bodyPartSelections.Length; i++)
        {
            GetCurrentBodyPart(i);
        }
    }

    private void GetCurrentBodyPart(int partIndex)
    {
        bodyPartSelections[partIndex].bodyPartNameTextComponent.text = characterBody.characterBodyParts[partIndex].bodyPart.bodyPartName;
        bodyPartSelections[partIndex].bodyPartCurrentIndex = characterBody.characterBodyParts[partIndex].bodyPart.bodyPartId;
    }

    public void NextBodyPart(int partIndex)
    {
        if (ValidateIndexValue(partIndex))
        {
            if (bodyPartSelections[partIndex].bodyPartCurrentIndex < bodyPartSelections[partIndex].bodyPartOptions.Length - 1)
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex++;
            }
            else
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex = 0;
            }

            UpdateCurrentParts(partIndex);
        }
    }

    public void PreviousBodyPart(int partIndex)
    {
        if (ValidateIndexValue(partIndex))
        {
            if (bodyPartSelections[partIndex].bodyPartCurrentIndex > 0)
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex--;
            }
            else
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex = bodyPartSelections[partIndex].bodyPartOptions.Length - 1;
            }

            UpdateCurrentParts(partIndex);
        }
    }

    public void UpdateCurrentParts(int partIndex)
    {
        bodyPartSelections[partIndex].bodyPartNameTextComponent.text = bodyPartSelections[partIndex].bodyPartOptions[bodyPartSelections[partIndex].bodyPartCurrentIndex].bodyPartName;
    }

    private bool ValidateIndexValue(int partIndex)
    {
        if (partIndex > bodyPartSelections.Length || partIndex < 0)
        {
            Debug.Log("Index value does not match any body parts!");
            return false;
        }
        else
        {
            return true;
        }
    }

        
    [System.Serializable]
    public class BodyPartSelection
    {
        public string bodyPartName;
        public SO_BodyPart[] bodyPartOptions;
        public TMP_Text bodyPartNameTextComponent;
        [HideInInspector] public int bodyPartCurrentIndex;
    }
}
