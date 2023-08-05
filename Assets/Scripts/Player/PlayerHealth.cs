using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    static public PlayerHealth Instance;
    [SerializeField] private int _health = 5; public int Health => _health;
    [SerializeField] private int _maxHealth = 8; public int MaxHealth => _maxHealth;

    [SerializeField] private SkinnedMeshRenderer _playerBody;

    private Material _yellowMat;
    private Material _blackMat;
    private Material _whiteMat;
    private Material[] _bodyMats;
    [SerializeField] private Material _damageMat;

    [SerializeField] private HealthUI _healthUI;

    private bool _invulnerable = false;
    private bool _die;

    public UnityEvent EventOnTakeDamage;
    public UnityEvent EventDie;

    private void Awake()
    {
        _bodyMats = new Material[3];
        _blackMat = _playerBody.materials[0];
        _whiteMat = _playerBody.materials[1];
        _yellowMat = _playerBody.materials[2];
        _bodyMats[0] = _blackMat;
        _bodyMats[1] = _whiteMat;
        _bodyMats[2] = _yellowMat;
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;

        _healthUI.Setup(_maxHealth);
        _healthUI.DisplayHealth(_maxHealth);
    }

    public void TakeDamage(int damageValue)
    {
        if (_invulnerable == false)
        {
            _health -= damageValue;
            if (_health <= 0)
            {
                Debug.Log("1");
                _health = 0;
                if (_die == false)
                {
                    Debug.Log("2");
                    _die = true;
                    Die();
                }
            }
            _playerBody.materials = new Material[3] { _damageMat, _damageMat, _damageMat };
            _invulnerable = true;
            Invoke(nameof(StopInvulnerable), 1f);
            _healthUI.DisplayHealth(_health);
            EventOnTakeDamage.Invoke();
        }
    }

    private void StopInvulnerable()
    {
        _invulnerable = false;
        _playerBody.materials = _bodyMats;
    }

    public void AddHealth(int healthValue)
    {
        _health += healthValue;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }

        _healthUI.DisplayHealth(_health);
    }

    public void ResetHealth()
    {
        _die = false;
        _health = _maxHealth;
    }
    private void Die()
    {
        Debug.Log("3");
        //Invoke(nameof(OpenLoseWindow), 2f);
        EventDie.Invoke();
    }

    private void OpenLoseWindow()
    {
        //WindowManager.Instance.Lose();
    }
}
