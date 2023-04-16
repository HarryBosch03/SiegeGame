using UnityEngine;

namespace Siege.Core
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class PlayerAvatar : EventfulBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private new PlayerCamera camera;

        public Vector3 Velocity { get; set; }
        public Vector3 Acceleration { get; set; }
        
        public PlayerInput Input => input;

        protected override void Awake()
        {
            EventfulModule<PlayerAvatar>.InitializeAll(this,
                input,
                movement,
                camera);

            base.Awake();
        }
    }
}