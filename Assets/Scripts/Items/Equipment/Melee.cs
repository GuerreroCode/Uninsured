using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Equip
{
    [SerializeField] private Collider2D meleeCol;
    [SerializeField] private float strengthMultiplier;
    [SerializeField] private Animator anim;

    void Start()
    {
        meleeCol.gameObject.layer = this.gameObject.layer;
    }

    public override void Release()
    {
        meleeCol.gameObject.SetActive(false);
        anim.Play("Idle");
    }

    public override bool Shoot(Vector2 lookDir)
    {
        meleeCol.gameObject.SetActive(true);
        anim.Play("Swing");
        return true;
    }

    public float GetMeleeDamage()
    {
        return (strengthMultiplier + entity.GetStrength());
    }


}
