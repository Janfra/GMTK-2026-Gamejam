using TMPro;
using UnityEngine;

namespace GMTK
{
    public class NumberComponent : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _tileText;

        [SerializeField]
        private int _number;

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
            }
        }

        private void Awake()
        {
            if (_tileText == null)
            {
                _tileText = GetComponentInChildren<TMP_Text>();
            }
        }

        private void OnValidate()
        {
            Number = _number;
        }
    }
}
