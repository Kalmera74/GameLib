using System;
using System.Collections;
using System.Collections.Generic;
using GameLib.Managers.GameManager.Modes;
using GameLib.Managers.GameManager.States;
using GameLib.ScriptableObjectBases.EventDelegates;
using UnityEngine;
using Object = UnityEngine.Object;
namespace GameLib.Managers.GameManager
{
    /// <summary>
    /// A class that manages the overall flow of a game, including the current state and operation mode.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// The current state of the game.
        /// </summary>
        [SerializeField] private GameState State;

        /// <summary>
        /// The current operation mode of the game.
        /// </summary>
        [SerializeField] private OperationMode OperationMode;

        /// <summary>
        /// A delegate that can be used to request a change to the operation mode of the game.
        /// </summary>
        [SerializeField] private EventDelegateSO<OperationMode> SetOperationModeRequest;

        /// <summary>
        /// A delegate that can be used to request a change to the state of the game.
        /// </summary>
        [SerializeField] private EventDelegateSO<GameState> SetGameStateRequest;
        /// <summary>
        /// An event that is fired before the state of the game is changed.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO OnBeforeStateChangedEvent;

        /// <summary>
        /// An event that is fired after the state of the game is changed.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO OnAfterStateChangedEvent;

        /// <summary>
        /// Subscribes to the appropriate delegate events when the object is awakened.
        /// </summary>
        void Awake()
        {
            SetOperationModeRequest.Subscribe(SetOperationMode);
            SetGameStateRequest.Subscribe(SetState);
        }

        /// <summary>
        /// Sets the initial state of the game when the object is started.
        /// </summary>
        void Start()
        {
            SetState(State);
        }

        /// <summary>
        /// Sets the state of the game and operates the game in the current operation mode.
        /// </summary>
        /// <param name="state">The new state of the game.</param>
        private void SetState(GameState state)
        {
            if (OperationMode is null)
            {
                throw new ArgumentNullException("Operation Mode cannot be null. Set the Operation Mode before you set the State");
            }

            OnBeforeStateChangedEvent.FireEvent();

            State = state;
            OperationMode.Operate(State);

            OnAfterStateChangedEvent.FireEvent();
        }

        /// <summary>
        /// Sets the operation mode of the game.
        /// </summary>
        ///
        /// <param name="mode">The new operation mode of the game.</param>
        private void SetOperationMode(OperationMode mode)
        {
            OperationMode = mode;
            if (State is not null)
            {
                OperationMode.Operate(State);
            }
        }
    }
}
