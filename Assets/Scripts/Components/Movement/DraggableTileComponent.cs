using System;
using UnityEngine;

namespace GMTK
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DraggableTileComponent : TileComponent, ISelectable, IInventoryItem
    {
        public event Action<IDraggable> OnDragStateChanged;
        public event Action<IInventoryItem, bool> OnReturnedToInventory;

        [SerializeField]
        private LimitsComponent _limits;

        public bool IsBeingDragged { 
            get {
                return _isBeingDragged;
            }
            set {
                var oldState = _isBeingDragged;
                _isBeingDragged = value;
                if (oldState != _isBeingDragged)
                {
                    OnDragStateChanged?.Invoke(this);
                }
            }
        }

        public bool IsLocked { get; set; }

        private Rigidbody2D _rb;
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

        public void ReturnToInventory(bool isInstant)
        {
            OnReturnedToInventory?.Invoke(this, isInstant);
        }
    }
}
