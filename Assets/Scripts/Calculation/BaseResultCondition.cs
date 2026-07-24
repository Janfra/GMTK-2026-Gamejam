using UnityEngine;

namespace GMTK.Calculation
{
    public abstract class BaseResultCondition : ScriptableObject
    {
        [SerializeField]
        private string _conditionDescription;

        public string ConditionDescription => _conditionDescription;
        public abstract bool IsResultPositive(int result);
    }
}