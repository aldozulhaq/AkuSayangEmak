using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ihaten
{
    public class UIShop : UIAnim
    {
        public Shop shop;
        public GameObject shopItemList;
        public GameObject itemTemplate;
        public List<Stock> selectedStock;
        public ItemPopupSetup itemPopup;

        [SerializeField] GameObject itemListPanel;
        [SerializeField] GameObject priceInputPanel;

        [SerializeField] Button nextButton;
        [SerializeField] Button prevButton;
        [SerializeField] Button buyButton;

        private PriceInputManager priceInputManager;

        protected override void Awake() {
            nextButton.onClick.AddListener(() => NextClick());
            prevButton.onClick.AddListener(() => PrevClick());
            buyButton.onClick.AddListener(() => BuyClick());

            priceInputManager = GetComponentInChildren<PriceInputManager>();
            CleanChildren(shopItemList);
            base.Awake();
        }

        public override void OnEnable() {
            if(shop != null)
                InstantiateShopItem();
            base.OnEnable();
        }

        public override void StartDisable()
        {
            selectedStock.Clear();
            CleanChildren(shopItemList);

            // Back to base shop menu
            PrevClick();

            base.StartDisable();
        }

        void CleanChildren(GameObject parent)
        {
            int childrenCount = parent.transform.childCount;

            for(int i = childrenCount - 1; i >= 0; i--)
            {
                DestroyImmediate(parent.transform.GetChild(i).gameObject);
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

        public void BuyClick()
        {
            EventManager.OnClickBuy();
            /*itemPopup.gameObject.SetActive(true);
            itemPopup.EnablePopup();*/
        }

        private void NextClick()
        {
            prevButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);

            itemListPanel.SetActive(false);
            priceInputPanel.SetActive(true);
        }

        private void PrevClick()
        {
            prevButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);

            itemListPanel.SetActive(true);
            priceInputPanel.SetActive(false);
        }

        public List<Stock> GetSelectedStocks()
        {
            return selectedStock;
        }
    }
}
