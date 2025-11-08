using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenArea : DefenseArea
{
    [Header("Beds inside the garden")]
    public GardenBed[] gardenBeds;

    private void Start()
    {
        base.Start();

        if (gardenBeds == null || gardenBeds.Length == 0)
            gardenBeds = GetComponentsInChildren<GardenBed>();
    }

    protected override void OnDestroyed()
    {

        foreach (GardenBed bed in gardenBeds)
        {
            if (bed != null)
                Destroy(bed.gameObject);
        }

        base.OnDestroyed(); 
    }
}
