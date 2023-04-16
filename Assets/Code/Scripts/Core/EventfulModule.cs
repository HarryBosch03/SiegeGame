using UnityEngine;

namespace Siege.Core
{
    public abstract class EventfulModule<T> where T : EventfulBehaviour
    {
        public T Parent { get; private set; }
        public Transform transform => Parent.transform;
        public GameObject gameObject => Parent.gameObject;
        
        public virtual void Initialize(T parent)
        {
            Parent = parent;
            
            parent.AwakeEvent += Awake;
            parent.StartEvent += Start;
            
            parent.EnableEvent += OnEnable;
            parent.DisableEvent += OnDisable;
            
            parent.UpdateEvent += Update;
            parent.FixedUpdateEvent += FixedUpdate;
            parent.LateUpdateEvent += LateUpdate;
            
            parent.DestroyEvent += OnDestroy;
        }

        public static void InitializeAll(T parent, params EventfulModule<T>[] modules)
        {
            foreach (var module in modules)
            {
                module.Initialize(parent);
            }
        }

        protected virtual void Awake() { }
        protected virtual void Start() { }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
        protected virtual void Update() { }
        protected virtual void FixedUpdate() { }
        protected virtual void LateUpdate() { }
        protected virtual void OnDestroy() { }
    }
}
