using GameLib.ScriptableObjectBases.EventDelegates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameLib.Editor
{

    [CustomEditor(typeof(EventDelegateSO<bool>), true)]
    public class BooleanEventDelegateEditor : Editor
    {


        private bool _param;
        private EventDelegateSO<bool> _delegate;
        private void OnEnable()
        {
            _delegate = target as EventDelegateSO<bool>;
        }
        public override void OnInspectorGUI()
        {


            GUILayout.BeginHorizontal(GUILayout.MinWidth(0));

            GUILayout.Label("Parameter");
            _param = GUILayout.Toggle(_param, "");
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Fire The Event"))
            {
                _delegate?.FireEvent(_param);


            }



            base.OnInspectorGUI();

        }
    }
}
