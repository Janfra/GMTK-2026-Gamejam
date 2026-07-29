using UnityEngine;

namespace GMTK.Calculation
{
    [CreateAssetMenu(fileName = "Less Than Condition", menuName = "Conditions/Less Than Condition")]
    public class LessThanCondition : BaseResultCondition
    {
        [SerializeField]
        private int Value;

        public override bool IsResultPositive(int result)
        {
            return result < Value;
        }
    }
}