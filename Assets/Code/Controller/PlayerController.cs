using UnityEngine;
using Assets.Code.EventSytem;

namespace Assets.Code.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private Transform cameraTransform;

        [SerializeField]
        private float rotationSmoothness;

        private CharacterController characterController;

        private void Awake()
        {
            characterController = gameObject.GetComponent<CharacterController>();
            WasdEventSytem.RegisterListener(Move);
        }

        private void LookTowardsCamera(Transform transform, Transform cameraTransform)
        {
            Vector2 currentRotation = transform.eulerAngles;
            currentRotation.y = Mathf.LerpAngle(currentRotation.y,
                cameraTransform.rotation.eulerAngles.y, Time.deltaTime * rotationSmoothness);
            transform.eulerAngles = currentRotation;
        }

        private void Move(Vector3 data)
        {
            Vector3 velocity =  data * speed * Time.deltaTime;
            velocity = transform.TransformDirection(velocity);
            characterController.Move(velocity);

            LookTowardsCamera(transform, cameraTransform);
        }
    }
}