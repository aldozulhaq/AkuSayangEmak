using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ihaten
{
    public class Interactables : MonoBehaviour
    {

        public float radius = 3.0f;
        public GameObject interactBox;
        public Vector3 InteractBoxoffset = new Vector3(0, 6, 0);

        bool isFocused = false;

        protected GameObject player;
        Vector3 objectPos;
        protected GameObject box;
        GameObject WSCanvas;
    
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public virtual void Awake()
        {
            player = FindObjectOfType<CharacterController>().gameObject;
            
            WSCanvas = GameObject.FindGameObjectWithTag("World Space UI");
            
            Vector3 boxPos = transform.position + InteractBoxoffset;
            box = Instantiate(interactBox, boxPos, transform.rotation, WSCanvas.transform);
            box.SetActive(false);
        }

        public virtual void Update()
        {
            Vector3 targetPosition = player.transform.position;
            Vector3 objectPos = transform.position;

            
            if(Vector3.Distance(targetPosition, objectPos) < radius)
            {

                if(!isFocused)
                    toggleActive(true);
                if (Input.GetButtonDown("Interact"))
                    Interact();
            }
            else
            {
                toggleActive(false);
            }
        }

        public virtual void toggleActive(bool status)
        {
            box.GetComponent<InteractBox>().ToggleActive(status);
        }

        public virtual void Interact()
        {
            toggleActive(false);
            isFocused = !isFocused;
        }
    }
}