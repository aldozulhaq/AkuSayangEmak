using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ihaten
{
    public class CharAnim : MonoBehaviour
    {

        [SerializeField] private float rotateDuration = 2f;
        bool isFlipping = false;
        [SerializeField] private SpriteRenderer[] playerSR;

        protected void Flip(bool isLeft)
        {
            if (playerSR[0].flipX && isLeft)
            {
                foreach (SpriteRenderer sr in playerSR)
                {
                    sr.flipX = false;
                }
                FlipAnim();
            }
            else if (!playerSR[0].flipX && !isLeft)
            {
                foreach (SpriteRenderer sr in playerSR)
                {
                    sr.flipX = true;
                }
                FlipAnim();
            }
        }


        void FlipAnim()
        {
            if (!isFlipping)
            {
                StartCoroutine(FlipCoroutine());
            }
        }

        IEnumerator FlipCoroutine()
        {
            isFlipping = true;
            float elapsed = 0f; // time elapsed during rotation
            float startAngle = transform.rotation.eulerAngles.y; // starting angle of the rotation
            float endAngle = startAngle + 360f; // ending angle of the rotation

            while (elapsed < rotateDuration)
            {
                float t = elapsed / rotateDuration; // current lerp time
                float angle = Mathf.Lerp(startAngle, endAngle, t); // current rotation angle
                transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, angle, transform.localEulerAngles.z); // set the rotation
                elapsed += Time.deltaTime; // increase the elapsed time
                yield return null;
            }

            transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, 0, transform.localEulerAngles.z); // ensure the final rotation is exact
            isFlipping = false;
        }
    }
}
