using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Ihaten
{
    public class PriceInputManager : MonoBehaviour
    {
        [SerializeField] GameObject priceInputListParent;
        [SerializeField] GameObject priceInput;
        
        [SerializeField] TMP_InputField grandTotalInputField;

        private UIShop shopUI;
        private TaskManager taskManager;

        int grandTotal;

        private void Awake()
        {
            shopUI = GetComponentInParent<UIShop>();
            taskManager = GameObject.FindObjectOfType<TaskManager>();
        }

        private void OnEnable()
        {
            EventManager.OnClickBuyE += CheckGrandTotal;

            InstantiatePriceInputList();
        }

        private void OnDisable()
        {
            EventManager.OnClickBuyE -= CheckGrandTotal;
        }

        void CleanChildren(GameObject parent)
        {
            int childrenCount = parent.transform.childCount;

            for (int i = childrenCount - 1; i >= 0; i--)
            {
                DestroyImmediate(parent.transform.GetChild(i).gameObject);
            }
        }

        private void InstantiatePriceInputList()
        {
            CleanChildren(priceInputListParent);
            grandTotal = 0;

            List <Stock> selectedStock = shopUI.GetSelectedStocks();

            if (selectedStock.Count == 0)
                return;

            foreach (Stock stock in selectedStock)
            {
                if (taskManager.IsItemInTaskList(stock.item))
                {
                    Task tempTask = taskManager.GetTaskFromItemObject(stock.item);
                    GameObject _tempPriceInput = Instantiate(priceInput, priceInputListParent.transform);
                    _tempPriceInput.GetComponent<PriceInput>().SetComponent(
                        stock.item,
                        stock.item.itemName,
                        tempTask.amount,
                        stock.item.cost);

                    grandTotal += tempTask.amount * stock.item.cost;
                }
            }            
        }

        public void CheckGrandTotal()
        {
            if (grandTotal != int.Parse(grandTotalInputField.text))
            {
                foreach (Stock stock in shopUI.GetSelectedStocks())
                {
                    Task tempTask = taskManager.GetTaskFromItemObject(stock.item);
                    taskManager.SetTaskDone(tempTask, false);
                }
            }
            else
            {
                foreach (Stock stock in shopUI.GetSelectedStocks())
                {
                    Task tempTask = taskManager.GetTaskFromItemObject(stock.item);
                    taskManager.SetTaskDone(tempTask, true);
                }
                EventManager.OnGrandTotalCorrect();
            }
        }
    }
}
