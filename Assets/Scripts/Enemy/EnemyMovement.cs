using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy)),RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private Transform _target;
	private NavMeshAgent _navMeshAgent;
	private Enemy _enemy;

	private const float Range = 0.5f;

	private void Start()
	{	
		_enemy = GetComponent<Enemy>();
		_target = _enemy.target;
		
		_navMeshAgent = GetComponent<NavMeshAgent>();
		_navMeshAgent.speed = _enemy.speed;
		_navMeshAgent.SetDestination(_target.position);
	}

	private void Update()
	{
		if (CheckEnd())
		{
			EndPath();
		}
	}

	private bool CheckEnd() => (_navMeshAgent.destination -  transform.position).sqrMagnitude <= Range;

	private void EndPath()
	{
		_enemy.TargetReached();
		Destroy(gameObject);
	}
}
