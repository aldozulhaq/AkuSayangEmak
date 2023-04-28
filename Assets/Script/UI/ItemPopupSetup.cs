using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ihaten
{
    public class ItemPopupSetup : MonoBehaviour
    {
        public Stock selectedStock;
        public Image icon;
        public string itemName;
        public string stockAmount;


        private void Awake()
        {
            
        }

        public void EnablePopup()
        {
            updateContent();
            gameObject.SetActive(true);
        }

        void updateContent()
        {
            updateStock();
            itemName = selectedStock.item.itemName;
            icon.sprite = selectedStock.item.sprite;
        }

        public void setStock(Stock stock)
        {
            selectedStock = stock;
        }    

        public void updateStock()
        {
            stockAmount = selectedStock.amount.ToString();
        }
    }
}
