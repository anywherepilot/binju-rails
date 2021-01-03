namespace DigitalRails.Managers
{
    using UnityEngine;

    public abstract class Manager<T> : MonoBehaviour where T : Manager<T>
    {
        private static T instance;

        public static T Instance
        {
            get => instance;
            private set
            {
                if(instance == null)
                {
                    instance = value;
                    DontDestroyOnLoad(instance.gameObject);
                    instance.gameObject.hideFlags = HideFlags.HideAndDontSave;
                }
                else if(instance != value)
                {
                    Debug.LogError("[Singleton] Trying to instantiate second instance.");
                    Destroy(value.gameObject);
                }
            }
        }

        public static bool IsInitialized => Instance != null;

        protected virtual void Awake() => Instance = this as T;

        protected virtual void OnDestroy()
        {
            if(Instance == this) Instance = null;
        }
    }
}
