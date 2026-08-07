using Janito.EditorExtras;
using TMPro;
using UnityEngine;

namespace GMTK
{
    public class NumberComponent : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _tileText;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private int _number;

        [InlineInspector]
        [SerializeField]
        private NumberColourConfiguration _colourConfiguration;

        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = Mathf.Clamp(value, -9, 9);
                _tileText?.SetText(_number.ToString());
                _spriteRenderer.color = _colourConfiguration ? _colourConfiguration.GetColorForNumber(Number) : Color.white;
            }
        }

        private void Awake()
        {
            if (_tileText == null)
            {
                _tileText = GetComponentInChildren<TMP_Text>();
            }

            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }

        private void OnValidate()
        {
            Number = _number;
        }
    }
}
