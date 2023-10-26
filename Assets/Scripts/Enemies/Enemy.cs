using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : Initializable
{
    [SerializeField] private Animator _selfAnimator;
    [SerializeField] private NavMeshAgent _meshAgent;
    [SerializeField] private GameObject _drop;

    private bool _alive = true;

    private void OnValidate()
    {
        _meshAgent = _meshAgent != null ? _meshAgent : GetComponent<NavMeshAgent>();
    }

    public override void Init()
    {
        _selfAnimator.Play("Walk");

    }

    public override void Finish()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        _alive = false;
        _selfAnimator.Play("Death");
        yield return new WaitForSeconds(2f);
        Instantiate(_drop, transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (_alive)
        {
            _meshAgent.SetDestination(Bootstrap.Instance.Player.transform.position);
        }
        else
        {
            _meshAgent.isStopped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() && _alive)
        {
            Destroy(other.gameObject);
            Finish();
        }
    }
}
