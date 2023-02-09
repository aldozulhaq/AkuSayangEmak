using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ihaten
{
    public class InteractBox : UIAnim
    {
        public override void OnEnable()
        {
            StartCoroutine(FadeIn(fadeDuration));
        }
    }
}
