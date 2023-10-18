using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class GameUI : Initializable
{

    [SerializeField] private Canvas _canvas;
    [SerializeField] private Text _coins;

    [SerializeField] private GameObject _uiWin;
    [SerializeField] private GameObject _uiGame;

    private void OnValidate()
    {
        _canvas = _canvas != null ? _canvas : GetComponent<Canvas>();
    }

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
        _coins.text = "Coins: " + coins;
    }

    public void ShowWin()
    {
        _uiWin.SetActive(true);
        _uiGame.SetActive(false);
    }
}
