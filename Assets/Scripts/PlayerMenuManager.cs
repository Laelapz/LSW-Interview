using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _clothesPanel;
    [SerializeField] private BodyPartsManager _bodyPartsManager; 
    
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
            _clothesPanel.GetComponent<BodyPartsSelector>()._bodyPartSelections = inventoryParts;
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
