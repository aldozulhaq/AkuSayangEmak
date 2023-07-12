using System.Collections;
using UnityEngine;
using Unity.Burst;

namespace Ihaten
{
    public class CharacterController : CharAnim
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float accelSpeed = 10f;
        [SerializeField] private float decelSpeed = 10f;
        [SerializeField] private float rotationSpeed = 100f;
        [SerializeField] private float maxRotationAngle = 24f;
        [SerializeField] private float rotationLerpSpeed = 5f;
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private Transform TailBone;
        Joystick joystick;

        float currentRotationAngle = 0f;
        float lastHorizontalInput = 0f;
        float startRotationAngle = 0f;
        float currentSpeed;
        Vector3 moveDir;
        bool isRotating = false;

        private void Awake()
        {
            joystick = FindObjectOfType<Joystick>();
        }

        [BurstCompile]
        void Update()
        {
            /*float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");*/

            float horizontal = joystick.Horizontal;
            float vertical = joystick.Vertical;

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

            Vector3 movement = moveDir * currentSpeed * Time.deltaTime;
            movement.y = 0f;
            if(transform.position.z > -15)
            {
                transform.position += movement;
            }
            else
            {
                transform.position = new Vector3(transform.position.x,transform.position.y, -14.9f);
            }

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


    }
}
