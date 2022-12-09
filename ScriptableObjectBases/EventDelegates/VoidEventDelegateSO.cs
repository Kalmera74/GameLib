using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.EventDelegates
{
    [CreateAssetMenu(menuName = "Mobiversite/EventDelegates/Void Event Delegate", fileName = "Void_Delegate")]
    public class VoidEventDelegateSO : ScriptableObject
    {

        private event Action _voidEvent;


        public void Subscribe(Action subscriber)
        {
            _voidEvent += subscriber;
        }
        public void UnSubscribe(Action subscriber)
        {
            _voidEvent -= subscriber;
        }
        public void FireEvent()
        {
            _voidEvent?.Invoke();
        }

    }
}
