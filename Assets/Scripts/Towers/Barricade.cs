using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Barricade : Tower
{
    public NavMeshObstacle obstacle;
    
    protected override void Initialize()
    {
        obstacle.enabled = true;
    }
}
