using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    public float speed = 10f;

	public Health health;

	public int moneyCost = 50;

	[HideInInspector]
	public Transform target;

	public event Action<int> onDie;
	public event Action onRemove;
	public event Action onDamageTarget;
	
	private bool _isDead;

	public void TakeDamage (int amount)
	{
		health.CurrentHealth -= amount;

		if (health.CurrentHealth <= 0 && !_isDead)
		{
			Die();
		}
	}
	
	private void Start ()
	{
		health.Ini();
	}

	private void Die ()
	{
		_isDead = true;
		onDie?.Invoke(moneyCost);
		Destroy(gameObject);
	}

	private void OnDestroy()
	{
		onRemove?.Invoke();
	}

	public void TargetReached()
	{
		onDamageTarget?.Invoke();
	}
}
