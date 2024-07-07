using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [Header("Weapons Stats")]
    public WeaponScriptableObject weaponData;
    float currentCD;

    protected PlayerMovement pm;

    protected virtual void Start()
    {
        pm = FindAnyObjectByType<PlayerMovement>();
        currentCD = weaponData.CdDuration;
    }


    protected virtual void Update()
    {
        currentCD -= Time.deltaTime;
        if(currentCD <= 0f)
        {
            Atack();
        }
    }

    protected virtual void Atack()
    {
        currentCD = weaponData.CdDuration;
    }
}
