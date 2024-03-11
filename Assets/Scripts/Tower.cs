using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private int damage;
    public int towerHP;
    public int towerMaxHP;
    private GameObject[] otherTowers;
    public int attackRange = 10;
    public int towerType = 0;
    public LayerMask enemyMask;
    public GameObject projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        otherTowers = GameObject.FindGameObjectsWithTag("Tower");
        if (towerType == 1)
        {
            Heal();
        }
        else
        {
            ShootEnemies();
        }

    }
    public void TakeDamage(int damage)
    {
        towerHP = towerHP - damage;
        if (towerHP > 0)
        {
            Destroy(gameObject);
        }
    }
    public void Heal()
    {

    }
    public void ShootEnemies()
    {
        RaycastHit2D[] towersInRange = Physics2D.RaycastAll(transform.position, Vector2.zero, attackRange, enemyMask);
        if (towersInRange.Length > 0)
        {
            // Find the closest tower
            Tower closestTower = null;
            float closestDistance = Mathf.Infinity;
            foreach (RaycastHit2D hit in towersInRange)
            {
                Tower tower = hit.collider.GetComponent<Tower>();
                if (tower != null)
                {
                    float distance = Vector2.Distance(transform.position, tower.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTower = tower;
                    }
                }
            }

            // Shoot a projectile towards the closest tower
            if (closestTower != null)
            {
                Vector2 direction = (closestTower.transform.position - transform.position).normalized;
                GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectileInstance.transform.right = direction;
            }
        }
    }
    public void Upgrade()
    {
        if (TowerPlacement.playerMoney > 100)
        {
            towerMaxHP = towerMaxHP + 20;
            TowerPlacement.playerMoney = TowerPlacement.playerMoney - 100;
        }
        

    }
}
