using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBed : MonoBehaviour
{
    [Header("Attribute")]
    public bool isOccupied = false;
    private GameObject currentFlower;

    public GameObject PlantFlower(GameObject flowerPrefab)
    {
        if (isOccupied) return null;
        
        FlowerBase flowerBase = flowerPrefab.GetComponent<FlowerBase>();
        float offsetY = flowerBase != null ? flowerBase.flowerYOffset : 0.5f;

        Vector3 spawnPosition = transform.position + new Vector3(0, offsetY, 0);

        currentFlower = Instantiate(flowerPrefab, spawnPosition, Quaternion.identity);
        isOccupied = true;

        return currentFlower;

    }

    public void RemoveFlower()
    {
        if (currentFlower != null)
        {
            Destroy(currentFlower);
            currentFlower = null;
            isOccupied = false;
        }
    }

    public bool HasFlowerID(int flowerID)
    {
        if (currentFlower == null) return false;

        FlowerBase flowerBase = currentFlower.GetComponent<FlowerBase>();
        return (flowerBase != null && flowerBase.flowerID == flowerID);
    }
}
