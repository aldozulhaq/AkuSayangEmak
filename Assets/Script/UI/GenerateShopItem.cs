using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ihaten
{
    public class GenerateShopItem : MonoBehaviour
    {
        public Stock stock;
        public Image icon;
        public TMP_Text amount;
        public TMP_Text itemName;
        public TMP_Text price;
        public Image panel;

        UIShop uishop;
        Color baseColor;
        public Color selectedColor;

        public void SetItem(Stock stockItem)
        {
            stock = stockItem;
            icon.sprite = stock.item.sprite;
            amount.text = stock.amount.ToString();
            itemName.text = stock.item.name;
            price.text = "Rp" + stock.item.cost.ToString();
        }

        private void Awake() {
            uishop = GameObject.FindAnyObjectByType<UIShop>().GetComponent<UIShop>();
            baseColor = panel.color;
        }

        public void click()
        {
            if (!uishop.selectedStock.Contains(stock))
            {
                uishop.selectedStock.Add(stock);
                panel.color = selectedColor;
            }
            else
            {
                uishop.selectedStock.Remove(stock);
                panel.color = baseColor;
            }
        }
    }
}
