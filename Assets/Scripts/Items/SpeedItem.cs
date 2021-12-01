using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : Item
{
  void Start()
  {
      float percentage = effect.GetPercentage();
      effect = new SpeedEffect();
      effect.Initialize(percentage);
  }
}
