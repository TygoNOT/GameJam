using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBed : MonoBehaviour
{
    [Header("Attribute")]
    public bool isOccupied = false;
    private GameObject currentFlower;

    public void PlantFlower(GameObject flowerPrefab)
    {
        if (isOccupied) return;
        
        FlowerBase flowerBase = flowerPrefab.GetComponent<FlowerBase>();
        float offsetY = 0.5f; 
        if (flowerBase != null)
            offsetY = flowerBase.flowerYOffset;

        Vector3 spawnPosition = transform.position + new Vector3(0, offsetY, 0);

        currentFlower = Instantiate(flowerPrefab, spawnPosition, Quaternion.identity);
        isOccupied = true;
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
}
