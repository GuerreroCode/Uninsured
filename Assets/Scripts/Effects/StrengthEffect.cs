using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthEffect : Effect
{
  //speed 20% less

  public override bool AddEffect(Entity _entity)
  {
      if(_entity.SetStrengthPercentage(effectPercentage))
      {
          entity = _entity;
          return true;
      }

      return false;
  }

  public override void RemoveEffect()
  {
      entity.SetStrengthPercentage(-effectPercentage);
  }

  public override void Initialize(float percentage)
  {
      base.Initialize(percentage);
      effectName = "Strength";
      float displayPercentage = effectPercentage * 100f;
      effectStatus = "Strength: " + displayPercentage;

  }
}
