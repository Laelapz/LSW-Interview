using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BodyPartsSelector : MonoBehaviour
{
    [SerializeField] public SO_CharacterBody _characterBody;
    public List<BodyPartSelection> _bodyPartSelections;

    void Start()
    {
        for(int i = 0; i < _bodyPartSelections.Count; i++)
        {
            GetCurrentBodyPart(i);
        }
    }

    private void GetCurrentBodyPart(int partIndex)
    {
        _bodyPartSelections[partIndex].bodyPartNameTextComponent.text = _characterBody.characterBodyParts[partIndex].bodyPart.bodyPartName;
        _bodyPartSelections[partIndex].bodyPartCurrentIndex = _characterBody.characterBodyParts[partIndex].bodyPart.bodyPartId;
    }

    public void NextBodyPart(int partIndex)
    {
        if (ValidateIndexValue(partIndex))
        {
            if (_bodyPartSelections[partIndex].bodyPartCurrentIndex < _bodyPartSelections[partIndex].bodyPartOptions.Length - 1)
            {
                _bodyPartSelections[partIndex].bodyPartCurrentIndex++;
            }
            else
            {
                _bodyPartSelections[partIndex].bodyPartCurrentIndex = 0;
            }

            UpdateCurrentParts(partIndex);
        }
    }

    public void PreviousBodyPart(int partIndex)
    {
        if (ValidateIndexValue(partIndex))
        {
            if (_bodyPartSelections[partIndex].bodyPartCurrentIndex > 0)
            {
                _bodyPartSelections[partIndex].bodyPartCurrentIndex--;
            }
            else
            {
                _bodyPartSelections[partIndex].bodyPartCurrentIndex = _bodyPartSelections[partIndex].bodyPartOptions.Length - 1;
            }

            UpdateCurrentParts(partIndex);
        }
    }

    public void UpdateCurrentParts(int partIndex)
    {
        _bodyPartSelections[partIndex].bodyPartNameTextComponent.text = _bodyPartSelections[partIndex].bodyPartOptions[_bodyPartSelections[partIndex].bodyPartCurrentIndex].bodyPartName;
        _characterBody.characterBodyParts[partIndex].bodyPart = _bodyPartSelections[partIndex].bodyPartOptions[_bodyPartSelections[partIndex].bodyPartCurrentIndex];
    }

    private bool ValidateIndexValue(int partIndex)
    {
        if (partIndex > _bodyPartSelections.Count || partIndex < 0)
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
