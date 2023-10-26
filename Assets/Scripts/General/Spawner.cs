using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private string _pathLoad;
    [SerializeField] private GameObject[] _enemies;
    
    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    [ContextMenu("Load")]
    private void Load()
    {
        _enemies = Resources.LoadAll<GameObject>(_pathLoad);
    }

    private IEnumerator Spawn()
    {
        while(true)
        {
            GameObject gameObject = Instantiate(_enemies[Random.Range(0, _enemies.Length)], transform.position, Quaternion.identity);

            gameObject.transform.SetParent(transform);
            gameObject.GetComponent<Initializable>().Init();

            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
