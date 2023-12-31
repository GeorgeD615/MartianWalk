using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserFire : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _firePoint;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _targetPoint;

    [SerializeField] private float _damagePerSecond = 10;

    [SerializeField] private Scrollbar _redScrollbar;
    [SerializeField] private Scrollbar _greenScrollbar;
    [SerializeField] private Scrollbar _blueScrollbar;

    [SerializeField] private Image _redBound;
    [SerializeField] private Image _greenBound;
    [SerializeField] private Image _blueBound;

    [SerializeField] private GameObject _effectOnEnemy;
    [SerializeField] private AudioSource _laserSound;
    [SerializeField] private AudioSource _laserOnSound;
    [SerializeField] private AudioSource _selectColorSound;
    [SerializeField] private AudioSource _switchColorSound;


    private Scrollbar _currentChosenScrollbar;
 
    static public LaserFire Instanse;

    public Vector3 CurrentColor;


    void Awake()
    {
        if(Instanse == null)
            Instanse = this;
        else
            Destroy(gameObject);
        CurrentColor = new Vector3(0, 0, 0);
        _currentChosenScrollbar = _redScrollbar;
        _redBound.enabled = true;
        _greenBound.enabled = false;
        _blueBound.enabled = false;
        _effectOnEnemy.SetActive(false);
    }

    private bool _laserSoundPlay = false;
    void Update()
    {
        //Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Ray ray = new Ray(_firePoint.position, _firePoint.forward);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
        {
            _targetPoint.transform.position = raycastHit.point;
        }
        else
        {
            _targetPoint.transform.position = ray.direction.normalized * 10000;
        }

        if (Input.GetMouseButton(0) && CurrentColor != Vector3.zero)
        {
            if (!_laserSoundPlay)
            {
                _laserOnSound.Play();
                _laserSound.Play();
                _laserSoundPlay = true;
            }
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, _firePoint.position);
            _lineRenderer.SetPosition(1, _targetPoint.transform.position);
            _effectOnEnemy.SetActive(true);
            _effectOnEnemy.transform.position = _targetPoint.transform.position;

            if (raycastHit.transform != null && raycastHit.transform.root.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                Debug.Log("enemy found");
                if(enemyHealth.EnemyColor == CurrentColor)
                {
                    Debug.Log("enemy hit");
                    enemyHealth.TakeDamage(_damagePerSecond * Time.deltaTime);
                }
            }
        }
        else
        {
            _lineRenderer.enabled = false;
            _effectOnEnemy.SetActive(false);
            _laserSound.Pause();
            _laserSoundPlay = false;
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetAxis("Mouse ScrollWheel") > 0.1)
        {
            _selectColorSound.Play();
            if (_currentChosenScrollbar == _redScrollbar)
            {
                _currentChosenScrollbar = _blueScrollbar;
                _redBound.enabled = false;
                _blueBound.enabled = true;
            }else if(_currentChosenScrollbar == _blueScrollbar)
            {
                _currentChosenScrollbar = _greenScrollbar;
                _blueBound.enabled = false;
                _greenBound.enabled = true;
            }
            else
            {
                _currentChosenScrollbar = _redScrollbar;
                _redBound.enabled = true;
                _greenBound.enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetAxis("Mouse ScrollWheel") < -0.1)
        {
            _selectColorSound.Play();
            if (_currentChosenScrollbar == _redScrollbar)
            {
                _currentChosenScrollbar = _greenScrollbar;
                _redBound.enabled = false;
                _greenBound.enabled = true;
            }
            else if (_currentChosenScrollbar == _blueScrollbar)
            {
                _currentChosenScrollbar = _redScrollbar;
                _blueBound.enabled = false;
                _redBound.enabled = true;
            }
            else
            {
                _currentChosenScrollbar = _blueScrollbar;
                _greenBound.enabled = false;
                _blueBound.enabled = true;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            _switchColorSound.Play();
            if (_currentChosenScrollbar.value == 1)
            {
                _currentChosenScrollbar.value = 0;
                _currentChosenScrollbar.GetComponent<Image>().color = Color.white;
                if (_currentChosenScrollbar == _redScrollbar)
                    CurrentColor.x = 0;
                else if (_currentChosenScrollbar == _blueScrollbar)
                    CurrentColor.z = 0;
                else
                    CurrentColor.y = 0;
            }
            else
            {
                _currentChosenScrollbar.value = 1;
                if (_currentChosenScrollbar == _redScrollbar)
                {
                    CurrentColor.x = 1;
                    _currentChosenScrollbar.GetComponent<Image>().color = Color.red;
                }
                else if (_currentChosenScrollbar == _blueScrollbar)
                {
                    CurrentColor.z = 1;
                    _currentChosenScrollbar.GetComponent<Image>().color = Color.blue;
                }
                else
                {
                    CurrentColor.y = 1;
                    _currentChosenScrollbar.GetComponent<Image>().color = Color.green;
                }
            }

            Color newColor = new Color(CurrentColor.x, CurrentColor.y, CurrentColor.z, 1.0f);
            _lineRenderer.material.SetColor("_EmissionColor", newColor);
            _lineRenderer.startColor = newColor;
            _lineRenderer.endColor = newColor;

        }
    }
}
