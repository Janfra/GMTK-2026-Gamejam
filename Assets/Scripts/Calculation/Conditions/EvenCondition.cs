using UnityEngine;

namespace GMTK.Calculation
{
    [CreateAssetMenu(fileName = "Even Condition", menuName = "Conditions/Even Condition")]
    public class EvenCondition : BaseResultCondition
    {
        public override bool IsResultPositive(int result)
        {
            return result % 2 == 0;
        }
    }
}