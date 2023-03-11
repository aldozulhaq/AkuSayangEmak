using System.Collections;
using UnityEngine;
using Unity.Burst;

namespace Ihaten
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float accelSpeed = 10f;
        [SerializeField] private float decelSpeed = 10f;
        [SerializeField] private float rotateDuration = 2f;
        [SerializeField] private float rotationSpeed = 100f;
        [SerializeField] private float maxRotationAngle = 24f;
        [SerializeField] private float rotationLerpSpeed = 5f;
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private SpriteRenderer[] playerSR;
        [SerializeField] private Transform TailBone;

        float currentRotationAngle = 0f;
        float lastHorizontalInput = 0f;
        float startRotationAngle = 0f;
        float currentSpeed;
        Vector3 moveDir;
        bool isFlipping = false;
        bool isRotating = false;

        [BurstCompile]
        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            moveDir = new Vector3(horizontal, 0f, vertical).normalized;
            
            if (moveDir.magnitude > 0f) // if the character is moving
            {
                playerAnimator.SetBool("Walk", true);
                currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed, accelSpeed * Time.deltaTime);
            }
            else // if the character is not moving
            {
                playerAnimator.SetBool("Walk", false);
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, decelSpeed * Time.deltaTime);
            }

            if(moveDir.x < 0) //facing left
            {
                Flip(true);
            }
            else if (moveDir.x > 0)
            {
                Flip(false);
            }
            
            //Debug.Log(currentSpeed / moveSpeed);
            //Debug.Log(Mathf.Abs(horizontal));

            Vector3 movement = moveDir * currentSpeed * Time.deltaTime;
            movement.y = 0f;
            transform.position += movement;

            if (Mathf.Abs(horizontal) > 0f)
            {
                RotateOnZAxis(horizontal * rotationSpeed);
                lastHorizontalInput = Mathf.Sign(horizontal);
            }
            else if (Mathf.Abs(vertical) > 0f)
            {
                RotateOnZAxis(lastHorizontalInput * Mathf.Abs(vertical) * rotationSpeed);
            }
            else if(isRotating)
            {
                InterpolateBackToStartRotation();
            }

        }

        void RotateOnZAxis(float rotationAmount)
        {
            currentRotationAngle = Mathf.Clamp(currentRotationAngle + rotationAmount * Time.deltaTime, -maxRotationAngle, maxRotationAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, -currentRotationAngle), rotationLerpSpeed * Time.deltaTime);
            isRotating = true;
        }

        private void InterpolateBackToStartRotation()
        {
            float t = Mathf.Clamp01(rotationLerpSpeed * Time.deltaTime);
            currentRotationAngle = Mathf.Lerp(currentRotationAngle, startRotationAngle, t);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, currentRotationAngle), t);
            if (Mathf.Approximately(currentRotationAngle, startRotationAngle))
            {
                isRotating = false;
            }
        }

        void Flip(bool isLeft)
        {
            if(playerSR[0].flipX && isLeft)
            {
                foreach(SpriteRenderer sr in playerSR)
                    {
                        sr.flipX = false;
                    }
                FlipAnim();
            }
            else if(!playerSR[0].flipX && !isLeft)
            {
                foreach(SpriteRenderer sr in playerSR)
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
