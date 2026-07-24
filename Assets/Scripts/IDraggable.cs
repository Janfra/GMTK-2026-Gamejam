using System;
using UnityEngine;

namespace GMTK
{
    public interface IDraggable
    {
        public event Action<IDraggable> OnDragStateChanged;
        public bool IsLocked { get; set; }
        public bool IsBeingDragged { get; set; }
        public void UpdateDesiredDragPosition(Vector2 position);
    }
}
