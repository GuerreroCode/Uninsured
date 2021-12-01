using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeChild : MonoBehaviour
{
    [SerializeField] Melee melee;

    public float GetMeleeDamage()
    {
        return melee.GetMeleeDamage();
    }
}
