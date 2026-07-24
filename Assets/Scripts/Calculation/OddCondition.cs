using UnityEngine;

namespace GMTK.Calculation
{
    [CreateAssetMenu(fileName = "Odd Condition", menuName = "Conditions/Odd Condition")]
    public class OddCondition : BaseResultCondition
    {
        public override bool IsResultPositive(int result)
        {
            return result % 2 != 0;
        }
    }
}