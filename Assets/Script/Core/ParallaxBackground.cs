using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ihaten
{
    public class ParallaxBackground : MonoBehaviour
    {
        private float length, startPos;
        [SerializeField] GameObject camera;
        [SerializeField] float parallaxEffectAmount;

        private void Start()
        {
            startPos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void Update()
        {
            float distance = (camera.transform.position.x * parallaxEffectAmount);

            transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        }
    }
}
