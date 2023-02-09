using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ihaten
{
    [System.Serializable]
    public struct Stock
    {
        public Item item;
        public int amount;
    }

    public class Shop : MonoBehaviour
    {
        public string shopName;
        public Stock[] stock;
    }
}
