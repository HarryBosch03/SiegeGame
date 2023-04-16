using Siege.Utility;
using UnityEngine;

namespace Siege.Core
{
    [System.Serializable]
    public sealed class PlayerCamera : EventfulModule<PlayerAvatar>
    {
        private Vector2 rotation;
        
        private Transform rotor;
        
        protected override void Awake()
        {
            rotor = transform.DeepFind("Camera Rotor");
        }

        protected override void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        protected override void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        protected override void LateUpdate()
        {
            rotation += Parent.Input.LookFrameDelta;
            rotation.y = Mathf.Clamp(rotation.y, -90.0f, 90.0f);
            
            transform.rotation = Quaternion.Euler(0.0f, rotation.x, 0.0f);
            rotor.rotation = Quaternion.Euler(-rotation.y, rotation.x, 0.0f);
        }
    }
}
