using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.EventSytem
{
    class InputEventSystem : MonoBehaviour
    {

        public enum InputState
        {
            NOT_DOWN, DOWN, HOLD, UP
        }

        private static readonly List<KeyCode> keys = new List<KeyCode>();
        private static readonly Dictionary<KeyCode, Action> onDown = new Dictionary<KeyCode, Action>();
        private static readonly Dictionary<KeyCode, Action> onHold = new Dictionary<KeyCode, Action>();
        private static readonly Dictionary<KeyCode, Action> onUp = new Dictionary<KeyCode, Action>();
        private static readonly Dictionary<KeyCode, Action> onNotDown = new Dictionary<KeyCode, Action>();

        public static void RegisterListener(InputState state, KeyCode keyCode, Action callback)
        {
            switch(state)
            {
                case InputState.NOT_DOWN:
                    onNotDown.Add(keyCode, callback);
                    break;
                case InputState.DOWN:
                    onDown.Add(keyCode, callback);
                    break;
                case InputState.HOLD:
                    onHold.Add(keyCode, callback);
                    break;
                case InputState.UP:
                    onUp.Add(keyCode, callback);
                    break;
            }

            keys.Add(keyCode);
        }

        public void Update()
        {
            foreach(KeyCode key in keys)
                FireEventForState(key, GetState(key));
        }

        private InputState GetState(KeyCode key)
        {
            if (Input.GetKeyDown(key))
                return InputState.DOWN;

            if (Input.GetKeyUp(key))
                return InputState.UP;

            return Input.GetKey(key) ? InputState.HOLD : InputState.UP;
        }

        private void FireEventForState(KeyCode key, InputState state)
        {
            switch (state)
            {
                case InputState.NOT_DOWN:
                    FireEvent(onNotDown, key);
                    break;
                case InputState.DOWN:
                    FireEvent(onDown, key);
                    break;
                case InputState.HOLD:
                    FireEvent(onHold, key);
                    break;
                case InputState.UP:
                    FireEvent(onUp, key);
                    break;
            }
        }

        private void FireEvent(Dictionary<KeyCode, Action> eventDictionary, KeyCode key)
        {
            if (eventDictionary.ContainsKey(key))
                eventDictionary[key]();
        }
    }
}
