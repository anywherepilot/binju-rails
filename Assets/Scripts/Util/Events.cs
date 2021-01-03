namespace DigitalRails.Utils
{
    using System;
    using Map;
    using UnityEngine;
    using UnityEngine.Events;

    public sealed class Events : MonoBehaviour
    {
        // Mouse events
        [Serializable] public class MouseDraggedOverMilepost : UnityEvent<Milepost> { }
        [Serializable] public class MainMouseButtonUp : UnityEvent { }
    }
}
