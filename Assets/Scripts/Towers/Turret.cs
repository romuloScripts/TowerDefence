using System.Collections;
using UnityEngine;

public class Turret : Tower
{
	public float range = 15f;
	public float shootDelay = 1f;
	public float rotationSpeed = 10f;
	
	public LayerMask enemyLayerMask;

	public Projectile projectilePrefab;

	public Transform shootPoint;
	
	private float shootTime = 0f;
	
	private Transform _target;

	private IEnumerator UpdateTarget()
	{
		const float timeToUpdate = 0.5f;
		
		while (true)
		{
			NearestEnemyInRange(out var nearestEnemy);

			_target = nearestEnemy != null ? nearestEnemy.transform : null;
			
			yield return new WaitForSeconds(timeToUpdate);
		}

		void NearestEnemyInRange(out Transform nearestEnemy)
		{
			nearestEnemy = null;
			var enemiesInRange = Physics.OverlapSphere(transform.position, range, enemyLayerMask);
			var shortestDistance = float.MaxValue;

			foreach (var enemy in enemiesInRange)
			{
				var distanceToEnemy = (transform.position - enemy.transform.position).sqrMagnitude;
				if (distanceToEnemy > shortestDistance) continue;

				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy.transform;
			}
		}
	}

	private void Update()
	{
		UpdateAim();
	}

	private void UpdateAim()
	{
		if (!_target) return;

		Aim();

		if (shootTime <= Time.time - shootDelay)
		{
			shootTime = Time.time;
			Shoot();
		}
	}

	private void Aim()
	{
		var dir = _target.position - transform.position;
		var lookRotation = Quaternion.LookRotation(dir);
		var rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
		transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	private void Shoot()
	{
		var projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
		projectile.target = _target;
	}

	private void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, range);
	}

	protected override void Initialize()
	{
		StartCoroutine(UpdateTarget());
		enabled = true;
	}
}
