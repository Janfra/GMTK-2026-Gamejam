using System;

namespace GMTK.Calculation
{
    [Serializable]
    public abstract class BaseFunction
    {
        public abstract bool TryGetResult(int A, int B, out int result);
        public abstract string GetSymbol();
    }
}
