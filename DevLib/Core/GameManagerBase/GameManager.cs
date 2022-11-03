using System.Collections;
using System.Collections.Generic;
using Mobiversite.GameLib.DevLib.Core.GameManagerBase.Modes;
using Mobiversite.GameLib.DevLib.Core.GameManagerBase.States;
using UnityEngine;

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

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
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
    }
}
