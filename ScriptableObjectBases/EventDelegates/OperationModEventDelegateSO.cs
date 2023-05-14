using GameLib.Managers.GameManager.Modes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.EventDelegates
{
    [CreateAssetMenu(menuName = "Game/EventDelegates/Game Manager/Operation Mode Param Event Delegate", fileName = "OperationMode_Delegate")]
    public class OperationModEventDelegateSO : EventDelegateSO<OperationMode>
    {
    }
}
