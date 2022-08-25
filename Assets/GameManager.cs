using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;

public class GameManager : MonoBehaviour
{

    private bool _isOpened = false;
    private bool _canSpawnNpc = true;
    private int _actualNpcNumber = 0;
    private int _maxNpcCapacity = 5;

    public float instantiateCooldown = 10f;

    [SerializeField] TMP_Text _isOpenedText;
    [SerializeField] GameObject _npcs;
    [SerializeField] Transform[] _path;

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
        if (_actualNpcNumber >= _maxNpcCapacity || !_canSpawnNpc || !_isOpened) return;

        GameObject npc = Instantiate(_npcs, _path[0].transform);
        _canSpawnNpc = false;
        npc.GetComponent<NpcBrain>().path = _path;

        _actualNpcNumber++;

        await SpawnCooldown();
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
}
