using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExpression
{
    /// <summary>
    /// Представляет константное значение, как узел дерева выражений.
    /// </summary>
    public class Constant : IExpression, IEquatable<Constant>
    {
        /// <summary>
        /// Значение константы.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Инициализирует константу, как узел дерева выражений.
        /// </summary>
        /// <param name="value">Значение константы.</param>
        public Constant(double value)
        {
            Value = value;
        }

        public double GetValue(params double[] arguments)
        {
            return Compile()(arguments);
        }

        public Func<double[], double> Compile()
        {
           return (double[] args) => Value;
        }

        public bool Equals(Constant other)
        {
            return Value == other.Value;
        }
        public override string ToString()
        {
            return $"{Value}";
        }

        
    }
}
