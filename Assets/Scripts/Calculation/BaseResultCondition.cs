using UnityEngine;

namespace GMTK.Calculation
{
    public abstract class BaseResultCondition : ScriptableObject
    {
        public abstract bool IsResultPositive(int result);
    }
}