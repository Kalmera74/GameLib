using GameLib.ScriptableObjectBases.EventDelegates;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameLib.EditorScripts
{

    [CustomEditor(typeof(EventDelegateSO<object>), true)]
    public class ObjectEventDelegateEditor : Editor
    {


        private Object _param;
        private EventDelegateSO<object> _delegate;
        private void OnEnable()
        {
            _delegate = target as EventDelegateSO<object>;
        }
        public override void OnInspectorGUI()
        {


            GUILayout.BeginHorizontal(GUILayout.MinWidth(0));

            GUILayout.Label("Parameter");
            _param = EditorGUILayout.ObjectField("", _param, typeof(object), true);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Fire The Event"))
            {
                _delegate?.FireEvent(_param);


            }



            base.OnInspectorGUI();

        }
    }
}