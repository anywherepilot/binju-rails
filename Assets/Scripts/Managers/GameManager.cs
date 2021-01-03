namespace DigitalRails.Managers
{
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class GameManager : Manager<GameManager>
    {
        public GameObject[] systemPrefabs;

        private List<GameObject> instancedSystemPrefabs;

        private void Start()
        {
            InstantiateSystemPrefabs();
        }

        protected override void OnDestroy()
        {
            if(this.instancedSystemPrefabs != null)
            {
                foreach(GameObject prefab in this.instancedSystemPrefabs) Destroy(prefab);
                this.instancedSystemPrefabs.Clear();
            }

            base.OnDestroy();
        }

        private void InstantiateSystemPrefabs()
        {
            this.instancedSystemPrefabs = new List<GameObject>(this.systemPrefabs.Length);
            foreach(GameObject prefab in this.systemPrefabs) this.instancedSystemPrefabs.Add(Instantiate(prefab));
        }
    }
}
