using GMTK.Calculation;
using Janito.EditorExtras;
using TMPro;
using UnityEngine;

namespace GMTK
{
    public class FunctionComponent : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _tileText;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [InlineInspector]
        [SerializeField]
        private FunctionColourConfiguration _colourConfiguration;

        [SerializeReference]
        [ChildTypeSelection(typeof(BaseFunction))]
        private BaseFunction _function;

        public BaseFunction Function
        {
            get
            {
                return _function;
            }
            set
            {
                _function = value;
                UpdateText();
                UpdateColour();
            }
        }

        private void Awake()
        {
            UpdateText();
            UpdateColour();
        }

        private void UpdateText()
        {
            if (_function != null)
            {
                _tileText?.SetText(_function.GetSymbol());
            }
        }

        private void UpdateColour()
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = _colourConfiguration.GetFunctionColor(Function);
            }
        }
    }
}
