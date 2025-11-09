using UnityEngine;

public class IceFireKiss : MonoBehaviour
{
    [SerializeField] private int minDamage = 2;
    [SerializeField] private int maxDamage = 4;
    [SerializeField] private float projectileSpeed = 10;
    [SerializeField] private float timeToLive = 5;
    private Vector2 dir;
    private GameObject player;
    private CharacterStats characterStats;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        characterStats = player.GetComponent<CharacterStats>();
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
            enemy.TakeDamage(damage * characterStats.playerBaseAttackDmg);
        }
    }
}
