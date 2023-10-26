using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    private void OnValidate()
    {
        _rb = _rb != null ? _rb : GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rb.AddForce(transform.up * 10, ForceMode.Impulse);

        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Coin>(out var coin))
        {
            Bootstrap.Instance.Wallet.AddCoin(coin);
        }
    }
}
