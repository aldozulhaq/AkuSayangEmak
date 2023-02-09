using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ihaten
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        public string itemName;
        public int cost;
        public Sprite sprite;
    }
}
