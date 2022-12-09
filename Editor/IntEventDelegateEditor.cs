using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Mobiversite
{

    [CustomEditor(typeof(EventDelegateSO<int>), true)]
    public class IntEventDelegateEditor : Editor
    {

        private string _textFieldValue;
        private int _param;
        private EventDelegateSO<int> _delegate;
        private void OnEnable()
        {
            _delegate = target as EventDelegateSO<int>;
        }
        public override void OnInspectorGUI()
        {


            GUILayout.BeginHorizontal(GUILayout.MinWidth(0));

            GUILayout.Label("Parameter");
            _textFieldValue = GUILayout.TextField("");
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Fire The Event"))
            {

                _param = Int32.Parse(_textFieldValue);
                _delegate?.FireEvent(_param);

            }



            base.OnInspectorGUI();

        }
    }
}
