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
        [SerializeField]
        private OptionType _type;
        private NumberComponent _number;
        private FunctionComponent _function;
        private IDraggable _selectedDrag;

        public OptionType Type => _type;
        public NumberComponent Number => _number;
        public FunctionComponent Function => _function;
        public bool HasSelection => Number || Function;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (HasSelection)
            {
                if (_selectedDrag.IsBeingDragged)
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
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!HasSelection) return;

            Deselect();
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

        private void Deselect()
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
        }
    }
}
