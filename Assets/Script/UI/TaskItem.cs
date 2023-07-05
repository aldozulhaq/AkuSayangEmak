using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ihaten
{
    public class TaskItem : MonoBehaviour
    {
        [SerializeField] Image icon;
        [SerializeField] TMP_Text amount;
        [SerializeField] TMP_Text itemName;

        public void SetTask(Item _item, int _amount)
        {
            icon.sprite = _item.sprite;
            amount.text = _amount.ToString();
            itemName.text = _item.itemName;
        }
    }
}
