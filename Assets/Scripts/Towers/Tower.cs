using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Tower : MonoBehaviour
{
    public int cost;
    [FormerlySerializedAs("TowerUi")] public TowerUI towerUI;

    public event Action<int> onBuy;
    public event Action<int> onSell; 
    
    protected abstract void Initialize();

    public void PlaceTower()
    {
        towerUI.ActiveConfirmUI(true);
    }

    public void Confirm()
    {
        towerUI.Disable();
        onBuy?.Invoke(cost);
        Initialize();
    }   

    public void Cancel()
    {
        Destroy(gameObject);
    }
    
    public void Sell()
    {
        onSell?.Invoke(cost);
        Destroy(gameObject);
    }
}
