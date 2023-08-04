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


    private Scrollbar _currentChosenScrollbar;
 
    static public LaserFire Instanse;

    public Vector3 CurrentColor;


    void Start()
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

    void Update()
    {
        

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
        {
            if (raycastHit.point.z > 0.5)
                _targetPoint.transform.position = raycastHit.point;
            else
                _targetPoint.transform.position = new Vector3(raycastHit.point.x, 0, 0.5f);

        }
        else
        {
            _targetPoint.transform.position = ray.direction.normalized * 10000;
        }

        if (Input.GetMouseButton(0) && CurrentColor != Vector3.zero)
        {
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
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(_currentChosenScrollbar == _redScrollbar)
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
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
            _lineRenderer.startColor = newColor;
            _lineRenderer.endColor = newColor;

        }
    }
}
