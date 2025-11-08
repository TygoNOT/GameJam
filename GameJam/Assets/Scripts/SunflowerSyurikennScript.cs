using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunflowerSyurikennScript : MonoBehaviour
{
    public Transform player;        // The player to orbit
    public float orbitSpeed = 180f; // Degrees per second
    public float orbitRadius = 2f;  // Distance from player
    public float selfRotateSpeed = 90f; // Spin speed (degrees per second)
    private int minDamage = 3;
    private int maxDamage = 10;

    private float angle; // Current orbit angle
    private string enemyTag = "Enemy";

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;

        // Calculate initial angle based on starting position
        Vector2 offset = transform.position - player.position;
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        // Update orbit angle over time
        angle += orbitSpeed * Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;

        // Compute orbit position (fixed radius)
        Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * orbitRadius;
        transform.position = (Vector2)player.position + offset;

        // Rotate the object around its own axis
        transform.Rotate(Vector3.forward * selfRotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag))
        {
            Debug.Log("Doing some damage");
            int damage = UnityEngine.Random.Range(minDamage, maxDamage);
            BaseEnemyScript enemy = collision.GetComponent<BaseEnemyScript>();
            enemy.TakeDamage(damage);
        }
    }
}
