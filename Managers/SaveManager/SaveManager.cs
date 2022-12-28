using GameLib.ScriptableObjectBases.EventDelegates;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameLib.Managers.SaveManager
{
    /// <summary>
    /// The SaveManager class is responsible for managing the saving and loading of data in the game.
    /// </summary>
    public class SaveManager : MonoBehaviour
    {
        /// <summary>
        /// A reference to the object that implements the ISaver interface, which is responsible for saving data.
        /// </summary>
        [SerializeField] private Object Saver;

        /// <summary>
        /// A reference to the object that implements the ILoader interface, which is responsible for loading data.
        /// </summary>
        [SerializeField] private Object Loader;

        /// <summary>
        /// A reference to a scriptable object that contains a delegate for handling save requests.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO SaveRequestDelegate;

        /// <summary>
        /// A reference to a scriptable object that contains a delegate for handling load requests.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO LoadRequestDelegate;

        /// <summary>
        /// A reference to a scriptable object that contains a delegate for handling events before a save operation.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO OnBeforeSaveEventDelegate;

        /// <summary>
        /// A reference to a scriptable object that contains a delegate for handling events after a save operation.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO OnAfterSaveEventDelegate;

        /// <summary>
        /// A reference to a scriptable object that contains a delegate for handling events before a load operation.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO OnBeforeLoadingEventDelegate;

        /// <summary>
        /// A reference to a scriptable object that contains a delegate for handling events after a load operation.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO OnAfterLoadingEventDelegate;

        /// <summary>
        /// A reference to the object that implements the ISaver interface.
        /// </summary>
        private ISaver _saver;

        /// <summary>
        /// A reference to the object that implements the ILoader interface.
        /// </summary>
        private ILoader _loader;

        /// <summary>
        /// Initializes the SaveManager by setting up the references to the ISaver and ILoader objects and subscribing to the save and load request delegate events.
        /// </summary>
        void Awake()
        {
            _saver = (ISaver)Saver;
            _loader = (ILoader)Loader;

            SaveRequestDelegate?.Subscribe(Save);
            LoadRequestDelegate?.Subscribe(Load);
        }

        /// <summary>
        /// Loads data when the game starts.
        /// </summary>
        void Start()
        {
            Load();
        }
        /// <summary>
        /// Saves data by firing the OnBeforeSaveEventDelegate
        /// event, performing the save operation using the ISaver object, and then firing the OnAfterSaveEventDelegate event.
        /// </summary>
        private void Save()
        {
            OnBeforeSaveEventDelegate?.FireEvent();
            _saver?.Save();
            OnAfterSaveEventDelegate?.FireEvent();
        }
        /// <summary>
        /// Loads data by firing the OnBeforeLoadingEventDelegate event, performing the load operation using the ILoader object, and then firing the OnAfterLoadingEventDelegate event.
        /// </summary>
        private void Load()
        {
            OnBeforeLoadingEventDelegate?.FireEvent();
            _loader?.Load();
            OnAfterLoadingEventDelegate?.FireEvent();
        }

        /// <summary>
        /// Unsubscribes from the save and load request delegate events when the SaveManager is destroyed.
        /// </summary>
        void OnDestroy()
        {
            SaveRequestDelegate?.UnSubscribe(Save);
            LoadRequestDelegate?.UnSubscribe(Load);
        }
    }
}
