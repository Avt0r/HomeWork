using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class WeaponController : MonoBehaviour
{

    [SerializeField] private Transform _shotPoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _delay;

    private Dictionary<string, int> _animations = new()
        { 
        {"Idle", Animator.StringToHash("Idle") },
        { "Shot", Animator.StringToHash("Shot") }
    };

    [SerializeField] private Animator _selfAnimator;

    private void OnValidate()
    {
        _selfAnimator = _selfAnimator != null ? _selfAnimator : GetComponent<Animator>();
    }

    private void Awake()
    {
        _selfAnimator.Play(_animations["Idle"]);
    }

    public void ShootStart(InputAction.CallbackContext obj)
    {
        StartCoroutine(ShootCoroutine());
    }

    public void ShootStop(InputAction.CallbackContext obj)
    {
        StopAllCoroutines();
    }

    private IEnumerator ShootCoroutine()
    {
        while(true)
        {
            Instantiate(_bullet, _shotPoint.position, _shotPoint.rotation).transform.SetParent(Bootstrap.Instance.Level.transform);

            AnimShot();

            yield return new WaitForSeconds(_delay);
        }
    }

    private void AnimShot()
    {
        _selfAnimator.Play(_animations["Shot"]);
    }

    private void AnimIdle()
    {
        _selfAnimator.Play(_animations["Idle"]);
    }
}
