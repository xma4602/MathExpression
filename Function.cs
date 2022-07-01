using System;

namespace MathExpression
{
    public abstract class Function 
    {
        public abstract void SetValuesForVariables(string[] names, double[] values);

        protected IExpression SetValuesForVariables(Variable v, string[] names, double[] values)
        {
            int index = GetMassiveIndex(v, names, values);

            if (index < 0) return v;
            else return new Constant(values[index]);
        }

        protected int GetMassiveIndex(Variable v, string[] names, double[] values)
        {
            int index = 0;
            for (; v.Name != names[index] && index < names.Length; index++) { }

            if (index >= names.Length) return -1;
            else return index;
        }
    }
}
