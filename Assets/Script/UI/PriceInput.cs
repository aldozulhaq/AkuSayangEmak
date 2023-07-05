using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Ihaten
{
    public class PriceInput : MonoBehaviour
    {
        [SerializeField] TMP_Text itemName;
        [SerializeField] TMP_Text quantity;
        [SerializeField] TMP_Text price;
        [SerializeField] TMP_InputField totalInputField;
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
            this.quantity.text = quantity.ToString();
            this.price.text = price.ToString();

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
