using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFireKiss : MonoBehaviour
{
    [SerializeField] private int minDamage = 2;
    [SerializeField] private int maxDamage = 4;
    [SerializeField] private float projectileSpeed = 10;
    [SerializeField] private float timeToLive = 5;
    private Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    public void SetDir(Vector2 newDir)
    {
        dir = newDir.normalized;   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * projectileSpeed * Time.deltaTime, Space.World);  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            int damage = UnityEngine.Random.Range(minDamage, maxDamage+1);
            BaseEnemyScript enemy = collision.GetComponent<BaseEnemyScript>();
            enemy.TakeDamage(damage);
        }
    }
}
