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

        [SerializeField]
        [ReadOnly]
        private NumberComponent _number;
        [SerializeField]
        [ReadOnly]
        private FunctionComponent _function;
        private IDraggable _selectedDrag;

        public OptionType Type => _type;
        public NumberComponent NumberComponent => _number;
        public FunctionComponent FunctionComponent => _function;
        public bool HasSelection => NumberComponent || FunctionComponent;

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
        }
    }
}
