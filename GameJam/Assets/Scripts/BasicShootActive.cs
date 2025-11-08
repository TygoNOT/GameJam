using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class BasicShootActive : MonoBehaviour
{
    [SerializeField] private GameObject iceFirePrefab;
    private float cooldown = 3;
    private float timer;
    void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        if (timer > cooldown)
        {
            timer = 0;
            ShootFireKiss();
        }

        timer += Time.deltaTime;
    }

    void ShootFireKiss()
    {
        Vector2 currentPlayerPos = this.transform.position;
        Vector2[] dirs = {
            new Vector2(1, 1).normalized,
            new Vector2(-1, 1).normalized,
            new Vector2(1, -1).normalized,
            new Vector2(-1, -1).normalized
        };
        foreach (var dir in dirs)
        {
            GameObject iceFire = Instantiate(iceFirePrefab, currentPlayerPos, Quaternion.identity);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            iceFire.transform.rotation = Quaternion.Euler(0, 0, angle);

            IceFireKiss iceFireKiss = iceFire.GetComponent<IceFireKiss>();
            iceFireKiss.SetDir(dir);
        }
    }


    
}
