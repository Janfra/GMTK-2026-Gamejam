using UnityEngine;

namespace GMTK
{
    public interface IDraggable
    {
        public void StartDrag();
        public void UpdateDesiredDragPosition(Vector2 position);
        public void EndDrag();
    }
}
