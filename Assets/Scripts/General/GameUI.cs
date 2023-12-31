using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : Initializable
{

    [SerializeField] private Canvas _canvas;
    [SerializeField] private Text _coins;
    [SerializeField] private Transform _coinPosition;

    [SerializeField] private GameObject _uiWin;
    [SerializeField] private GameObject _uiGame;

    public override void Init()
    {
        if (_inited) return;
        _inited = true;

        _canvas.worldCamera = Bootstrap.Instance.Camera;
        _canvas.planeDistance = 0.5f;

        _uiWin.SetActive(false);
        _uiGame.SetActive(true);
    }

    public void UpdateCoins(int coins)
    {
        _coins.text = coins.ToString();
    }

    private void ShowWin()
    {
        _uiWin.SetActive(true);
        _uiGame.SetActive(false);
    }

    public Transform CoinPosition { get => _coinPosition; set => _coinPosition = value; }
}
