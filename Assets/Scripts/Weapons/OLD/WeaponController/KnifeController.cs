using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponsController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Atack()
    {
        base.Atack();
        GameObject knife = Instantiate(weaponData.Prefab);
        knife.transform.position = transform.position;
        knife.GetComponent<KnifeBehavior>().DirectionChecker(pm.lastMoved);
    }
}
