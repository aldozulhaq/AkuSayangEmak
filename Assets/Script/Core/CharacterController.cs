using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ihaten
{
    public class CharacterController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float rotateSpeed = 180f;
        public float bgLimit = 2f;
        public float fgLimit = -12f;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontal, 0f, vertical);
            movement = transform.position + movement * moveSpeed * Time.deltaTime;

            movement.z = Mathf.Clamp(movement.z, fgLimit, bgLimit);
            rb.MovePosition(movement);
        }
    }
}
