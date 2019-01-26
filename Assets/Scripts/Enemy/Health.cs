using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Health
{
    public float StartHealth = 100;
    public ChangeValueEvent UiUpdate;
    
    [Serializable]
    public class ChangeValueEvent: UnityEvent<float>{}
		
    private float _currentHealth;

    public void Ini()
    {
        CurrentHealth = StartHealth;
    }

    public float CurrentHealth
    {
        get => _currentHealth;
        set{
            _currentHealth = value;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, StartHealth);
            UiUpdate.Invoke(_currentHealth/StartHealth);
        }
    }
}