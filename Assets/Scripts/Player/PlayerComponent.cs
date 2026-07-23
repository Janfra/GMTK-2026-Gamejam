using UnityEngine;

namespace GMTK
{ 
    public class PlayerComponent : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _selectionMask;

        private Vector3 _cursorPosition;
        private IDraggable _currentDraggable;

        public void OnSelectStart()
        {
            var collision = Physics2D.OverlapPoint(_cursorPosition, _selectionMask.value);
            if (collision == null)
            {
                return;
            }

            if (collision.TryGetComponent(out IDraggable draggable))
            {
                // Start dragging the draggable object
                _currentDraggable = draggable;
                _currentDraggable.IsBeingDragged = true;
            }
        }

        public void OnSelectEnd()
        {
            if (_currentDraggable != null)
            {
                _currentDraggable.IsBeingDragged = false;
                _currentDraggable = null;
            }
        }

        private void LateUpdate()
        {
            _currentDraggable?.UpdateDesiredDragPosition(_cursorPosition);
        }

        public void UpdateSelectionPosition(Vector2 pointerPosition)
        {
            Camera camera = Camera.main;
            float distance = -camera.transform.position.z; // Assumes the camera is facing the XY plane and is positioned along the Z-axis
            _cursorPosition = camera.ScreenToWorldPoint(new Vector3(pointerPosition.x, pointerPosition.y, distance));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_cursorPosition, 0.01f);
        }
    }
}

