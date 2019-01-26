using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerWave : MonoBehaviour
{
    public float startTime = 4f;
    public float timeBetweenWaves = 2f;
    
    public Transform enemyTarget;
    
    public Wave[] waves;

    public event Action<int> onEnemyDie;
    public event Action onEnd; 
    public event Action onTargetReached; 
    
    public StringEvent onWaveChanged;
    
    private int _waveId;
    private int _enemyCount;
    
    [Serializable]
    public class StringEvent : UnityEvent<string>{ }
    
    [Serializable]
    public class Wave
    {
        public Transform spawnPoint;
        public Enemy enemy;
        public int count=5;
        public float time =10;
        public float timeBetweenEnemies=1;
        
    }

    public void Initialize() 
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        var timeStartWave = 0f;
        yield return new WaitForSeconds(startTime);
        
        while (true)
        {  
            if (LastWave())
            {
                if (_enemyCount <= 0)
                {
                    onEnd.Invoke();
                    yield break;
                }
                yield return null;
            }
            else
            {     
                timeStartWave = Time.time;
                StartCoroutine(SpawnWave());
                
                yield return new WaitWhile(WaitNextWave);
                yield return new WaitForSeconds(timeBetweenWaves);
                
                _waveId++;
            }
        }
        
        bool WaitNextWave()
        {
            return Time.time - timeStartWave < waves[_waveId].time && _enemyCount > 0;
        }

        bool LastWave()
        {
            return _waveId == waves.Length;
        }
    }

    

    private IEnumerator SpawnWave()
    {
        onWaveChanged.Invoke((_waveId+1).ToString());

        var wave = waves[_waveId];

        for (var i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy,wave.spawnPoint);
            yield return new WaitForSeconds(wave.timeBetweenEnemies);
        }  
    }

    private void SpawnEnemy (Enemy enemy, Transform spawnPoint)
    {
        var newEnemy = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation,transform);
        AddEnemy(newEnemy);
    }    
    
    private void AddEnemy(Enemy enemy)
    {
        enemy.onRemove += RemoveEnemy;
        enemy.onDie += onEnemyDie;
        enemy.onDamageTarget += onTargetReached;
        
        enemy.target = enemyTarget;
        _enemyCount++;
    }
    
    private void RemoveEnemy()
    {
        _enemyCount--;
    }
}
