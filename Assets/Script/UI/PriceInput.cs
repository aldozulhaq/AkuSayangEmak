using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ihaten
{
    public class PriceInput : MonoBehaviour
    {
        [SerializeField] TMP_Text itemName;
        [SerializeField] TMP_Text priceTimesQuantity;
        [SerializeField] TMP_InputField totalInputField;
        [SerializeField] Image itemIcon;

        private Item itemObject;
        int totalPrice = 0;

        TaskManager taskManager;

        private void Awake()
        {
            taskManager = GameObject.FindObjectOfType<TaskManager>();
            totalInputField.onValueChanged.AddListener((string newText) => { 
                CheckTotal(newText); 
            });
        }

        public void SetComponent(Item item, string itemName, int quantity, int price)
        {
            itemObject = item;
            this.itemName.text = itemName;
            this.priceTimesQuantity.text = quantity.ToString() + " x " + price.ToString();
            itemIcon.sprite = item.sprite;

            totalPrice = quantity * price;
        }

        private void CheckTotal(string text)
        {
            int value = int.Parse(text);
            if (value == totalPrice)
            {
                taskManager.SetTaskDone(taskManager.GetTaskFromItemObject(itemObject), true);
            } else
            {
                taskManager.SetTaskDone(taskManager.GetTaskFromItemObject(itemObject), false);
            }
        }

        public int GetTotalPrice()
        {
            return totalPrice;
        }
    }
}
