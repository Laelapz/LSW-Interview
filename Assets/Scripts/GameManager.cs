using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;

public class GameManager : MonoBehaviour
{

    private bool _isOpened = false;
    private bool _canSpawnNpc = true;
    public int actualMoney = 0;
    public int clothesPrice = 10;
    public int actualNpcNumber = 0;
    public int maxNpcCapacity = 5;

    public float instantiateCooldown = 10f;

    [SerializeField] TMP_Text _isOpenedText;
    [SerializeField] TMP_Text _moneyValueText;
    [SerializeField] GameObject _npcs;
    [SerializeField] Transform[] _path;
    [SerializeField] ShopManager _shopManager;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] Image _musicIcon;
    [SerializeField] Sprite[] _musicOnAndOff;

    public List<BodyPartsSelector.BodyPartSelection> inventoryParts;

    void Update()
    {
        InstantiateNpc();
    }

    public void OpenCloseShop()
    {
        if (!_isOpened)
        {
            _isOpened = true;
            _isOpenedText.text = "Close Shop";
        }
        else
        {
            _isOpened = false;
            _isOpenedText.text = "Open Shop";
        }
    }

    public async void InstantiateNpc()
    {
        if (actualNpcNumber >= maxNpcCapacity || !_canSpawnNpc || !_isOpened) return;

        GameObject npc = Instantiate(_npcs, _path[0].transform);
        _canSpawnNpc = false;
        var npcBrain = npc.GetComponent<NpcBrain>();
        npcBrain.path = (Transform[])_path.Clone();
        npcBrain.gameManager = this;

        actualNpcNumber++;

        await SpawnCooldown();
    }

    public void ReciveMoney(int quantity)
    {
        actualMoney += quantity;
        _moneyValueText.text = actualMoney.ToString();
    }

    private async Task SpawnCooldown()
    {
        float timeEnd = Time.time + instantiateCooldown;

        while (Time.time < timeEnd)
        {
            await Task.Yield();
        }

        _canSpawnNpc = true;
    }

    public void UpgradeSpawnSpeed()
    {
        if (actualMoney < _shopManager.upgradeCosts[2]) return;

        ReciveMoney(-_shopManager.upgradeCosts[2]);
        instantiateCooldown -= 1f;
        _shopManager.SpawnSpeedUpgradePrice();

        if (instantiateCooldown == 1f) _shopManager.DeactivateSpeedButton();
    }

    public void UpgradeShopCapacity()
    {
        if (actualMoney < _shopManager.upgradeCosts[1]) return;

        ReciveMoney(-_shopManager.upgradeCosts[1]);
        maxNpcCapacity += 5;
        _shopManager.ShopCapacityUpgradePrice();

        if (maxNpcCapacity >= 20) _shopManager.DeactivateCapacityButton();
    }

    public void UpgradeClothes()
    {
        if (actualMoney < _shopManager.upgradeCosts[0]) return;

        ReciveMoney(-_shopManager.upgradeCosts[0]);
        instantiateCooldown -= 1f;
        _shopManager.ClothesUpgradePrice();

        if (instantiateCooldown == 1f) _shopManager.DeactivateClothesButton();
    }

    public void ChangeMusicMute()
    {
        _audioSource.mute = !_audioSource.mute;

        if (!_audioSource.mute)
        {
            _musicIcon.sprite = _musicOnAndOff[0];
        }
        else
        {
            _musicIcon.sprite = _musicOnAndOff[1];
        }
    }
}
