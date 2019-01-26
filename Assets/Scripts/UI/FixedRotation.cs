using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    private Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = startRotation;
    }
}
