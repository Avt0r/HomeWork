using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private static Bootstrap _bootstrap;

    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Wallet _wallet;

    [SerializeField] private bool _spawnedObjects;

    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _levelPrefab;
    [SerializeField] private GameObject _uiPrefab;

    [SerializeField] private Vector3 _playerPosition = Vector3.zero;
    [SerializeField] private Vector3 _levelPosition = Vector3.zero;

    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject _level;
    [SerializeField] private GameUI _ui;

    [SerializeField] private Camera _camera;

    private void Awake()
    {
        SpawnObjects();
        InitObjects();
    }

    [ContextMenu("Reboot")]
    private void Reboot()
    {
        if(Application.isEditor)
        {
            Debug.Log("Don`t use in editor mode");
            return;
        }
        FinishObjects();
        DestroyObjects();
        SpawnObjects();
        InitObjects();
    }

    [ContextMenu("Respawn")]
    private void Respawn()
    {
        DestroyObjects();
        SpawnObjects();
    }

    private void InitObjects()
    {
        _bootstrap = this;

        _inputHandler.Init();

        _player.Init();
        _ui.Init();
    }

    private void FinishObjects()
    {
        _player?.Finish();
    }

    [ContextMenu("Spawn objects")]
    private void SpawnObjects()
    {
        if (!_spawnedObjects)
        {
            _spawnedObjects = true;

            _player = Instantiate(_playerPrefab, _playerPosition, Quaternion.identity).GetComponent<PlayerController>();
            _level = Instantiate(_levelPrefab, _levelPosition, Quaternion.identity);
            _ui = Instantiate(_uiPrefab, _playerPosition, Quaternion.identity).GetComponent<GameUI>();

            _camera = _player.Camera.gameObject.GetComponent<Camera>();
        }
    }

    [ContextMenu("Destroy objects")]
    private void DestroyObjects()
    {
        if (!_spawnedObjects) return;

        DestroyImmediate(_player.gameObject);
        DestroyImmediate(_level);
        DestroyImmediate(_ui.gameObject);

        _spawnedObjects = false;
    }

    public static Bootstrap Instance { get => _bootstrap = _bootstrap != null ? _bootstrap : FindAnyObjectByType<Bootstrap>(); }
    public Camera Camera { get => _camera; }
    public InputHandler InputHandler { get => _inputHandler; }
    public Wallet Wallet => _wallet;
    public PlayerController Player { get => _player; }
    public GameObject Level { get => _level; }
    public GameUI UI { get => _ui; }
}
