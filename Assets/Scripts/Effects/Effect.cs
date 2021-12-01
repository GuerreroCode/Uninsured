using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Effect
{
    //Names and effetcts for display
    [SerializeField] protected string effectName = "Generic";
    [SerializeField] protected string effectStatus = "None";
    //[SerializeField] protected float maxEffectTime = 0;
    //[SerializeField] protected float curEffectTime = 0;
    [SerializeField] protected Entity entity;
    [SerializeField] protected float effectPercentage;

    public virtual void EffectUpdate()
    {
        /*if (maxEffectTime > 0)
        {
            curEffectTime += Time.deltaTime;
            if (curEffectTime >= maxEffectTime)
              entity.RemoveEffect(this);
        }*/
    }
    public virtual Effect GetActiveEffect()
    {
        return null;
    }

    public virtual Effect GetPassiveEffect()
    {
        return null;
    }

    public virtual bool AddEffect(Entity _entity)
    {
        entity = _entity;
        return true;
    }

    public virtual void RemoveEffect()
    {

    }

    public virtual void Initialize(float percentage)
    {
        effectPercentage = percentage;
    }

    public virtual string GetEffectName()
    {
        return effectName;
    }

    public virtual float GetPercentage()
    {
        return effectPercentage;
    }
}
