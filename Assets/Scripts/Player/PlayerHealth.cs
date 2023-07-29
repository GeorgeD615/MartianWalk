using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    static public PlayerHealth Instance;
    [SerializeField] private int _health = 5; public int Health => _health;
    [SerializeField] private int _maxHealth = 8; public int MaxHealth => _maxHealth;


    //[SerializeField] private AudioSource _addHealthSound;
    [SerializeField] private HealthUI _healthUI;

    private bool _invulnerable = false;
    private bool _die;

    public UnityEvent EventOnTakeDamage;
    public UnityEvent EventDie;

    private void Start()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
        DontDestroyOnLoad(gameObject);

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
            _invulnerable = true;
            Invoke(nameof(StopInvulnerable), 1f);
            _healthUI.DisplayHealth(_health);
            EventOnTakeDamage.Invoke();
        }
    }

    private void StopInvulnerable()
    {
        _invulnerable = false;
    }

    public void AddHealth(int healthValue)
    {
        _health += healthValue;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        //addHealthSound.Play();
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
