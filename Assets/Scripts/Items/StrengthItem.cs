using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthItem : Item
{
    // Start is called before the first frame update
    void Start()
    {
        float percentage = effect.GetPercentage();
        effect = new StrengthEffect();
        effect.Initialize(percentage);
    }

}
