using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helth : MonoBehaviour
{
    [SerializeField] public float _maxHealth;
    [SerializeField] public float _curHealth;

    private void Start()
    {
        _curHealth = _maxHealth;
    }
    public void GetDamage(float d)
    {
        _curHealth -= d;
    }
    public void GetHeal(float d)
    {
        _curHealth += d;
        if(_curHealth > _maxHealth)
        {
            _curHealth = _maxHealth;
        }
    }
    public bool IsAlive()
    {
        return _curHealth > 0;
    }
}
