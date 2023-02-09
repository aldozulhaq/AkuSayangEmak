using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ihaten
{
    public class UIAnim : MonoBehaviour
    {
        protected Image[] images;
        public float fadeDuration = 1.0f;
        public float moveAmount = 10.0f;
        public AnimationCurve fadeCurve;

        private Vector3[] startingPosition;
        private Vector3[] startingDownPosition;
        private Color[] originalColor;

        protected virtual void Awake() {
            images = GetComponentsInChildren<Image>();

            startingPosition = new Vector3[images.Length];
            startingDownPosition = new Vector3[images.Length];
            originalColor = new Color[images.Length];

            for(int i = 0; i < images.Length; i++)
            {
                startingPosition[i] = images[i].rectTransform.localPosition;
                images[i].rectTransform.localPosition = startingPosition[i] - new Vector3(0, moveAmount, 0);
                startingDownPosition[i] = startingPosition[i];
                startingDownPosition[i].y += moveAmount;

                originalColor[i] = images[i].color;
            }
        }

        public virtual void OnEnable()
        {
            StartCoroutine(FadeIn(fadeDuration));
        }

        public virtual void StartDisable()
        {
            if(gameObject.activeSelf)
                StartCoroutine(FadeOut(fadeDuration));
        }

        public virtual void ToggleActive(bool status)
        {
            if(status)
                gameObject.SetActive(true);
            else
                StartDisable();
        }

        protected IEnumerator FadeIn(float duration)
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = fadeCurve.Evaluate(elapsedTime / duration);
                float alpha = Mathf.Lerp(0, 1, elapsedTime / duration);
                float move = Mathf.Lerp(moveAmount, 0, t);
                for(int i = 0; i < images.Length; i++)
                {
                    images[i].color = new Color(originalColor[i].r, originalColor[i].g, originalColor[i].b, alpha);
                    images[i].rectTransform.localPosition = startingPosition[i] - new Vector3(0, move, 0);
                }
                yield return null;
            }
        }

        protected IEnumerator FadeOut(float duration)
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = fadeCurve.Evaluate(elapsedTime / duration);
                float alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
                float move = Mathf.Lerp(0, moveAmount, t);
                for(int i = 0; i < images.Length; i++)
                {
                    images[i].color = new Color(originalColor[i].r, originalColor[i].g, originalColor[i].b, alpha);
                    images[i].rectTransform.localPosition = startingPosition[i] - new Vector3(0, move, 0);
                }
                yield return null;
            }
             gameObject.SetActive(false);
        }
    }
}
