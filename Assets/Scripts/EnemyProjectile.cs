using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public LayerMask towerMask;

    private void Update()
    {
        // Move the projectile
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the projectile hit a tower
        if (((1 << other.gameObject.layer) & towerMask) != 0)
        {
            Tower tower = other.GetComponent<Tower>();
            if (tower != null)
            {
                tower.TakeDamage(damage);
                Destroy(gameObject); // Destroy the projectile after hitting a tower
            }
        }
    }
}
