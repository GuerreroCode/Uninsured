using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SpeedEffect : Effect
{
    public override bool AddEffect(Entity _entity)
    {
        if (_entity.SetSpeedPercentage(effectPercentage))
        {
            entity = _entity;
            return true;
        }

        return false;

    }

    public override void RemoveEffect()
    {
        entity.SetSpeedPercentage(-effectPercentage);
    }

    public override void Initialize(float percentage)
    {
        base.Initialize(percentage);
        effectName = "Speed";
        float displayPercentage = effectPercentage * 100f;
        effectStatus = "Speed: " + displayPercentage;

    }
}
