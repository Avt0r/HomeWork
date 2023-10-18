using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{

    [SerializeField] private Transform _shotPoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _delay;

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
            Instantiate(_bullet, _shotPoint.position, _shotPoint.rotation);

            yield return new WaitForSeconds(_delay);
        }
    }
}
