using System.Collections;
using UnityEngine;

public class AllAroundStormScript : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int bulletCount = 360;
    [SerializeField] private float spawnDelay = 0.03f;
    [SerializeField] private float cooldown = 5f;

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

        if (timer >= cooldown / characterStats.playerAttackSpeed)
        {
            timer = 0;
            StartCoroutine(ShootCircle());
        }
    }

    private IEnumerator ShootCircle()
    {
        float angleStep = 360f / bulletCount;
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float rad = angle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            proj.transform.rotation = Quaternion.Euler(0, 0, angle);

            var projScript = proj.GetComponent<SmallBulletScript>();
            if (projScript != null)
            {
                projScript.SetDir(dir.normalized);
            }

            angle += angleStep;

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
