using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Box : MonoBehaviour
{
    [SerializeField] private GameObject _boxBroken;
    [SerializeField] private GameObject _coin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>())
        {
            Destroy(gameObject);

            Instantiate(_boxBroken, transform.position, Quaternion.identity).transform.SetParent(transform.parent);
            Instantiate(_coin, transform.position, Quaternion.identity).transform.SetParent(transform.parent);
        }
    }
}
