
using UnityEditor;
using UnityEngine;
namespace Mobiversite
{

    [CustomEditor(typeof(EventDelegateSO<OperationMode>), true)]
    public class OperationModDelegateEditor : Editor
    {


        private Object _param;
        private EventDelegateSO<OperationMode> _delegate;
        private void OnEnable()
        {
            _delegate = target as EventDelegateSO<OperationMode>;
        }
        public override void OnInspectorGUI()
        {


            GUILayout.BeginHorizontal(GUILayout.MinWidth(0));

            GUILayout.Label("Parameter");
            _param = EditorGUILayout.ObjectField("", _param, typeof(OperationMode), true);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Fire The Event"))
            {
                _delegate?.FireEvent((OperationMode)_param);


            }



            base.OnInspectorGUI();

        }
    }
}