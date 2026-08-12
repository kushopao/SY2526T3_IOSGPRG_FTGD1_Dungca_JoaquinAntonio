using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Unit>())
        {
            other.gameObject.GetComponent<Unit>().TakeDamage(_damage);
            Destroy(this.gameObject);
        }
    }
}
