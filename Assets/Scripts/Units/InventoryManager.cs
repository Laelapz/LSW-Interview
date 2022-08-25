using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _grid;
    [SerializeField] private GameObject _gridSlot;

    public Image bodyPartIcon;
    public List<BodyPartsSelector.BodyPartSelection> inventoryParts;

    public void SetInventory()
    {
        for(int i  = 0; i < inventoryParts.Count; i++)
        {
            for(int j = 0; j < inventoryParts[i].bodyPartOptions.Length; j++)
            {
                var gridInstance = Instantiate(_gridSlot, _grid.transform);
                gridInstance.GetComponentsInChildren<Image>()[1].sprite = inventoryParts[i].bodyPartOptions[j].icon;
            }
        }
    }

    public void FreeInventory()
    {
        int childs = _grid.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(_grid.transform.GetChild(i).gameObject);
        }
    }

    public void OpenInventory()
    {
        SetInventory();
        _inventoryPanel.SetActive(true);
    }

    public void CloseInventory()
    {
        _inventoryPanel.SetActive(false);
        FreeInventory();
    }
}
