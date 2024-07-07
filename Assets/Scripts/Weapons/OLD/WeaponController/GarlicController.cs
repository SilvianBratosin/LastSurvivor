using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : WeaponsController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Atack()
    {
        base.Atack();
        GameObject spawnedGarlic = Instantiate(weaponData.Prefab);
        spawnedGarlic.transform.position = transform.position;
        spawnedGarlic.transform.parent = transform;
    }
}
