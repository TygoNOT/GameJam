using UnityEngine;

public class superDuperBaicWeapon : MonoBehaviour
{
    [SerializeField] private GameObject basicBullet;
    [SerializeField] private float radius = 6f;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float cooldown = 1f;

    private float timer;
    private GameObject player;
    private CharacterStats characterStats;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        characterStats = player.GetComponent<CharacterStats>();
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > cooldown / characterStats.playerAttackSpeed)
            DoDamage();
    }

    void DoDamage()
    {
        timer = 0;
        var closestEnemy = Physics2D.OverlapCircle(transform.position, radius, enemyMask);
        if (closestEnemy != null)
        {
            var enemy = closestEnemy.GetComponent<BaseEnemyScript>();

            Vector2 currentPlayerPos = transform.position;
            Vector2 enemyPos = enemy.transform.position;

            GameObject bullet = Instantiate(basicBullet, currentPlayerPos, Quaternion.identity);
            Vector2 dir = (enemyPos - currentPlayerPos).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

            IceFireKiss bullet1 = bullet.GetComponent<IceFireKiss>();
            bullet1.SetDir(dir);
        }
    }
}
