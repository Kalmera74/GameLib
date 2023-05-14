
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

namespace GameLib.Utility
{
    /// <summary>
    /// Enumeration of possible keyboard modifiers.
    /// </summary>
    [Serializable]
    public enum Modifier
    {
        Shift,
        Control,
        Alt,
        Command
    }
    /// <summary>
    /// Enumeration of possible main keys
    /// </summary>
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
    /// <summary>
    /// Struct that contains the combination of keys and the event to be invoked
    /// </summary>

    [Serializable]
    public struct KeyToEvent
    {
        public List<Modifier> Modifiers;
        public List<MainKey> MainKeys;
        public UnityEvent Function;
    }
    /// <summary>
    /// A class that manages the shortcut keys
    /// </summary>
    public class ShortCutManager : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this);
        }
        /// <summary>
        /// List of all the shortcut keys
        /// </summary>
        [SerializeField] private List<KeyToEvent> ShortCuts = new List<KeyToEvent>();
        /// <summary>
        /// Dictionary representation of the list
        /// </summary>
        private Dictionary<string, UnityEvent> _keyBinds = new Dictionary<string, UnityEvent>();


        void Start()
        {

            ConvertListToDictionary();

        }

        void OnGUI()
        {
#if UNITY_EDITOR
            if (Event.current.isKey)
            {
                CheckKeyEvents();
            }
#endif
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
        /// <summary>
        /// Converts the list of shortcuts to a dictionary
        /// </summary>
        private void ConvertListToDictionary()
        {
            // TODO use string builder instead of string concat

            foreach (var shortCut in ShortCuts)
            {
                string modifiers = string.Empty;
                string mainKeys = string.Empty;

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
        /// <summary>
        /// Converts the keyboard modifiers to special characters
        /// </summary>
        /// <param name="key">Modifier key</param>
        /// <returns>Special character representation of the modifier key</returns>
        private string ConvertModifiersToSpecialCharacters(Modifier key)
        {
            string convertedKey = string.Empty;
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

