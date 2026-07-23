using UnityEngine;

namespace GMTK
{ 
    public class PlayerComponent : MonoBehaviour
    {
        Vector3 _cursorPosition;

        public void TriggerSelectStarted()
        {

        }

        public void TriggerSelectEnded()
        {

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
            Gizmos.DrawSphere(_cursorPosition, 0.1f);
        }
    }
}

