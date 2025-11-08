using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{

    private float speedPoints;
    private float damagePoints;
    private float maxHealthPoints;
    private float currentHealthPoints;
    private float attackRange;
    private bool isFenceAlive = false;
    private bool isGardenAlive;
    private bool isMoving;

    private Vector2 myPosition;
    private Vector2 targetPosition;

    private GameObject fence;
    private GameObject garden;
    private LayerMask attackTarget;

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
        if (fence == null && isGardenAlive != true)
            ChangeTarget();

        if (CheckInAttackRange() != null)
            Attack();
            isMoving = false;
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
        Collider2D target = CheckInAttackRange();
        //take the class from target and use the takedamage method
    }

    private Collider2D CheckInAttackRange()
    {
        var hits = Physics2D.OverlapCircle(transform.position, attackRange, attackTarget);
        if (hits != null)
            return hits;
        else
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

    void Die()
    {
        //I die
    }
}
