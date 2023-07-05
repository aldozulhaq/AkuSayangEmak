using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Ihaten
{
    public class NPC_Mom : Interactables
    {
        CinemachineVirtualCamera stallCam;
        bool isActive = false;

        [SerializeField] UITask taskUI;
        // Start is called before the first frame update
        void Start()
        {
            stallCam = transform.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
            stallCam.Priority = 0;
        }

        public override void Interact()
        {
            base.Interact();

            isActive = !isActive;
            stallCam.Priority = isActive ? 1 : 0;
            player.GetComponent<CharacterController>().enabled = !isActive;

            taskUI.ToggleActive(isActive);
        }
    }
}
