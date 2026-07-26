using GMTK.Calculation;
using Janito.EditorExtras;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GMTK
{
    [Serializable]
    public class FormulaComposition
    {
        public FormulaSource FirstDigitSlot;
        public FormulaSource FunctionSlot;
        public FormulaSource SecondDigitSlot;

        public void Verify()
        {
            if (FirstDigitSlot.OptionType != OptionType.Number)
            {
                LogLibrary.LogErrorInDevelopment<FormulaComposition>("Option in slot 1 must be a number option.");
            }

            if (FunctionSlot.OptionType != OptionType.Function)
            {
                LogLibrary.LogErrorInDevelopment<FormulaComposition>($"Option in slot 2 must be a function option.");
            }

            if (SecondDigitSlot.OptionType != OptionType.Number)
            {
                LogLibrary.LogErrorInDevelopment<FormulaComposition>("Option in slot 3 must be a number option.");
            }
        }

        public bool AreAllSet()
        {
            return FirstDigitSlot.HasSelection == true && FunctionSlot.HasSelection == true && SecondDigitSlot.HasSelection == true;
        }

        public bool TryGetResult(out int result)
        {
            if (!AreAllSet())
            {
                result = 0;
                return false;
            }

            result = FunctionSlot.Function.GetResult(FirstDigitSlot.Number, SecondDigitSlot.Number);
            return true;
        }

        public void AddToOnOptionUpdate(Action onUpdate)
        {
            FirstDigitSlot.OnOptionUpdated += onUpdate;
            FunctionSlot.OnOptionUpdated += onUpdate;
            SecondDigitSlot.OnOptionUpdated += onUpdate;
        }

        public void RemoveFromOnOptionUpdate(Action onUpdate)
        {
            FirstDigitSlot.OnOptionUpdated -= onUpdate;
            FunctionSlot.OnOptionUpdated -= onUpdate;
            SecondDigitSlot.OnOptionUpdated -= onUpdate;
        }

        public void ResetOptions()
        {
            FirstDigitSlot.ReleaseTile();
            SecondDigitSlot.ReleaseTile();
            FunctionSlot.ReleaseTile();
        }
    }

    [Serializable]
    public class FormulaSource
    {
        [SerializeField]
        private OptionComponent _optionComponent;
        [SerializeField]
        private int _numberDecimalPlace;

        public OptionType OptionType => _optionComponent.Type;
        public int Number
        {
            get
            {
                return _optionComponent.NumberComponent ? _optionComponent.NumberComponent.Number + (_numberDecimalPlace * 10) : 0;
            }
            set
            {
                if (_optionComponent.NumberComponent)
                {
                    _optionComponent.NumberComponent.Number = value;
                }
            }
        }
        public BaseFunction Function
        {
            get
            {
                return _optionComponent.FunctionComponent.Function;
            }
            set
            {
                if (_optionComponent.FunctionComponent)
                {
                    _optionComponent.FunctionComponent.Function = value;
                }
            }
        }
        public bool HasSelection => _optionComponent.HasSelection;
        public NumberComponent NumberComponent => _optionComponent.NumberComponent;
        public FunctionComponent FunctionComponent => _optionComponent.FunctionComponent;
        public void ReleaseTile() => _optionComponent.Deselect();

        public event Action OnOptionUpdated
        {
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

    [Serializable]
    public class ValueReassigner
    {
        [SerializeField]
        private List<int> _numberReassignOptions;
        [SerializeReference]
        [ChildTypeSelection(typeof(BaseFunction))]
        private List<BaseFunction> _functionReassignOptions;

        public void GetValue(out int value)
        {
            value = GetNewValue(_numberReassignOptions);
        }

        public void GetValue(out BaseFunction value)
        {
            value = GetNewValue(_functionReassignOptions);
        }

        private T GetNewValue<T>(List<T> source)
        {
            int index = Random.Range(0, source.Count);
            return source[index];
        }
    }

    [Serializable]
    public class Formula
    {
        public event Action<int> OnResultCalculated;

        [SerializeField]
        private FormulaComposition _formulaComposition;
        private int _result;

        public int LastResult => _result;
        public FormulaComposition FormulaComposition => _formulaComposition;

        public void OnEnable()
        {
            SetListeningForOptionChangesTo(true);
        }

        public void OnDisable()
        {
            SetListeningForOptionChangesTo(false);
        }

        public void Awake()
        {
            _formulaComposition.Verify();
        }

        private void SetListeningForOptionChangesTo(bool isEnabled)
        {
            if (isEnabled)
            {
                _formulaComposition.AddToOnOptionUpdate(TryGetResult);
            }
            else
            {
                _formulaComposition.RemoveFromOnOptionUpdate(TryGetResult);
            }
        }

        private void TryGetResult()
        {
            if (_formulaComposition.TryGetResult(out _result))
            {
                OnResultCalculated?.Invoke(_result);
            }
        }
    }

    public class FormulaComponent : MonoBehaviour
    {
        public event Action<int> OnResultCalculated
        {
            add => _formula.OnResultCalculated += value;
            remove => _formula.OnResultCalculated -= value;
        }

        [SerializeField]
        private TMP_Text _resultText;
        [SerializeField]
        private Formula _formula;
        [SerializeField]
        private ValueReassigner _sourceReassignment;

        private void OnEnable()
        {
            _formula.OnEnable();
            _formula.OnResultCalculated += UpdateText;
            _formula.OnResultCalculated += ReassignUsedValues;
            _formula.OnResultCalculated += ResetOptions;
        }

        private void OnDisable()
        {
            _formula.OnDisable();
            _formula.OnResultCalculated += UpdateText;
            _formula.OnResultCalculated += ReassignUsedValues;
            _formula.OnResultCalculated -= ResetOptions;
        }

        private void Awake()
        {
            _formula.Awake();
        }

        private void UpdateText(int result)
        {
            _resultText.SetText(result.ToString());
        }

        private void ReassignUsedValues(int _result)
        {
            int newNumber;
            BaseFunction newFunction;
            _sourceReassignment.GetValue(out newNumber);
            _formula.FormulaComposition.FirstDigitSlot.Number = newNumber;
            _sourceReassignment.GetValue(out newNumber);
            _formula.FormulaComposition.SecondDigitSlot.Number = newNumber;
            _sourceReassignment.GetValue(out newFunction);
            _formula.FormulaComposition.FunctionSlot.Function = newFunction;
        }

        private void ResetOptions(int _result)
        {
            IInventoryItem heldItem;
            if (TryGetInventoryItemFromFormulaSource(_formula.FormulaComposition.FirstDigitSlot, out heldItem))
            {
                heldItem.ReturnToInventory(true);
            }

            if (TryGetInventoryItemFromFormulaSource(_formula.FormulaComposition.FunctionSlot, out heldItem))
            {
                heldItem.ReturnToInventory(true);
            }

            if (TryGetInventoryItemFromFormulaSource(_formula.FormulaComposition.SecondDigitSlot, out heldItem))
            {
                heldItem.ReturnToInventory(true);
            }

            _formula.FormulaComposition.ResetOptions();
        }

        private bool TryGetInventoryItemFromFormulaSource(FormulaSource source, out IInventoryItem item)
        {
            switch (source.OptionType)
            {
                case OptionType.Number:
                    return source.NumberComponent.TryGetComponent(out item);
                case OptionType.Function:
                    return source.FunctionComponent.TryGetComponent(out item);
                default:
                    item = null;
                    return false;
            }
        }
    }
}
