using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMages.Utility
{
    public class GameObjectTracker : MonoBehaviour
    {
        [SerializeField]
        [Header("Tracking Settings")]
        [Tooltip("Object being tracked by the camera.")]
        private GameObject tracked;

        [SerializeField]
        [Tooltip("Offset from the final position the camera will track to.")]
        private Vector3 trackingOffset;

        [SerializeField]
        [Tooltip("Speed the camera will track with. Larger == Faster")]
        [Range(0, 1)]
        private float trackingSpeed;

        // Update is called once per frame
        void Update()
        {
            Vector3 desiredPosition = tracked.transform.position + trackingOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, trackingSpeed);
            transform.position = smoothedPosition;
        }
    }
}
