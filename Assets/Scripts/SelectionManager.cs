using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectionManager : MonoBehaviour
{
    public Camera cam;
    public LayerMask arenaLayerMask;
    public Collider permittedArea;

    private Tower _selectedTowerPrefab;
    private Tower _selectedTower;

    public event Func<int,bool> affordable;
    public event Action<int> onBuy;
    public event Action<int> onSell;

    public void SelectTowerType(Tower prefab) {
        _selectedTowerPrefab = prefab;
    }

    private void Update()
    {
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        if (!_selectedTowerPrefab) return;
        if (!CanBePurchased())
        {
            _selectedTowerPrefab = null;
            return;
        }
        
        if (RaycastMousePosition(out var hit))
        {
            if (!_selectedTower) _selectedTower = SpawnTower();
            
            MoveTower();
            
            if (PermittedArea() && Input.GetMouseButtonDown(0))
            {
                _selectedTower.PlaceTower();
                _selectedTowerPrefab = _selectedTower = null;
            }
        }

        bool CanBePurchased()
        {
            return affordable != null && affordable.Invoke(_selectedTowerPrefab.cost);
        }

        bool RaycastMousePosition(out RaycastHit raycastHit)
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out raycastHit, 300, arenaLayerMask);
        }

        void MoveTower()
        {
            _selectedTower.transform.position = hit.point;
        }

        bool PermittedArea()
        {
            var result = (Physics.Raycast(_selectedTower.transform.position + Vector3.up, Vector3.down, 
                              out hit, 1, arenaLayerMask) && hit.collider == permittedArea);
            
            _selectedTower.towerUI.ActivePermittedArea(result);

            return result;
        }
    }

    private Tower SpawnTower()
    {
        var t = Instantiate(_selectedTowerPrefab);
        t.onBuy += onBuy;
        t.onSell += onSell;
        return t;
    }
}
