using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.EventDelegates
{
    /// <summary>
    /// Abstract ScriptableObject class that represents an event delegate.
    /// </summary>
    /// <typeparam name="T">The type of the event delegate's parameter.</typeparam>
    public abstract class EventDelegateSO<T> : ScriptableObject
    {
        /// <summary>
        /// The event delegate.
        /// </summary>
        protected event Action<T> _event;

        /// <summary>
        /// Subscribes the given action to the event delegate.
        /// </summary>
        /// <param name="subscriber">The action to subscribe to the event delegate.</param>
        public virtual void Subscribe(Action<T> subscriber)
        {
            _event += subscriber;
        }

        /// <summary>
        /// Unsubscribes the given action from the event delegate.
        /// </summary>
        /// <param name="subscriber">The action to unsubscribe from the event delegate.</param>
        public virtual void UnSubscribe(Action<T> subscriber)
        {
            _event -= subscriber;
        }

        /// <summary>
        /// Fires the event delegate, invoking all subscribed actions.
        /// </summary>
        /// <param name="value">The value to pass as a parameter to the subscribed actions.</param>
        public virtual void FireEvent(T value)
        {
            _event?.Invoke(value);
        }
    }
}
