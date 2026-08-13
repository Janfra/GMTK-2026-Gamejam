using Janito.EditorExtras;
using System;
using UnityEngine;

namespace GMTK
{
    public enum OptionType
    {
        Number,
        Function
    }

    public class OptionComponent : MonoBehaviour
    {
        public Action OnSelectionUpdate;

        [SerializeField]
        private OptionType _type;

        [Header("Animation")]
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private AnimationClip _selectAnimation;
        [SerializeField]
        private AnimationClip _deselectAnimation;

        [Header("Debugging")]
        [SerializeField]
        [ReadOnly]
        private NumberComponent _number;
        [SerializeField]
        [ReadOnly]
        private FunctionComponent _function;

        private IDraggable _selectedDrag;
        private int _selectAnimationHash;
        private int _deselectAnimationHash; 

        public OptionType Type => _type;
        public NumberComponent NumberComponent => _number;
        public FunctionComponent FunctionComponent => _function;
        public bool HasSelection => NumberComponent || FunctionComponent;

        private void Awake()
        {
            InitialiseAnimation();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (HasSelection)
            {
                if (_selectedDrag.IsBeingDragged || !_selectedDrag.IsLocked)
                {
                    Deselect();
                }
            }
            else
            {
                if (collision.TryGetComponent(out IDraggable draggable) && !draggable.IsBeingDragged)
                {
                    if (TrySelect(collision))
                    {
                        _selectedDrag = draggable;
                        _selectedDrag.IsLocked = true;
                        OnSelectionUpdate?.Invoke();
                        _animator?.Play(_selectAnimationHash);
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!HasSelection) return;

            if (collision.TryGetComponent(out IDraggable draggable))
            {
                if (draggable == _selectedDrag)
                {
                    Deselect();
                }
            }
        }

        private void InitialiseAnimation()
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }

            if (_animator == null)
            {
                this.LogWarningInDevelopment($"Animator component is not assigned in the inspector or found on the GameObject.");
            }
            else
            {
                if (_selectAnimation)
                {
                    _selectAnimationHash = Animator.StringToHash(_selectAnimation.name);
                }
                else
                {
                    this.LogWarningInDevelopment($"Animation clip {nameof(_selectAnimation)} is not assigned in the inspector.");
                }

                if (_deselectAnimation)
                {
                    _deselectAnimationHash = Animator.StringToHash(_deselectAnimation.name);
                }
                else
                {
                    this.LogWarningInDevelopment($"Animation clip {nameof(_deselectAnimation)} is not assigned in the inspector.");
                }
            }
        }

        private bool TrySelect(Collider2D collision)
        {
            switch (_type)
            {
                case OptionType.Number:
                    if (collision.TryGetComponent(out NumberComponent numComponent))
                    {
                        _number = numComponent;
                        _number.transform.position = transform.position;
                        return true;
                    }
                    return false;

                case OptionType.Function:
                    if (collision.TryGetComponent(out FunctionComponent funcComponent))
                    {
                        _function = funcComponent;
                        _function.transform.position = transform.position;
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }

        public void Deselect()
        {
            switch (_type)
            {
                case OptionType.Number:
                    _number = null;
                    break;

                case OptionType.Function:
                    _function = null;
                    break;
                default:
                    break;
            }

            _selectedDrag.IsLocked = false;
            _selectedDrag = null;
            OnSelectionUpdate?.Invoke();
            _animator?.Play(_deselectAnimationHash);
        }
    }
}
