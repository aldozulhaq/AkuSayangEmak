using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ihaten
{
    public class HouseManager : MonoBehaviour
    {
        public BoxCollider livingRoomCollider;
        public BoxCollider bedroomCollider;
        public Renderer livingRoom;
        public Renderer bedroom;
        public Transform player;

        // Start is called before the first frame update
        void Start()
        {
            //livingRoom.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            BoundRenderer(livingRoomCollider, livingRoom);
            BoundRenderer(bedroomCollider, bedroom);
        }

        void BoundRenderer(Collider col, Renderer ren)
        {
            if(col.bounds.Contains(player.position))
            {
                ren.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }
            else
            {
                ren.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;
            }
        }
    }
}
