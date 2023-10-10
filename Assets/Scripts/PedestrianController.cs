using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PedestrianController : MonoBehaviour
{

    private Rigidbody _rb;

    [SerializeField] private float _speed;

    private bool _move = false;
    private bool _nonStop = false;

    private void OnValidate()
    {
        _rb = _rb != null ? _rb : GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        EventFather.SubscibeToRedLight(() => { _move = false; });
        EventFather.SubscribeToGreenLight(() => { _move = true; });
    }

    private void FixedUpdate()
    {
        if (_move || _nonStop)
        {
            Move();
        }
        else
        {
            Stop();
        }
    }

    private void Move()
    {
        _rb.velocity = Vector3.forward * _speed;
    }

    private void Stop()
    {
        _rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Road"))
        {
            _nonStop = true;
        }
    }
}
