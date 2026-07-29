using Janito.EditorExtras;
using UnityEngine;

namespace GMTK.Calculation
{
    public abstract class BaseResultCondition : ScriptableObject
    {
        [SerializeField]
        private string[] _conditionDescriptions;

        public string ConditionDescription => GetRandomConditionDescription();
        public abstract bool IsResultPositive(int result);

        private string GetRandomConditionDescription()
        {
            if (_conditionDescriptions == null || _conditionDescriptions.Length == 0)
            {
                this.LogWarningInDevelopment($"No description is available for {name}, returning empty.");
                return "";
            }

            int index = Random.Range(0, _conditionDescriptions.Length);
            return _conditionDescriptions[index];
        }
    }
}