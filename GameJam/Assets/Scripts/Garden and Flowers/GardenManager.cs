using UnityEngine;
using System.Collections.Generic;

public class GardenManager : MonoBehaviour
{
    [Header("Attribute")]
    public static GardenManager Instance;
    public List<GardenBed> gardenBeds;

    private void Awake()
    {
        Instance = this;
    }

    public bool HasFreeBed()
    {
        foreach (var bed in gardenBeds)
            if (!bed.isOccupied) return true;
        return false;
    }

    public bool PlantFlowerInFirstFreeBed(GameObject flowerPrefab)
    {
        foreach (var bed in gardenBeds)
        {
            if (!bed.isOccupied)
            {
                GameObject plantedFlower = bed.PlantFlower(flowerPrefab);

                FlowerBase flowerBase = plantedFlower.GetComponent<FlowerBase>();
                if (flowerBase != null)
                {
                    PlayerAttackManager.Instance.ActivateAttack(flowerBase.flowerID);
                }

                return true;
            }
        }
        return false;
    }

    public bool HasFlowerWithID(int flowerID)
    {
        foreach (var bed in gardenBeds)
        {
            if (bed.isOccupied && bed.HasFlowerID(flowerID))
                return true;
        }
        return false;
    }
}