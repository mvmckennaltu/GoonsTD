using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class EnemyBase : MonoBehaviour
{
    public GameObject[] waypoints;
    private int currentWaypointIndex;
    public float baseMoveSpeed = 0.1f;
    public int HP = 10;
    public int maxHP = 10;
    private SpriteRenderer spriteRenderer;
    public int enemyType = 0;
    public LayerMask towerMask;
    public int attackRange = 10;
    public GameObject projectilePrefab;
    public float shootingInterval = 1f;
    private float shootingTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetupWaypoints();
    }

    // Update is called once per frame
    void Update()
    {
        
        MoveToWaypoint();
        if (enemyType == 1) 
        {
            shootingTimer += Time.deltaTime;
            if (shootingTimer >= shootingInterval)
            {
                RangedDamage();
                shootingTimer = 0f;
            }
        }
        

    }
    void MoveToWaypoint()
    {
        if (waypoints.Length > 0)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex].transform;
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, baseMoveSpeed * Time.deltaTime);

            if (transform.position == targetWaypoint.position)
            {
                currentWaypointIndex++;
            }
            if(currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 2;
            }
        }
    }

    void TakeDamage()
    {
        StartCoroutine(FlashColor());
        if (HP > 0)
        {

            HP--;
        }
        if (HP == 0)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator FlashColor()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player Projectile"))
        {
            TakeDamage();
        }
    }
    void RangedDamage()
    {
        RaycastHit2D[] towersInRange = Physics2D.RaycastAll(transform.position, Vector2.zero, attackRange, towerMask);
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
    void SetupWaypoints()
    {
        // Find all GameObjects with a specific tag or name
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        // Alternatively, you can find them by name
        // waypoints = new GameObject[] { GameObject.Find("Waypoint1"), GameObject.Find("Waypoint2"), ... };
    }

}
