using System;
using UnityEngine;

namespace Assets.Code.EventSytem
{
    class WasdEventSytem : MonoBehaviour
    {
        private static event Action<Vector3> Callbacks = delegate { };
        private Vector3 data;


        private bool IsTriggered()
        {
            data.x = Input.GetAxisRaw("Horizontal");
            data.z = Input.GetAxisRaw("Vertical");
            data = data.normalized;

            return data.magnitude != 0;
        }

        public void Update()
        {
            if (!IsTriggered())
                return;

            WasdEventSytem.Callbacks(data);
        }

        public static void RegisterListener(Action<Vector3> callback)
        {
            Callbacks += callback;
        }

        public static void DeregisterListener(Action<Vector3> callback)
        {
            Callbacks -= callback;
        }
    }
}
