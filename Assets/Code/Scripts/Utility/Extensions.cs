using System.Collections.Generic;
using UnityEngine;

namespace Siege.Utility
{
    public static class Extensions
    {
        public static Transform DeepFind(this Transform transform, string name, Utility.NameComparisons.Comparison equal = null)
        {
            equal ??= Utility.NameComparisons.Simple;
            
            var queue = new Queue<Transform>();
            queue.Enqueue(transform);

            while (queue.Count > 0)
            {
                var head = queue.Dequeue();
                if (equal(head.name, name)) return head;
                
                foreach (Transform child in head)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }
    }
}
