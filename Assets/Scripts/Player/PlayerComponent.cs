using UnityEngine;

namespace GMTK
{ 
    public class PlayerComponent : MonoBehaviour
    {
        private Vector3 _cursorPosition;
        private IDraggable _currentDraggable;
        private ISelectable _lastSelectable;

        public void TriggerSelectStarted()
        {
            var collision = Physics2D.OverlapPoint(_cursorPosition);
            if (collision == null)
            {
                ClearLastSelected();
                return;
            }

            if (collision.TryGetComponent(out ISelectable selectable))
            {
                if (selectable != _lastSelectable)
                {
                    ClearLastSelected();
                }

                if (!selectable.IsSelected)
                {
                    selectable.Select();
                    _lastSelectable = selectable;
                }
                else
                {
                    selectable.Deselect();
                }
            }

            if (collision.TryGetComponent(out IDraggable draggable))
            {
                // Start dragging the draggable object
                _currentDraggable = draggable;
            }
        }

        public void UpdateSelectionPosition(Vector2 pointerPosition)
        {
            Camera camera = Camera.main;
            float distance = -camera.transform.position.z; // Assumes the camera is facing the XY plane and is positioned along the Z-axis
            _cursorPosition = camera.ScreenToWorldPoint(new Vector3(pointerPosition.x, pointerPosition.y, distance));
        }

        private void ClearLastSelected()
        {
            _lastSelectable?.OnFocusLost();
            _lastSelectable = null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_cursorPosition, 0.1f);
        }
    }
}

