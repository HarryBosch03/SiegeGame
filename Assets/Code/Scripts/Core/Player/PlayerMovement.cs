using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Siege.Core
{
    [System.Serializable]
    public sealed class PlayerMovement : EventfulModule<PlayerAvatar>
    {
        [SerializeField] private float moveSpeed, accelerationTime;

        [Space] 
        [SerializeField] private float footSpacing;
        [SerializeField] private float footSize;
        [SerializeField] private float legLength;

        private Collider[] colliders;

        private Vector3 leftFootPosition, rightFootPosition;

        protected override void OnEnable()
        {
            colliders = gameObject.GetComponentsInChildren<Collider>();
        }

        protected override void FixedUpdate()
        {    
            MovePlayer();
            MoveFeet();
            Integrate();
            Depenetrate();
        }

        private void MoveFeet()
        {
            Vector3 lfp = leftFootPosition, rfp = rightFootPosition, com = transform.position;

            lfp.y = 0.0f;
            rfp.y = 0.0f;
            com.y = 0.0f;

            var direction = (rfp - lfp).normalized;
            var dot = Vector3.Dot(direction, com - lfp);
            var comp = lfp + direction * dot;
            var dist2 = (comp - com).sqrMagnitude;
            if (dist2 < footSize * footSize) return;

            if (dot < (rfp - lfp).magnitude * 0.5f) MoveFoot(ref lfp, rfp, Vector3.zero);
            else MoveFoot(ref rfp, lfp, Vector3.zero);
        }

        private void MoveFoot(ref Vector3 footPos, Vector3 otherFootPos, Vector3 offset)
        {
            
        }

        private void MovePlayer()
        {
            var current = Parent.Velocity;
            var target = Parent.Input.MoveDirection * moveSpeed;
            
            var acceleration = 1.0f / Mathf.Max(accelerationTime, Time.deltaTime);
            var force = (target - current) * acceleration;
            force.y = 0.0f;
            force = Vector3.ClampMagnitude(force, moveSpeed * acceleration);
            Parent.Acceleration += force;
        }

        private void Depenetrate()
        {
            if (colliders.Length == 0) return;
            var bounds = colliders[0].bounds;
            foreach (var collider in colliders)
            {
                bounds.Encapsulate(collider.bounds);
            }

            var offset = Vector3.zero;
            Debug.Log(bounds.center);
            
            var others = Physics.OverlapBox(bounds.center, bounds.extents + Vector3.one * 0.1f, transform.rotation);
            foreach (var collider in colliders)
            {
                foreach (var other in others)
                {
                    if (other.transform.IsChildOf(transform)) continue;
                    
                    Physics.ComputePenetration(
                        collider, collider.transform.position, collider.transform.rotation,
                        other, other.transform.position, other.transform.rotation, 
                        out var direction, out var distance);

                    offset += direction * distance;
                }
            }

            transform.position += offset;
            var dir = offset.normalized;
            Parent.Velocity -= dir * Vector3.Dot(dir, Parent.Velocity);
        }

        private void Integrate()
        {
            transform.position += Parent.Velocity * Time.deltaTime;
            Parent.Velocity += Parent.Acceleration * Time.deltaTime;
            Parent.Acceleration = Physics.gravity;
        }
    }
}
