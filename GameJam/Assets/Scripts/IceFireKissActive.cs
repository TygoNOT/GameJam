using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFireKissActive : MonoBehaviour
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
        Vector2[] dirs = { Vector2.right, Vector2.up, Vector2.down, Vector2.left };
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
