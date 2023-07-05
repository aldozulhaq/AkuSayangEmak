using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ihaten
{
    public class EventManager : MonoBehaviour
    {
        public static event Action OnClickBuyE;
        public static event Action OnFinishE;

        public static void OnClickBuy()
        {
            OnClickBuyE?.Invoke();
        }

        public static void OnFinish()
        {
            OnFinishE?.Invoke();
        }
    }
}
