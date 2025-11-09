using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{

    [SerializeField] private float speedPoints;
    [SerializeField] public float speedAmp = 1;
    [SerializeField] private int damagePoints;
    [SerializeField] private float maxHealthPoints;
    private float currentHealthPoints;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private GameObject test;
    [SerializeField] private Animator _animator;
    private bool isFenceAlive = false;
    private bool isGardenAlive;
    private bool isMoving = true;
    private bool isAttacking = false;
    private bool targetChanged = false;

    private Vector2 myPosition;
    private Vector2 targetPosition;

    private GameObject fence;
    public GameObject garden;
    [SerializeField] private LayerMask attackTarget;
    private string targetTagFence = "DefenseArea";
    private string targetTagGarden = "GardenArea";

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

        garden = GameObject.FindGameObjectWithTag(targetTagGarden);
        fence = GameObject.FindGameObjectWithTag(targetTagFence);
        Debug.Log(garden);
        Debug.Log(fence);

        if (fence != null)
        {
            //make a list with Fence child game objects
            //find the one which child game object is closest to you
            //set that child object as a target
            isFenceAlive = true;
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
            isFenceAlive = false;
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
        {
            MoveTowardsTarget();
            _animator.SetBool("isMoving", true);
        }
        else _animator.SetBool("isMoving", false);
    }

    private void FixedUpdate()
    {
        if (fence == null && garden != null && !targetChanged)
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
        myPosition = gameObject.transform.position;
        float step = speedPoints * Time.deltaTime * speedAmp;
        transform.position = Vector2.MoveTowards(myPosition, targetPosition, step);
    }

    void ChangeTarget()
    {
        targetChanged = true;
        myPosition = gameObject.transform.position;
        isFenceAlive = true;
        isMoving = true;

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
        var damageable = target.GetComponent<DefenseArea>();
        if (damageable != null)
        {
            damageable.TakeDamage(damagePoints);
        }

        Invoke("ResetAttack", attackSpeed);
    }

    private Collider2D CheckInAttackRange()
    {
        var hits = Physics2D.OverlapCircle(transform.position, attackRange, attackTarget);
        if (hits != null && (hits.CompareTag(targetTagFence) || hits.CompareTag(targetTagGarden)))
        {
            return hits;
        }
            
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
