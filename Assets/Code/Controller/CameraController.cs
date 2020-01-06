using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Controllers
{
    public class CameraController : MonoBehaviour
    {

        [SerializeField]
        private Vector2 rotationSpeed;

        [SerializeField]
        private bool invertY;

        [SerializeField]
        private float upperRotationLimit;

        [SerializeField]
        private float lowerRotationLimit;

        private float localXRotation = 0.0f;

        private void OnEnable()
        {
            localXRotation = transform.localEulerAngles.x;
        }

        // Update is called once per frame
        void Update()
        {
            if (!Input.GetMouseButton(1))
            {
                OnRightMouseUp();
                return;
            }

            OnRightMouseDown();

            Vector2 deltaMouse = GetDeltaMouse() * rotationSpeed * Time.deltaTime;
            RotateHorizontally(transform, deltaMouse.x);
            RotateVertically(transform, ref localXRotation, deltaMouse.y, lowerRotationLimit, upperRotationLimit);
        }

        private Vector2 GetDeltaMouse()
        {
            return new Vector2
            {
                x = Input.GetAxisRaw("Mouse X"),
                y = Input.GetAxisRaw("Mouse Y")
            };
        }

        private void RotateHorizontally(Transform transform, float deltaHorizontalRotation)
        {
            Vector3 horizontal = new Vector3(0, deltaHorizontalRotation, 0);
            transform.Rotate(horizontal, Space.World);
        }

        private void RotateVertically(Transform transform, ref float currentRotation, float deltaVerticalRotation,
            float lowerBounds, float upperBounds)
        {
            deltaVerticalRotation *= invertY ? -1.0f : 1.0f;
            currentRotation += deltaVerticalRotation;
            currentRotation = Mathf.Clamp(currentRotation, lowerBounds, upperBounds);

            Vector3 nRotation = transform.localEulerAngles;
            nRotation.x = currentRotation;

            transform.localEulerAngles = nRotation;
        }

        private void OnRightMouseDown()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnRightMouseUp()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }
}