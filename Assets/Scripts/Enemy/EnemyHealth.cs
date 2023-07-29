using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _health = 100;
    [SerializeField] private Slider _healthBar;
    //[SerializeField] private GameObject _effectPrefab;
    //[SerializeField] private AudioSource _deathSound;

    public UnityEvent EventOnTakeDamage;
    public UnityEvent EventOnDie;

    public Vector3 EnemyColor;

    private void Start()
    {
        _healthBar.maxValue = _health;
        _healthBar.value = _health;
        //_healthBar.gameObject.SetActive(false);
    }

    public void TakeDamage(float damageValue)
    {
        _health -= damageValue;
        _healthBar.value = _health;
        //_healthBar.gameObject.SetActive(true);
        if (_health <= 0)
        {
            Die();
        }
        else
        {
            EventOnTakeDamage.Invoke();
        }
    }
    public void Die()
    {
        //if (_deathSound != null)
        //{
        //    SoundDeathPlay();
        //}
        //Instantiate(_effectPrefab, transform.position, Quaternion.identity);
        _healthBar.gameObject.SetActive(false);
        EventOnDie.Invoke();
        Destroy(gameObject);
    }

    //public void SoundDeathPlay()
    //{
    //    _deathSound.transform.parent = null;
    //    _deathSound.Play();
    //    Destroy(_deathSound.gameObject, 3f);
    //}
}
