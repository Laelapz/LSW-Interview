using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _clothesPanel;
    [SerializeField] private BodyPartsManager _bodyPartsManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] TMP_Text[] _bodyOptionsText;

    public SO_CharacterBody characterBody;
    public List<BodyPartsSelector.BodyPartSelection> inventoryParts;

    private void Start()
    {
        _bodyPartsManager.Start();
    }

    public void SetupMenu()
    {
        if (!_clothesPanel.activeSelf)
        {
            _clothesPanel.SetActive(true);
            var bodyPartsSelector = _clothesPanel.GetComponent<BodyPartsSelector>()._bodyPartSelections = _gameManager.inventoryParts;
            
            bodyPartsSelector[0].bodyPartNameTextComponent = _bodyOptionsText[0];
            bodyPartsSelector[1].bodyPartNameTextComponent = _bodyOptionsText[1];
            bodyPartsSelector[2].bodyPartNameTextComponent = _bodyOptionsText[2];
            bodyPartsSelector[3].bodyPartNameTextComponent = _bodyOptionsText[3];
        }
        else
        {
            QuitMenu();
        }
    }

    public void QuitMenu()
    {
        _clothesPanel.SetActive(false);
    }
}
