using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private static Bootstrap _bootstrap;

    [SerializeField] private InputHandler _inputHandler;

    [SerializeField] private bool _initedObjects;

    [SerializeField] private GameObject _playerGameObject;
    [SerializeField] private GameObject _levelGameObject;
    [SerializeField] private GameObject _uiGameObject;

    [SerializeField] private Vector3 _playerPosition = Vector3.zero;
    [SerializeField] private Vector3 _levelPosition = Vector3.zero;

    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject _level;
    [SerializeField] private GameUI _ui;

    private Camera _camera;

    private void Awake()
    {
        InitObjects();

        _player.Init();
        _ui.Init();
    }

    [ContextMenu("Reboot")]
    private void Reboot()
    {
        DestroyObjects();
        InitObjects();
    }

    [ContextMenu("Init objects")]
    private void InitObjects()
    {
        _bootstrap = this;

        _inputHandler.Init();

        if (!_initedObjects)
        {
            _player = Instantiate(_playerGameObject, _playerPosition, Quaternion.identity).GetComponent<PlayerController>();
            _level = Instantiate(_levelGameObject, _levelPosition, Quaternion.identity);
            _ui = Instantiate(_uiGameObject, _playerPosition, Quaternion.identity).GetComponent<GameUI>();

            _camera = FindAnyObjectByType<Camera>();

            _initedObjects = true;
        }
    }

    [ContextMenu("Destroy objects")]
    private void DestroyObjects()
    {
        if (!_initedObjects) return;

        DestroyImmediate(_player.gameObject);
        DestroyImmediate(_level);
        DestroyImmediate(_ui.gameObject);

        _initedObjects = false;
    }

    public static Bootstrap Instance { get => _bootstrap = _bootstrap != null ? _bootstrap : FindAnyObjectByType<Bootstrap>(); }
    public Camera Camera { get => _camera; }
    public InputHandler InputHandler { get => _inputHandler; }
    public GameUI UI { get => _ui; }
}
