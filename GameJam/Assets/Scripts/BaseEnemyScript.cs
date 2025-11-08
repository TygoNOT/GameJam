using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{

    [SerializeField] private float speedPoints;
    [SerializeField] private int damagePoints;
    [SerializeField] private float maxHealthPoints;
    private float currentHealthPoints;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;
    private bool isFenceAlive = false;
    private bool isGardenAlive;
    private bool isMoving = true;
    private bool isAttacking = false;

    private Vector2 myPosition;
    private Vector2 targetPosition;

    private GameObject? fence = null;
    public GameObject garden;
    [SerializeField] private LayerMask attackTarget;
    private string targetTag = "GardenArea";

    private void OnEnable()
    {
        EnemyWaveManager.aliveEnemies++;
    }

    private void OnDisable()
    {
        EnemyWaveManager.aliveEnemies--;
    }

    void Start()
    {
        myPosition = gameObject.transform.position;
        Debug.Log(myPosition);

        garden = GameObject.FindGameObjectWithTag(targetTag);

        if (fence != null)
        {
            //make a list with Fence child game objects
            //find the one which child game object is closest to you
            //set that child object as a target
            isFenceAlive = false;
            Vector2 closestTarget =  Vector2.zero;
            float smallestDistanceDifference = Mathf.Infinity;

            foreach (Transform child in fence.transform)
            {
                float distance = Vector2.Distance(myPosition, child.position);
                if (distance < smallestDistanceDifference)
                {
                    smallestDistanceDifference = distance;
                    closestTarget = child.position;
                }
            }

            targetPosition = closestTarget;
            Debug.Log(targetPosition);
        }
        else
        {
            isFenceAlive = true;
            Vector2 closestTarget = Vector2.zero;
            float smallestDistanceDifference = Mathf.Infinity;

            foreach (Transform child in garden.transform)
            {
                float distance = Vector2.Distance(myPosition, child.position);
                if (distance < smallestDistanceDifference)
                {
                    smallestDistanceDifference = distance;
                    closestTarget = child.position;
                }
            }

            targetPosition = closestTarget;
            Debug.Log(targetPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFenceAlive &&  !isGardenAlive)
            return;

        if (isMoving)
            MoveTowardsTarget();
    }

    private void FixedUpdate()
    {
        if (fence == null && garden != null)
            ChangeTarget();

        if (garden == null)
            Debug.Log("All targets destroyed");

        if (CheckInAttackRange() != null && !isAttacking)
        {
            Attack();
        }
    }

    void MoveTowardsTarget()
    {
        float step = speedPoints * Time.deltaTime;
        transform.position = Vector2.MoveTowards(myPosition, targetPosition, step);
    }

    void ChangeTarget()
    {
        myPosition = gameObject.transform.position;
        isFenceAlive = true;

        Vector2 closestTarget = Vector2.zero;
        float smallestDistanceDifference = Mathf.Infinity;

        foreach (Transform child in garden.transform)
        {
            float distance = Vector2.Distance(myPosition, child.position);
            if (distance < smallestDistanceDifference)
            {
                smallestDistanceDifference = distance;
                closestTarget = child.position;
            }
        }

        targetPosition = closestTarget;
    }

    void Attack()
    {
        isMoving = false;
        isAttacking = true;
        Collider2D target = CheckInAttackRange();
        var damageable = target.GetComponent<GardenArea>();
        if (damageable != null)
            damageable.TakeDamage(damagePoints);

        Invoke("ResetAttack", attackSpeed);
    }

    private Collider2D CheckInAttackRange()
    {
        var hits = Physics2D.OverlapCircle(transform.position, attackRange, attackTarget);
        if (hits != null && hits.CompareTag(targetTag))
            return hits;
        
        return null;
    }

    public void PushEnemies()
    {

    }

    public void TakeDamage(float weaponDamage)
    {
        currentHealthPoints -= Mathf.Clamp(currentHealthPoints - weaponDamage, 0, maxHealthPoints);
        if (currentHealthPoints <= 0)
            Die();
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
