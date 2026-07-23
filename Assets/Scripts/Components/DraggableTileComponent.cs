using UnityEngine;

namespace GMTK
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DraggableTileComponent : TileComponent, ISelectable, IDraggable
    {
        Rigidbody2D _rb;
        public bool IsBeingDragged { get => _isBeingDragged; set => _isBeingDragged = value; }
        private bool _isBeingDragged;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void UpdateDesiredDragPosition(Vector2 position)
        {
            _rb.MovePosition(position);
        }
    }
}
