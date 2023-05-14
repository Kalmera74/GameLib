using GameLib.Managers.GameManager.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.EventDelegates
{
    [CreateAssetMenu(menuName = "Game/EventDelegates/Game Manager/Game State Param Event Delegate", fileName = "GameState_Delegate")]
    public class GameStateEventDelegateSO : EventDelegateSO<GameState>
    {

    }
}
