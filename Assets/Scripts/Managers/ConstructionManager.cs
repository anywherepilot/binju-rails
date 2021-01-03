namespace DigitalRails.Managers
{
    using System.Collections.Generic;
    using Map;
    using UnityEngine;

    public sealed class ConstructionManager : Manager<ConstructionManager>
    {
        [SerializeField] private Sprite trackSprite;

        private int trackLayer;

        private bool constructionMode;
        private List<Milepost> constructionPosts;
        private List<GameObject> constructionTracks;

        protected override void Awake()
        {
            base.Awake();
            this.constructionMode = false;
            this.trackLayer = SortingLayer.GetLayerValueFromName("Track");
        }

        public Sprite TrackSprite
        {
            get => this.trackSprite;
            set => this.trackSprite = value;
        }

        public void OnMainMouseButtonUp()
        {
            this.constructionMode = false;
        }

        public void OnMouseDraggedOverMilepost(Milepost milepost)
        {
            if(!this.constructionMode)
            {
                this.constructionMode = true;
                this.constructionPosts = new List<Milepost> {milepost};
                this.constructionTracks = new List<GameObject>();
                return;
            }

            // If the path already ends with this milepost, there's nothing to do
            if(milepost == this.constructionPosts[this.constructionPosts.Count - 1]) return;

            // If we're encountering the previous milepost again, remove the last part of the path
            if(this.constructionPosts.Count > 1 && milepost == this.constructionPosts[this.constructionPosts.Count - 2])
            {
                this.constructionPosts.RemoveAt(this.constructionPosts.Count - 1);
                Destroy(this.constructionTracks[this.constructionTracks.Count - 1]);
                this.constructionTracks.RemoveAt(this.constructionTracks.Count - 1);
                return;
            }

            // Encountered a new milepost
            // TODO check that this path can be built
            Milepost previousMilepost = this.constructionPosts[this.constructionPosts.Count - 1];

            if(this.constructionPosts.Contains(milepost))
            {
                // Check if the path to be constructed already contains this edge, in either direction
                for(int i = 0; i < this.constructionPosts.Count; i++)
                {
                    if(this.constructionPosts[i] != milepost) continue;

                    if(i > 0 && this.constructionPosts[i - 1] == previousMilepost ||
                       i < this.constructionPosts.Count - 1 && this.constructionPosts[i + 1] == previousMilepost)
                    {
                        return;
                    }
                }
            }

            this.constructionPosts.Add(milepost);
            AddUnderConstructionTrack(previousMilepost, milepost);
        }

        private void AddUnderConstructionTrack(Milepost fromMilepost, Milepost toMilepost)
        {
            GameObject track = new GameObject("Track");
            track.layer = this.trackLayer;
            SpriteRenderer spriteRenderer = track.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = TrackSprite;

            Vector3 fromPosition = fromMilepost.gameObject.transform.position;
            Vector3 toPosition = toMilepost.gameObject.transform.position;

            // TODO this assumes the sprite is 1 long. Check the actual length.
            track.transform.position = fromPosition + (toPosition - fromPosition) / 2;
            track.transform.localScale = new Vector3((fromPosition - toPosition).magnitude, 1, 1);
            Vector3 direction = (toPosition - fromPosition).normalized;
            track.transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);

            this.constructionTracks.Add(track);
        }
    }
}
