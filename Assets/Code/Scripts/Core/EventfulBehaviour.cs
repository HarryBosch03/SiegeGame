using System;
using UnityEngine;

namespace Siege.Core
{
    public abstract class EventfulBehaviour : MonoBehaviour
    {
        public event Action
            AwakeEvent,
            StartEvent,
            EnableEvent,
            DisableEvent,
            UpdateEvent,
            FixedUpdateEvent,
            LateUpdateEvent,
            DestroyEvent;

        private void RaiseEvent(Action e) => e?.Invoke();

        protected virtual void Awake()
        {
            RaiseEvent(AwakeEvent);
        }

        protected virtual void Start() => RaiseEvent(StartEvent);

        protected virtual void OnEnable() => RaiseEvent(EnableEvent);
        protected virtual void OnDisable() => RaiseEvent(DisableEvent);

        protected virtual void Update() => RaiseEvent(UpdateEvent);
        protected virtual void FixedUpdate() => RaiseEvent(FixedUpdateEvent);
        protected virtual void LateUpdate() => RaiseEvent(LateUpdateEvent);

        protected virtual void OnDestroy() => RaiseEvent(DestroyEvent);
    }
}