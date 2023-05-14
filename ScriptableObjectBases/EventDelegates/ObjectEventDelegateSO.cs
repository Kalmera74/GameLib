using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.EventDelegates
{
    [CreateAssetMenu(menuName = "Game/EventDelegates/Object Param Event Delegate", fileName = "Object_Delegate")]
    public class ObjectEventDelegateSO : EventDelegateSO<object>
    {
    }
}
