namespace DigitalRails.Map
{
    using UnityEngine;

    public sealed class Milepost : MonoBehaviour
    {
        [SerializeField] private MilepostData data;

        public MilepostData Data
        {
            get => this.data;
            set => this.data = value;
        }
    }
}
