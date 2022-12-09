using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.EventDelegates
{
    public abstract class EventDelegateSO<T> : ScriptableObject
    {
        protected event Action<T> _event;

        public virtual void Subscribe(Action<T> subscriber)
        {
            _event += subscriber;
        }
        public virtual void UnSubscribe(Action<T> subscriber)
        {
            _event -= subscriber;
        }
        public virtual void FireEvent(T value)
        {
            _event?.Invoke(value);
        }
    }
}
