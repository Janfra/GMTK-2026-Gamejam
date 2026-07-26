using GMTK.Calculation;
using GMTK.Generation;
using Janito.EditorExtras;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GMTK
{
    public class ResultHandlerComponent : MonoBehaviour
    {
        [SerializeField]
        private FormulaComponent _resultGenerator;
        [SerializeField]
        private CountdownComponent _countdown;
        [SerializeField]
        private ItemList<BaseResultCondition> _conditions;
        [SerializeField]
        private BaseResultCondition _startCondition;
        [SerializeField]
        private TMP_Text _conditionDisplay;

        private BaseResultCondition _currentCondition;

        [Header("Events")]
        [SerializeField]
        private UnityEvent<bool> _onResultOutcomeDetermined;

        private void Awake()
        {
            if (!_conditionDisplay)
            {
                _conditionDisplay = GetComponentInChildren<TMP_Text>();
                if (!_conditionDisplay)
                {
                    this.LogErrorInDevelopment($"Missing text component to display conditions on {name}.");
                }
            }

            _conditions.Initialise();

            if (_startCondition)
            {
                _currentCondition = _startCondition;
            }
            else
            {
                _conditions.TryGet(out _currentCondition);
            }
        }

        private void Start()
        {
            _conditionDisplay.SetText(_currentCondition.ConditionDescription);
        }

        private void OnEnable()
        {
            _resultGenerator.OnResultCalculated += ApplyResultToCountdown;
        }

        private void OnDisable()
        {
            _resultGenerator.OnResultCalculated -= ApplyResultToCountdown;
        }

        private void ApplyResultToCountdown(int result)
        {
            bool isPositive = _currentCondition.IsResultPositive(result);
            if (isPositive)
            {
                _countdown.RemainingTime += result;
            }
            else
            {
                _countdown.RemainingTime -= result;
            }

            _onResultOutcomeDetermined.Invoke(isPositive);
            UpdateCondition();
        }

        private void UpdateCondition()
        {
            if (_conditions.TryGet(out var condition))
            {
                _currentCondition = condition;
                _conditionDisplay.SetText(condition.ConditionDescription);
            }
        }
    }
}