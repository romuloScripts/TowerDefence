using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
	public ObservedInt money;
	public ObservedInt lives;
	
	public SpawnerWave spawnerWave;
	[FormerlySerializedAs("buildingManager")] public SelectionManager SelectionManager;
	public GameObject gameplayUI;
	
	public GameEvents events;

	private bool _lose;

	[Serializable]
	public class GameEvents
	{
		public UnityEvent onPlayerWin;
		public UnityEvent onPlayerLose;
	}

	public void StartGame()
	{
		BindEvents();
		spawnerWave.Initialize();
		EnableUI();		
	}

	private void EnableUI()
	{
		money.ValueChanged();
		lives.ValueChanged();
		gameplayUI.SetActive(true);
	}
	
	private void DisableUI()
	{
		gameplayUI.SetActive(false);
	}

	private void BindEvents()
	{
		spawnerWave.onEnd += Win;
		spawnerWave.onEnemyDie += EnemyDie;
		spawnerWave.onTargetReached += TakeDamage;

		SelectionManager.affordable += Affordable;
		SelectionManager.onBuy += Buy;
		SelectionManager.onSell += Sell;
	}

	private void TakeDamage()
	{
		if(_lose) return;
		lives--;
		
		if (lives <= 0)
		{
			_lose = true;
			Lose();
		}
	}

	private void Lose()
	{
		DisableUI();
		spawnerWave.gameObject.SetActive(false);
		events.onPlayerLose.Invoke();
	}

	private void Win()
	{
		DisableUI();
		spawnerWave.gameObject.SetActive(false);
		events.onPlayerWin.Invoke();
	}

	private void Buy(int cost) => money -= cost;

	private void Sell(int cost) => money += cost;

	private void EnemyDie(int cost) => money += cost;

	private bool Affordable(int cost) => money >= cost;
}
