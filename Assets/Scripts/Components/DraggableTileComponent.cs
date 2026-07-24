using UnityEngine;

namespace GMTK
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DraggableTileComponent : TileComponent, ISelectable, IDraggable
    {
        [SerializeField]
        private LimitsComponent _limits;

        Rigidbody2D _rb;
        public bool IsBeingDragged { get => _isBeingDragged; set => _isBeingDragged = value; }
        private bool _isBeingDragged;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _limits = _limits ? _limits : GetComponentInParent<LimitsComponent>();
        }

        public void UpdateDesiredDragPosition(Vector2 position)
        {
            if (_limits)
            {
                _rb.MovePosition(_limits.GetValidPosition(position));
            }
            else
            {
                _rb.MovePosition(position);
            }
        }
    }
}
