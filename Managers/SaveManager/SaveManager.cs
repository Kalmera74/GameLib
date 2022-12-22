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

        [SerializeField] private  Object Saver;
        [SerializeField] private  Object Loader;

        [SerializeField] private  VoidEventDelegateSO SaveRequestDelegate;
        [SerializeField] private  VoidEventDelegateSO LoadRequestDelegate;

        [SerializeField] private  VoidEventDelegateSO OnBeforeSaveEventDelegate;
        [SerializeField] private  VoidEventDelegateSO OnAfterSaveEventDelegate;

        [SerializeField] private  VoidEventDelegateSO OnBeforeLoadingEventDelegate;
        [SerializeField] private  VoidEventDelegateSO OnAfterLoadingEventDelegate;

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