using System;
using System.Collections.Generic;

namespace Railcar.Time.Subscriptions
{
    public class Timer : IDisposable
    {
        private Action<float> _callback;
        
        public Timer(LinkedList<Timer> list, float timestamp, Action<float> callback)
        {
            Node = new LinkedListNode<Timer>(this);
            Timestamp = timestamp;
            _callback = callback;

            Insert(Node, list);
            #if UNITY_EDITOR
            Name = Environment.StackTrace;
            #endif
        }

        #if UNITY_EDITOR
        public string Name { get; }
        public float Time => Timestamp;
        #endif
        protected LinkedListNode<Timer> Node { get; private set; }
        protected float Timestamp { get; set; }

        public void Update(float currentTime)
        {
            if (currentTime >= Timestamp)
            {
                _callback.Invoke(currentTime);
                OnElapsed(currentTime);
            }
        }

        public void Dispose()
        {
            if (Node == null)
                return;

            Node.List.Remove(Node);
            Node = null;
            _callback = null;
        }

        protected static void Insert(LinkedListNode<Timer> node, LinkedList<Timer> list)
        {
            if (list.Count == 0)
            {
                list.AddFirst(node);
                return;
            }

            var timestamp = node.Value.Timestamp;
            var next = list.First;
            while (next is not null)
            {
                if (next.Value.Timestamp > timestamp)
                {
                    list.AddBefore(next, node);
                    return;
                }

                next = next.Next;
            }

            list.AddLast(node);
        }

        protected virtual void OnElapsed(float time)
        {
            Dispose();
        }
    }
}