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

        private IGameState _state;
        private IOperationMode _operationMode;
        public static GameManager Instance;
        [SerializeField] private bool IsUniversal = true;
        [SerializeField] private bool InitializeWithFirstElements = true;
        [SerializeField] private List<Object> OperationModes = new List<Object>();
        [SerializeField] private List<Object> States = new List<Object>();

        public event Action OnBeforeStateChanged;
        public event Action OnAfterStateChanged;
        public event Action OnBeforeModeChanged;
        public event Action OnAfterModeChange;

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

            if (InitializeWithFirstElements && (States.Count > 0 && _state is null) && (OperationModes.Count > 0 && _operationMode is null))
            {
                SetOperationMode(OperationModes[0] as IOperationMode);
                SetState(States[0] as IGameState);
            }
        }

        public IGameState GetActiveState()
        {
            return _state;
        }
        public IOperationMode GetActiveOperationMode()
        {
            return _operationMode;
        }

        public void SetState(IGameState state)
        {
            OnBeforeStateChanged?.Invoke();

            _state = state;
            AddToStates(state);
            if (_operationMode is null)
            {
                throw new System.ArgumentNullException("Operation Mode cannot be null. Set the Operation Mode before you set the State");
            }
            _operationMode.Operate(_state);

            OnAfterStateChanged?.Invoke();

        }

        public void AddToStates(IGameState state)
        {
            var upCastedState = (Object)state;
            if (!States.Contains(upCastedState))
            {
                States.Add(upCastedState);
            }
        }

        public void AddToModes(IOperationMode mode)
        {
            var upCastedMode = (Object)mode;
            if (!OperationModes.Contains(upCastedMode))
            {
                OperationModes.Add(upCastedMode);
            }
        }

        public void RemoveState(IGameState state)
        {
            var upCastedState = (Object)state;
            States.Remove(upCastedState);
            _operationMode.Operate(null);
        }
        public void RemoveMode(IOperationMode mode)
        {
            var upCastedMode = (Object)mode;
            OperationModes.Remove(upCastedMode);
        }
        public void SetOperationMode(IOperationMode mode)
        {
            OnBeforeModeChanged?.Invoke();

            AddToModes(mode);
            _operationMode = mode;

            OnAfterModeChange?.Invoke();
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
