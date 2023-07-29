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

    [SerializeField] private GameObject _playerPref;

    [SerializeField] private CinemachineVirtualCamera _menuCamera;
    [SerializeField] private CinemachineVirtualCamera _settingsCamera;
    [SerializeField] private CinemachineVirtualCamera _playerCamera;

    [SerializeField] private Transform _leftRobotPosition;

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

        if (_checkpointLoaded)
        {
            PlayerControlEnable();
            MainMenuDisable();
            SettingsMenuDisable();
        }
        else
        {
            MainMenuEnable();
            SettingsMenuDisable();
            PlayerControlDisable();
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

    public void SettingsMenuEnable()
    {
        _settingsUI.SetActive(true);
        Cursor.visible = true;
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
}
