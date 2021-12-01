using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEffect : Effect
{

  public override bool AddEffect(Entity _entity)
  {
      if (_entity.SetHealthPercentage(effectPercentage))
      {
          base.AddEffect(_entity);
          return true;
      }

      return false;

  }

  public override void RemoveEffect()
  {
      entity.SetHealthPercentage(-effectPercentage);
  }

  public override void Initialize(float percentage)
  {
      base.Initialize(percentage);
      effectName = "Health";
      float displayPercentage = effectPercentage * 100f;
      effectStatus = "health: " + displayPercentage;

  }
}
