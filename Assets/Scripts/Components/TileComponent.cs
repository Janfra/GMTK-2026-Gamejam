using UnityEngine;
using UnityEngine.Events;

namespace GMTK
{
    public class TileComponent : MonoBehaviour, ISelectable
    {
        [SerializeField]
        private bool _deselectOnFocusLost = true;

        [Header("Events")]
        [SerializeField]
        private UnityEvent<PlayerComponent> _onSelect;
        [SerializeField]
        private UnityEvent<PlayerComponent> _onDeselect;
        [SerializeField]
        private UnityEvent _onMouseEnter;
        [SerializeField]
        private UnityEvent _onMouseExit;

        public bool IsSelected => _isSelected;

        private bool _isSelected;
        private PlayerComponent _selector;

        public void Select(PlayerComponent player)
        {
            if (IsSelected)
            {
                return;
            }

            _isSelected = true;
            _onSelect.Invoke(player);
            _selector = player;
        }

        public void Deselect()
        {
            if (!IsSelected)
            {
                return;
            }

            _onDeselect.Invoke(_selector);
            _isSelected = false;
        }

        public void OnFocusLost()
        {
            if (_deselectOnFocusLost)
            {
                Deselect();
            }
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
