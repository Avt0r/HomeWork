using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private Transform _coinOnUI;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private SoundSource _soundSource;

    private int _coins = 0;

    private void OnValidate()
    {
        _soundSource = _soundSource != null ? _soundSource : GetComponent<SoundSource>();
    }

    public void AddCoin(Coin coin)
    {
        if (coin.Collected) return;

        coin.Collected = true;
        _coins++;

        _coinOnUI = Bootstrap.Instance.UI.CoinPosition;

        StartCoroutine(CoinCollectAnim(coin));
    }

    private IEnumerator CoinCollectAnim(Coin coin)
    {
        Vector3 start = coin.transform.position;

        for (float i = 0; i < 1f; i += Time.deltaTime)
        {
            coin.transform.position = Vector3.Lerp(start, _coinOnUI.position, i);

            float scale = Mathf.Lerp(1f, 0.2f, i);
            coin.transform.localScale = Vector3.one * scale;
            yield return null;
        }

        _soundSource.Play("Coin");

        Destroy(coin.gameObject);
        Bootstrap.Instance.UI.UpdateCoins(_coins);

        for (float i = 0; i < 1f; i += Time.deltaTime)
        {
            float scale = _animationCurve.Evaluate(i);
            _coinOnUI.transform.localScale = Vector3.one * scale;
            yield return null;
        }

        yield return null;
    }

    public int Coins { get => _coins; }
}
