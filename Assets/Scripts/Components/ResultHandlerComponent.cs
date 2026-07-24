using System;
using UnityEngine;

namespace GMTK
{
    public class ResultHandlerComponent : MonoBehaviour
    {
        [SerializeField]
        private FormulaComponent _resultGenerator;
        [SerializeField]
        private CountdownComponent _countdown;

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
            _countdown.RemainingTime += result;
        }
    }
}