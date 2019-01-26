using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    void TakeDamage(int amount);
}

public class Projectile : MonoBehaviour
{
    public int damage = 1;
    public float speed = 15f;

    [HideInInspector]
    public Transform target;

    private void Update ()
    {
        if(target == null) {
            Destroy(gameObject);
            return;
        }
        Movement();
    }

    private void Movement()
    {
        var dir = target.position - transform.localPosition;

        var movement = speed * Time.deltaTime;

        if (InCollideDistance())
        {
            Hit();
        }
        else
        {
            transform.Translate(dir.normalized * movement, Space.World);
            var targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);
        }

        bool InCollideDistance()
        {
            return dir.magnitude <= movement;
        }
    }

    private void Hit() {
        target.GetComponent<IDamage>()?.TakeDamage(damage);
        Destroy(gameObject);
    }
}
