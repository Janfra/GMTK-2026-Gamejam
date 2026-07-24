using UnityEngine;

namespace GMTK.Calculation
{
    [CreateAssetMenu(fileName = "More Than Condition", menuName = "Conditions/More Than Condition")]
    public class MoreThanCondition : BaseResultCondition
    {
        [SerializeField]
        private int Value;

        public override bool IsResultPositive(int result)
        {
            return result > Value;
        }
    }
}