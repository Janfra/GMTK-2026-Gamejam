using UnityEngine;

namespace GMTK.Calculation
{
    [CreateAssetMenu(fileName = "Any Condition", menuName = "Conditions/Any Condition")]
    public class AnyCondition : BaseResultCondition
    {
        public override bool IsResultPositive(int result)
        {
            return true;
        }
    }
}