using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    [SerializeField] private Equip equip;
    [SerializeField] private int ammo;

    private void OnTriggerEnter2D(Collider2D coll)
    {

        Entity entity = coll.gameObject.GetComponent<Entity>();

        if (entity != null)
        {
            entity.PickupEquipment(equip, ammo);
            Destroy(this.gameObject);
        }
    }

    public void SetAmmo(int _ammo)
    {
        ammo = _ammo;
    }

    public int GetAmmo()
    {
        return ammo;
    }

    public Equip GetEquipPrefab()
    {
        return equip;
    }



}
