using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ihaten
{
    public class UIShop : UIAnim
    {
        public Shop shop;
        public GameObject shopItemList;
        public GameObject itemTemplate;
        public Stock selectedStock;
        
        protected override void Awake() {
            CleanChildren();
            base.Awake();
        }

        public override void OnEnable() {
            if(shop != null)
                InstantiateShopItem();
            base.OnEnable();
        }

        public override void StartDisable()
        {
            CleanChildren();
            base.StartDisable();
        }

        void CleanChildren()
        {
            int childrenCount = shopItemList.transform.childCount;

            for(int i = childrenCount - 1; i >= 0; i--)
            {
                DestroyImmediate(shopItemList.transform.GetChild(i).gameObject);
            }
        }

        void InstantiateShopItem()
        {
            Stock tempStock;
            
            foreach(Stock stock in shop.stock)
            {
                tempStock = stock;
                tempStock.item = stock.item;
                tempStock.amount = stock.amount;

                itemTemplate.GetComponent<GenerateShopItem>().SetItem(tempStock);
                Instantiate(itemTemplate, Vector3.zero, Quaternion.identity, shopItemList.transform);
            }
        }
    }
}
