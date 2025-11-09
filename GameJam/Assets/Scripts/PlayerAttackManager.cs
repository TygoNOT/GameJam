using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public static PlayerAttackManager Instance;

    [System.Serializable]
    public class AttackLink
    {
        public int flowerID;
        public MonoBehaviour attackScript;
        public GameObject attackObject;
    }
    [Header("Attack Bindings")]
    public AttackLink[] attacks;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var attack in attacks)
        {
            if (attack.attackScript != null)
                attack.attackScript.enabled = false;

            if (attack.attackObject != null)
                attack.attackObject.SetActive(false);
        }
    }

    public void ActivateAttack(int flowerID)
    {
        foreach (var attack in attacks)
        {
            if (attack.flowerID == flowerID)
            {
                if (attack.attackScript != null)
                    attack.attackScript.enabled = true;

                if (attack.attackObject != null)
                    attack.attackObject.SetActive(true);

                Debug.Log($"Активирована атака для FlowerID {flowerID}");
                return;
            }
        }

        Debug.LogWarning($"Не найдено атаки для FlowerID {flowerID}");
    }
}
