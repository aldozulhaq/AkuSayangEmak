using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ihaten
{
    public class UITutorial : MonoBehaviour
    {
        [SerializeField] private Button lanjutButton;
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] float buttonShowTime = 3f;

        private void Awake()
        {
            lanjutButton.onClick.AddListener(ClosePanel);
        }

        private void Start()
        {
            OpenPanel();
            lanjutButton.gameObject.SetActive(false);
        }

        private void OpenPanel()
        {
            tutorialPanel.SetActive(true);
            StartCoroutine(ShowButtonCoroutine());
        }

        private void ClosePanel()
        {
            tutorialPanel.SetActive(false);
        }

        private IEnumerator ShowButtonCoroutine()
        {
            yield return new WaitForSeconds(buttonShowTime);

            lanjutButton.gameObject.SetActive(true);
        }
    }
}
