using System;
using System.Collections;
using System.Collections.Generic;
using Mobiversite.GameLib.DevLib.Core.GameManagerBase.Modes;
using Mobiversite.GameLib.DevLib.Core.GameManagerBase.States;
using UnityEngine;
using Object = UnityEngine.Object;
namespace Mobiversite.GameLib.DevLib.Core.GameManagerBase
{
    public enum OperationMode
    {
        Update,
        LateUpdate,
        FixedUpdate,
        OnTrigger,
        AtInterval
    }
    public class GameManager : MonoBehaviour
    {

        private IGameState _state;
        private IOperationMode _operationMode;

        public static GameManager Instance;
        [SerializeField] private bool IsUniversal = false;
        [SerializeField] private List<Object> OperationModes = new List<Object>();
        [SerializeField] private List<Object> States = new List<Object>();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                if (IsUniversal)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                DestroyImmediate(gameObject);
            }


        }

        public void SetState(IGameState state)
        {
            _state = state;

            if (_operationMode is null)
            {
                throw new System.ArgumentNullException("Operation Mode cannot be null. Set the Operation Mode before you set the State");
            }

            _operationMode.Operate(_state);

        }
        public void SetOperationMode(IOperationMode mode)
        {
            _operationMode = mode;
        }

        public void SwitchStateTo(Type stateType)
        {
            foreach (var state in States)
            {
                IGameState downCastedState = state as IGameState;
                if (downCastedState.GetType().Equals(stateType))
                {
                    SetState(downCastedState);
                    break;
                }
            }
        }
        public void SwitchOperationModeTo(Type operationModeType)
        {
            foreach (var mode in OperationModes)
            {
                IOperationMode downCastedOperationMode = mode as IOperationMode;
                if (downCastedOperationMode.GetType().Equals(operationModeType))
                {
                    SetOperationMode(downCastedOperationMode);
                    break;
                }
            }
        }
    }
}
