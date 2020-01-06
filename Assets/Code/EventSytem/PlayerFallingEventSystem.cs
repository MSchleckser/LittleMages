using System;
using UnityEngine;

namespace Assets.Code.EventSytem
{
    class PlayerFallingEventSystem : MonoBehaviour
    {
        private static event Action<Vector3> Listeners;

        public static void RegisterListener(Action<Vector3> listener)
        {
            Listeners += listener;
        }

        public static void DeregisterListener(Action<Vector3> listener)
        {
            Listeners -= listener;
        }

        [SerializeField]
        private GameObject player;

        private CapsuleCollider playerCollider;
        private Vector3 lastPosition = new Vector3(0, float.MinValue);
        private float skinTolerance;

        public void Awake()
        {
            playerCollider = player.GetComponent<CapsuleCollider>();
            skinTolerance = player.GetComponent<CharacterController>().skinWidth + playerCollider.center.y;
        }

        public void Update()
        {
            Vector3 currentPosition = player.transform.position;

            float deltaHeight = GetDeltaHeight(currentPosition, lastPosition);
            Vector3 playerOrigin = playerCollider.center + currentPosition;

            RaycastHit hitInfo = GetDistanceToGround(playerOrigin);

            if (IsTriggered(hitInfo.distance, deltaHeight))
            {
                Vector3 distanceAndDirectionToGround = hitInfo.point - playerOrigin;
                Listeners(distanceAndDirectionToGround);
            }

            lastPosition = currentPosition;
        }

        private bool IsTriggered(float distanceToGround, float deltaHeight)
        {
            return distanceToGround > skinTolerance; 
        }

        private RaycastHit GetDistanceToGround(Vector3 colliderOrigin)
        {
            Vector3 rayDirection = Physics.gravity.normalized * skinTolerance;
            Physics.Raycast(colliderOrigin, rayDirection, out RaycastHit hitInfo);
            return hitInfo;
        }

        private float GetDeltaHeight(Vector3 currentPosition, Vector3 lastPosition)
        {
            return lastPosition.y == float.MinValue ? 0 : currentPosition.y - lastPosition.y;
        }
    }
}
