using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject _shopPanel;

    public int[] upgradeCosts = new int[3];
    [SerializeField] private TMP_Text[] updateTexts;
    [SerializeField] private Button[] updateButtons;

    private void Start()
    {
        upgradeCosts[0] = 10;
        upgradeCosts[1] = 10;
        upgradeCosts[2] = 10;
    }

    public void OpenShopMenu()
    {
        _shopPanel.SetActive(true);
    }

    public void CloseShopMenu()
    {
        _shopPanel.SetActive(false);
    }

    public void ClothesUpgradePrice()
    {
        upgradeCosts[0] *= 2;
        updateTexts[0].text = "S " + upgradeCosts[0].ToString();
    }

    public void ShopCapacityUpgradePrice()
    {
        upgradeCosts[1] *= 2;
        updateTexts[1].text = "S " + upgradeCosts[0].ToString();
    }

    public void SpawnSpeedUpgradePrice()
    {
        upgradeCosts[2] *= 2;
        updateTexts[2].text = "S " + upgradeCosts[0].ToString();
    }

    public void DeactivateClothesButton()
    {
        updateButtons[0].interactable = false;
    }

    public void DeactivateCapacityButton()
    {
        updateButtons[1].interactable = false;
    }

    public void DeactivateSpeedButton()
    {
        updateButtons[2].interactable = false;
    }
}
