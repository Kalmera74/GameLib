using System;
using System.Collections;
using System.Collections.Generic;
using Mobiversite.GameLib.DevLib.Core;
using UnityEngine;
using Object = UnityEngine.Object;
namespace Mobiversite.GameLib.DevLib.Core
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private GameState State;
        [SerializeField] private OperationMode OperationMode;

        [SerializeField] private EventDelegateSO<OperationMode> SetOperationModeRequest;
        [SerializeField] private EventDelegateSO<GameState> SetGameStateRequest;

        [SerializeField] private VoidEventDelegateSO OnBeforeStateChangedEvent;
        [SerializeField] private VoidEventDelegateSO OnAfterStateChangedEvent;


        void Awake()
        {
            SetOperationModeRequest.Subscribe(SetOperationMode);
            SetGameStateRequest.Subscribe(SetState);
        }

        void Start()
        {
            SetState(State);
        }



        public void SetState(GameState state)
        {
            if (OperationMode is null)
            {
                throw new System.ArgumentNullException("Operation Mode cannot be null. Set the Operation Mode before you set the State");
            }

            OnBeforeStateChangedEvent.FireEvent();

            State = state;
            OperationMode.Operate(State);

            OnAfterStateChangedEvent.FireEvent();

        }




        public void SetOperationMode(OperationMode mode)
        {

            OperationMode = mode;

        }



    }
}
