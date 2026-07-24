using UnityEngine;

namespace GMTK
{
    [RequireComponent(typeof(Collider2D))]
    public class LimitsComponent : MonoBehaviour
    {
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        public Vector2 GetValidPosition(Vector2 position)
        {
            if (_collider.bounds.Contains(position))
            {
                return position;
            }
            else
            {
                return _collider.ClosestPoint(position);
            }
        }
    }
}