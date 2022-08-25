using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    private int index = 0;
    [SerializeField] private GameObject _inventoryPanel;
    public Image bodyPartIcon;
    public List<BodyPartsSelector.BodyPartSelection> inventoryParts;

    public void OpenInventory()
    {
        bodyPartIcon.sprite = inventoryParts[0].bodyPartOptions[index].icon;
        index++;
        _inventoryPanel.SetActive(true);
    }

    public void CloseInventory()
    {
        _inventoryPanel.SetActive(false);
    }
}
