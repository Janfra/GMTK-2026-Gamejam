using Janito.EditorExtras;
using System;
using TMPro;
using UnityEngine;

namespace GMTK
{
    public class FormulaComponent : MonoBehaviour
    {
        [Serializable]
        class OptionEntry
        {
            [SerializeField]
            private OptionComponent _optionComponent;
            [SerializeField]
            private int _numberDecimalPlace;

            public OptionType OptionType => _optionComponent.Type;
            public int Number => _optionComponent.NumberComponent ? _optionComponent.NumberComponent.Number + (_numberDecimalPlace * 10) : 0;
            public int GetResult(int A, int B) => _optionComponent.FunctionComponent.Function.GetResult(A, B);
            public bool HasSelection => _optionComponent.HasSelection;

            public event Action OnOptionUpdated {
                add
                {
                    _optionComponent.OnSelectionUpdate += value;
                }
                remove
                {
                    _optionComponent.OnSelectionUpdate -= value;
                }
            }
        }

        [SerializeField]
        private TMP_Text _resultText;
        [SerializeField]
        private OptionEntry[] _options;
        [SerializeField]
        private float _heightOffset;

        private Vector2 _offsetPosition => new Vector2(transform.position.x, transform.position.y - _heightOffset);
        private int? _result;

        private void OnEnable()
        {
            SetListeningForOptionChangesTo(true);
        }

        private void OnDisable()
        {
            SetListeningForOptionChangesTo(false);
        }

        private void Awake()
        {
            if (_options[0].OptionType != OptionType.Number)
            {
                this.LogErrorInDevelopment("Option in slot 1 must be a number option.");
            }

            if (_options[1].OptionType != OptionType.Function)
            {
                this.LogErrorInDevelopment($"Option in slot 2 must be a function option.");
            }

            if (_options[2].OptionType != OptionType.Number)
            {
                this.LogErrorInDevelopment("Option in slot 3 must be a number option.");
            }
        }

        private void SetListeningForOptionChangesTo(bool isEnabled)
        {
            if (isEnabled)
            {
                foreach (var option in _options)
                {
                    option.OnOptionUpdated += TryGetResult;
                }
            }
            else
            {
                foreach (var option in _options)
                {
                    option.OnOptionUpdated -= TryGetResult;
                }
            }
        }

        private void TryGetResult()
        {
            bool areAllSet = true;
            foreach (var option in _options) 
            {
                areAllSet &= option.HasSelection;
            }

            if (!areAllSet) return;
            CalculateResult();
        }

        private void CalculateResult()
        {
            _result = null;

            // For now we assume the order is: digit - function - digit
            int A = _options[0].Number;
            int B = _options[2].Number;

            _result = _options[1].GetResult(A, B);
            _resultText.SetText(_result.Value.ToString());
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(_offsetPosition, 0.1f);
        }

        private void OnValidate()
        {
            if (_options.Length != 3)
            {
                Array.Resize(ref _options, 3);
            }
        }
    }
}
