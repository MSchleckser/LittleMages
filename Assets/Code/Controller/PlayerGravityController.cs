using Assets.Code.EventSytem;
using UnityEngine;

namespace Assets.Code.Controller
{
    class PlayerGravityController : MonoBehaviour
    {
        private CharacterController characterController;

        public void Awake()
        {
            characterController = gameObject.GetComponent<CharacterController>();
            PlayerFallingEventSystem.RegisterListener(Falling);
        }

        private void Falling(Vector3 distanceAndDirectionToGround)
        {
            if (distanceAndDirectionToGround.magnitude > Physics.gravity.magnitude)
                characterController.Move(Physics.gravity);

            characterController.Move(distanceAndDirectionToGround);
        }
    }
}
