using Janito.EditorExtras;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GMTK
{
    public class TileComponent : MonoBehaviour, ISelectable
    {
        [SerializeField]
        private TMP_Text _tileText;

        [SerializeField]
        private bool _deselectOnFocusLost = true;

        [Header("Events")]
        [SerializeField]
        private UnityEvent _onSelect;
        [SerializeField]
        private UnityEvent _onDeselect;
        [SerializeField]
        private UnityEvent _onMouseEnter;
        [SerializeField]
        private UnityEvent _onMouseExit;

        public bool IsSelected => _isSelected;

        private bool _isSelected;

        private void Awake()
        {
            if (_tileText == null)
            {
                _tileText = GetComponentInChildren<TMP_Text>();
            }
        }

        private void Start()
        {
            if (_tileText == null)
            {
                this.LogErrorInDevelopment("TileComponent: TMP_Text component is not assigned or found in children.");
            }   
        }

        public void SetContent(string content)
        {
            _tileText.text = content;
        }

        public void Select()
        {
            if (IsSelected)
            {
                return;
            }

            _isSelected = true;
            _onSelect.Invoke();    
        }

        public void Deselect()
        {
            if (!IsSelected)
            {
                return;
            }

            _onDeselect.Invoke();
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
