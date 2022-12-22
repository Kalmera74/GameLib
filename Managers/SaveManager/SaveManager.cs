using GameLib.ScriptableObjectBases.EventDelegates;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameLib.Managers.SaveManager
{
    public class SaveManager : MonoBehaviour
    {
        private ISaver _saver;
        private ILoader _loader;

        [SerializeField] private readonly Object Saver;
        [SerializeField] private readonly Object Loader;

        [SerializeField] private readonly VoidEventDelegateSO SaveRequestDelegate;
        [SerializeField] private readonly VoidEventDelegateSO LoadRequestDelegate;

        [SerializeField] private readonly VoidEventDelegateSO OnBeforeSaveEventDelegate;
        [SerializeField] private readonly VoidEventDelegateSO OnAfterSaveEventDelegate;

        [SerializeField] private readonly VoidEventDelegateSO OnBeforeLoadingEventDelegate;
        [SerializeField] private readonly VoidEventDelegateSO OnAfterLoadingEventDelegate;

        void Awake()
        {
            _saver = (ISaver)Saver;
            _loader = (ILoader)Loader;

            SaveRequestDelegate.Subscribe(Save);
            LoadRequestDelegate.Subscribe(Load);
            

        }
        void Start()
        {
            Load();
        }

        private void Save()
        {
            OnBeforeSaveEventDelegate.FireEvent();
            _saver.Save();
            OnAfterSaveEventDelegate.FireEvent();

        }
        private void Load()
        {
            OnBeforeLoadingEventDelegate.FireEvent();
            _loader.Load();
            OnAfterLoadingEventDelegate.FireEvent();
        }

       void OnDestroy()
        {
            SaveRequestDelegate.UnSubscribe(Save);
            LoadRequestDelegate.UnSubscribe(Load);
        }

      
    }
}