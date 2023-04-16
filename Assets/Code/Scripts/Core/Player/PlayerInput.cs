using UnityEngine;
using UnityEngine.InputSystem;

namespace Siege.Core
{
    [System.Serializable]
    public sealed class PlayerInput : EventfulModule<PlayerAvatar>
    {
        [SerializeField] private InputActionAsset inputAsset;
        [SerializeField] private float mouseSensitivity;
        [SerializeField] private float gamepadSensitivity;

        private InputAction
            moveAction,
            lookAction;

        public Vector3 MoveDirection
        {
            get
            {
                var input = moveAction.ReadValue<Vector2>();
                return transform.TransformDirection(input.x, 0.0f, input.y);
            }
        }
        
        public Vector2 LookFrameDelta
        {
            get
            {
                var mouse = (Mouse.current != null ? Mouse.current.delta.ReadValue() : Vector2.zero) * mouseSensitivity;
                var gamepad = lookAction.ReadValue<Vector2>() * gamepadSensitivity * Time.deltaTime;
                return mouse + gamepad;
            }
        }

        protected override void OnEnable()
        {
            inputAsset.Enable();

            moveAction = inputAsset.FindAction("Move");
            lookAction = inputAsset.FindAction("Look");
        }

        protected override void OnDisable()
        {
            inputAsset.Disable();
        }
    }
}