using UnityEngine;

namespace GMTK
{
    public interface IDraggable
    {
        public bool IsBeingDragged { get; set; }
        public void UpdateDesiredDragPosition(Vector2 position);
    }
}
