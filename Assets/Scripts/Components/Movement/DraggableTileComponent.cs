using System;
using UnityEngine;
using UnityEngine.Events;

namespace GMTK
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DraggableTileComponent : MonoBehaviour, IInventoryItem
    {
        public event Action<IDraggable> OnDragStateChanged;
        public event Action<IInventoryItem, bool> OnReturnedToInventory;

        [SerializeField]
        private LimitsComponent _limits;

        [Header("Animation")]
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private AnimationClip _returnAnimation;
        [SerializeField]
        private AnimationClip _appearAnimation;

        [Header("Events")]
        [Space]
        [SerializeField]
        private UnityEvent _dragStart;
        [SerializeField]
        private UnityEvent _dragEnd;
        [SerializeField]
        private UnityEvent _onMouseEnter;
        [SerializeField]
        private UnityEvent _onMouseExit;

        public bool IsBeingDragged { 
            get {
                return _isBeingDragged;
            }
            set {
                var oldState = _isBeingDragged;
                _isBeingDragged = value;
                if (oldState != _isBeingDragged)
                {
                    if (_isBeingDragged)
                    {
                        _dragStart.Invoke();
                    }
                    else
                    {
                        _dragEnd.Invoke();
                    }

                    OnDragStateChanged?.Invoke(this);
                }
            }
        }

        public bool IsLocked { get; set; }

        public float MoveDelay => _returnAnimation ? _returnAnimation.averageDuration : 0.0f;

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

        public void StartReturnAnimation()
        {
            _animator.Play(_returnAnimation.name);
        }

        public void OnReturned()
        {
            _animator.Play(_appearAnimation.name);
        }

        private void OnMouseEnter()
        {
            _onMouseEnter.Invoke();
        }

        private void OnMouseExit()
        {
            _onMouseExit.Invoke();
        }
    }
}
