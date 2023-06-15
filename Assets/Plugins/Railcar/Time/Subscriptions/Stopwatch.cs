using System;
using System.Collections.Generic;

namespace Railcar.Time.Subscriptions
{
    public class Stopwatch : IDisposable
    {
        private Action<float> _callback;
        private LinkedListNode<Stopwatch> _node;

        public Stopwatch(LinkedList<Stopwatch> list, Action<float> callback)
        {
            _node = new LinkedListNode<Stopwatch>(this);
            list.AddLast(_node);

            _callback = callback;
            #if UNITY_EDITOR
            Trace = Environment.StackTrace;
            #endif
        }
        
        #if UNITY_EDITOR
        public string Trace { get; }
        #endif

        public void Update(float delta)
        {
            _callback.Invoke(delta);
        }

        public void Dispose()
        {
            if (_node == null)
                return;
            _node.List.Remove(_node);
            _node = null;
            _callback = null;
        }
    }
}