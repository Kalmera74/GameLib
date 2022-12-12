using GameLib.ScriptableObjectBases.EventDelegates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameLib.EditorScripts
{

    [CustomEditor(typeof(EventDelegateSO<float>), true)]
    public class FloatEvenDelegateEditor : Editor
    {

        private string _textFieldValue;
        private float _param;
        private EventDelegateSO<float> _delegate;

        private void OnEnable()
        {
            _delegate = target as EventDelegateSO<float>;
        }
        public override void OnInspectorGUI()
        {


            GUILayout.BeginHorizontal(GUILayout.MinWidth(0));

            GUILayout.Label("Parameter");
            _textFieldValue = GUILayout.TextField("");
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Fire The Event"))
            {

                _param = float.Parse(_textFieldValue);
                _delegate?.FireEvent(_param);

            }



            base.OnInspectorGUI();

        }
    }
}
