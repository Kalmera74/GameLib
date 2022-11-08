using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

namespace Mobiversite
{
    [Serializable]
    public enum Modifier
    {
        Shift,
        Control,
        Alt,
        Command
    }
    [Serializable]
    public enum MainKey
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        W,
        X,
        Y,
        Z,
    }

    [Serializable]
    public struct KeyToEvent
    {
        public List<Modifier> Modifiers;
        public List<MainKey> MainKeys;
        public UnityEvent Function;
    }
    public class ShortCutManager : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this);
        }
        [SerializeField] private List<KeyToEvent> ShortCuts = new List<KeyToEvent>();
        private Dictionary<string, UnityEvent> _keyBinds = new Dictionary<string, UnityEvent>();


        void Start()
        {
            ConvertListToDictionary();
        }

        void OnGUI()
        {
            if (Event.current.isKey)
            {
                CheckKeyEvents();
            }
        }

        private void CheckKeyEvents()
        {
            foreach (var keyBinds in _keyBinds)
            {
                string key = keyBinds.Key;

                if (Event.current.Equals(Event.KeyboardEvent(key)))
                {
                    keyBinds.Value.Invoke();
                }
            }

        }
        private void ConvertListToDictionary()
        {
            // TODO use string builder instead of string concat

            foreach (var shortCut in ShortCuts)
            {
                string modifiers = String.Empty;
                string mainKeys = String.Empty;

                foreach (var modifier in shortCut.Modifiers)
                {
                    modifiers += ConvertModifiersToSpecialCharacters(modifier);
                }
                foreach (var mainKey in shortCut.MainKeys)
                {
                    mainKeys += mainKey.ToString().ToLower();
                }

                string finalKey = modifiers + mainKeys;
                _keyBinds.Add(finalKey, shortCut.Function);
            }
        }
        private string ConvertModifiersToSpecialCharacters(Modifier key)
        {
            string convertedKey = String.Empty;
            switch (key)
            {

                case Modifier.Shift:
                    convertedKey = "#";
                    break;
                case Modifier.Control:
                    convertedKey = "^";
                    break;
                case Modifier.Alt:
                    convertedKey = "&";
                    break;
                case Modifier.Command:
                    convertedKey = "%";
                    break;
            }
            return convertedKey;
        }

    }
}
