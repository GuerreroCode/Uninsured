using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    //[SerializeField] private Rigidbody2D rb;
    [SerializeField] protected Entity entity;
    [SerializeField] protected Pickup pickup;
    [SerializeField] protected int ammo = 10; // health for melee, ammo for guns
    [SerializeField] protected float weaponAttackTime = 0f;
    //[SerializeField] private GameObject handCollider;


    // Start is called before the first frame update
    void Start()
    {

    }


/*
    public void Rotate(float angle)
    {
        rb.rotation = angle;
    }
    public void SetVelocity(Vector2 position)
    {
        rb.velocity += position;
    }

    public void SetColliderActive(bool _active)
    {
        handCollider.SetActive(_active);
    }
*/
    public void Follow(Transform entity)
    {
        this.transform.position = entity.position;
    }

    public virtual void Release()
    {

    }

    //Returns is melee
    public virtual bool Shoot(Vector2 lookDir)
    {
        return false;
    }

    public void SetAmmo(int _ammo)
    {
        ammo = _ammo;
    }

    public int GetAmmo()
    {
        return ammo;
    }

    public Pickup GetPickup()
    {
        return pickup;
    }

    //shotgun
    //num Spread
    //spawn bullets from left to right
    //set the direction of the bullet equal to (lookDir - ((numSpread - 1) * bulletDeltaOffset))
    public static Vector2 Rotate(Vector2 v, float delta) {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }


    public virtual float GetAttackTime()
    {
        return weaponAttackTime;
    }
    
    public void SetEntity(Entity _entity)
    {
        entity = _entity;
    }





    public Vector2 GetAim()
    {
        return entity.GetAim();
    }

    public float GetStrength()
    {
        return entity.GetStrength();
    }
}
