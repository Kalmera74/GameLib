using UnityEditor;
using UnityEngine;

namespace Mobiversite
{

    [CustomEditor(typeof(EventDelegateSO<GameState>), true)]
    public class GameStateDelegateEditor : Editor
    {


        private Object _param;
        private EventDelegateSO<GameState> _delegate;
        private void OnEnable()
        {
            _delegate = target as EventDelegateSO<GameState>;
        }
        public override void OnInspectorGUI()
        {


            GUILayout.BeginHorizontal(GUILayout.MinWidth(0));

            GUILayout.Label("Parameter");
            _param = EditorGUILayout.ObjectField("", _param, typeof(GameState), true);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Fire The Event"))
            {
                _delegate?.FireEvent((GameState)_param);


            }



            base.OnInspectorGUI();

        }
    }
}