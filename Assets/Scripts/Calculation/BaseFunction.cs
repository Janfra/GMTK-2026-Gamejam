using System;

namespace GMTK.Calculation
{
    [Serializable]
    public abstract class BaseFunction
    {
        public abstract int GetResult(int A, int B);
        public abstract string GetSymbol();
    }
}
