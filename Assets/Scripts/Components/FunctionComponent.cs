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
            }
        }

        private void Awake()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            if (_function != null)
            {
                _tileText?.SetText(_function.GetSymbol());
            }
        }
    }
}
