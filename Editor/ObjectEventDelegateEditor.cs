using UnityEditor;
using UnityEngine;

namespace Mobiversite
{

    [CustomEditor(typeof(EventDelegateSO<Object>), true)]
    public class ObjectEventDelegateEditor : Editor
    {


        private Object _param;
        private EventDelegateSO<Object> _delegate;
        private void OnEnable()
        {
            _delegate = target as EventDelegateSO<Object>;
        }
        public override void OnInspectorGUI()
        {


            GUILayout.BeginHorizontal(GUILayout.MinWidth(0));

            GUILayout.Label("Parameter");
            _param = EditorGUILayout.ObjectField("", _param, typeof(Object), true);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Fire The Event"))
            {
                _delegate?.FireEvent(_param);


            }



            base.OnInspectorGUI();

        }
    }
}