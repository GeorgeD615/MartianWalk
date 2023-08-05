using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Camera _mainCamera;

    [SerializeField] private GameObject _healthUI;
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _settingsUI;
    [SerializeField] private GameObject _pauseMenuUI;

    [SerializeField] private GameObject _playerPref;

    [SerializeField] private CinemachineVirtualCamera _menuCamera;
    [SerializeField] private CinemachineVirtualCamera _settingsCamera;
    [SerializeField] private CinemachineVirtualCamera _playerCamera;

    [SerializeField] private Transform _leftRobotPosition;

    [SerializeField] private EnemyBehavior[] _enemyBehavior;
    [SerializeField] private EnemyThrow[] _enemyThrows;
    [SerializeField] private EnemyRotation[] _enemyRotations;
    [SerializeField] private EnemyHealth[] _enemyHealths;

    private CinemachineVirtualCamera _currentCamera;

    public Checkpoint LastCheckpoint;

    private bool _checkpointLoaded = false;
    private void Start()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);

        MainMenuEnable();
        SettingsMenuDisable();
        PlayerControlDisable();
        PauseMenuDisable();
    }

    private bool _onPause;

    public void PauseOff()
    {
        Time.timeScale = 1f;
    }

    public void PauseOn()
    {
        Time.timeScale = 0f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_menuUI.activeSelf && !_settingsCamera.enabled)
        {
            if (_onPause)
            {
                _onPause = false;
                Time.timeScale = 1f;
                EnableEnemys();
                PlayerControlEnable();
                SwitchCamera(_playerCamera);
                Cursor.visible = false;
                _pauseMenuUI.SetActive(false);
                _settingsUI.SetActive(false);
            }
            else
            {
                _onPause = true;
                Time.timeScale = 0f;
                PlayerControlDisable();
                DisableEnemys();
                Cursor.visible = true;
                _pauseMenuUI.SetActive(true);
            }
        }
    }

    public void PlayerControlEnable()
    {
        PlayerMovement.Instance.enabled = true;
        PlayerRotate.Instance.enabled = true;
        PlayerHealth.Instance.enabled = true;
        RobotMovement.Instanse.enabled = true;
        LaserFire.Instanse.enabled = true;
        _healthUI.SetActive(true);
        Cursor.visible = false;
        SwitchCamera(_playerCamera);
    }

    public void PlayerControlDisable()
    {
        PlayerMovement.Instance.enabled = false;
        PlayerRotate.Instance.enabled = false;
        PlayerHealth.Instance.enabled = false;
        RobotMovement.Instanse.enabled = false;
        LaserFire.Instanse.enabled = false;
        _healthUI.SetActive(false);
    }

    public void MainMenuEnable()
    {
        _menuUI.SetActive(true);
        Cursor.visible = true;
        SwitchCamera(_menuCamera);
    }

    public void MainMenuDisable()
    {
        _menuUI.SetActive(false);
    }

    public void PauseMenuEnable()
    {
        _pauseMenuUI.SetActive(true);
    }

    public void PauseMenuDisable()
    {
        _pauseMenuUI.SetActive(false);
    }

    public void SettingsMenuEnable()
    {
        _settingsUI.SetActive(true);
        Cursor.visible = true;
        if(!_onPause)
            SwitchCamera(_settingsCamera);
    }

    public void SettingsMenuDisable()
    {
        _settingsUI.SetActive(false);
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    private void SwitchCamera(CinemachineVirtualCamera newCamera) 
    {
        if (newCamera == _currentCamera)
            return;

        newCamera.enabled = true;
        
        if(_currentCamera != null)
            _currentCamera.enabled = false;

        _currentCamera = newCamera;
    }

    public void LoadLastCheckpoint()
    {
        _checkpointLoaded = true;
        PlayerMovement.Instance.transform.position = LastCheckpoint.transform.position;
        PlayerMovement.Instance.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        RobotMovement.Instanse.transform.position = _leftRobotPosition.position;
        PlayerHealth.Instance.ResetHealth();
    }

    public void EnableEnemys()
    {
        foreach (EnemyHealth obj in _enemyHealths)
        {
            obj.enabled = true;
        }
        foreach (EnemyRotation obj in _enemyRotations)
        {
            obj.enabled = true;
        }
        foreach (EnemyThrow obj in _enemyThrows)
        {
            obj.enabled = true;
        }
        foreach (EnemyBehavior obj in _enemyBehavior)
        {
            obj.enabled = true;
        }
    }

    public void DisableEnemys()
    {
        foreach (EnemyHealth obj in _enemyHealths)
        {
            obj.enabled = false;
        }
        foreach (EnemyRotation obj in _enemyRotations)
        {
            obj.enabled = false;
        }
        foreach (EnemyThrow obj in _enemyThrows)
        {
            obj.enabled = false;
        }
        foreach (EnemyBehavior obj in _enemyBehavior)
        {
            obj.enabled = false;
        }
    }

    public void BackButtonPressed()
    {
        SettingsMenuDisable();
        if (_onPause)
        {
            PauseMenuEnable();
        }
        else
        {
            MainMenuEnable();
        }
    }
}
