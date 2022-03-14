using System;

namespace MathExpression
{
    /// <summary>
    /// Представляет переменную, как узел дерева выражений.
    /// </summary>
    public class Variable : IExpression, IEquatable<Variable>
    {
        /// <summary>
        /// Порядковый индекс переменной.
        /// </summary>
        public uint VariableIndex { get; }

        /// <summary>
        /// Инициализирует переменную, как узел дерева выражений.
        /// </summary>
        /// <param name="index">Порядковый индекс переменной.</param>
        public Variable(uint index)
        {
            if (index < 0) throw new ArgumentException("Недопустим индекс переменной ниже нуля", nameof(index));
            VariableIndex = index;
        }

        public double GetValue(double[] arguments)
        {
            return Compile()(arguments);
        }
        public Func<double[], double> Compile()
        {
            return (double[] args) => args[VariableIndex];
        }

        public bool Equals(Variable other)
        {
            return VariableIndex == other.VariableIndex;
        }
        public override string ToString()
        {
            return $"X{VariableIndex}";
        }               
    }
}
