﻿using System;
using System.Collections.Generic;

namespace MathExpressionTree
{
    /// <summary>
    /// Представляет константное значение, как узел дерева выражений.
    /// </summary>
    public class Constant : IExpression
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

        public override string ToString()
        {
            return $"{Value}";
        }

        public double GetValue(string[] names, double[] values)
        {
            return Value;
        }

        public bool Equals(IExpression other)
        {
            bool flag = false;
            if (other is Constant)
            {
                flag = Value == (other as Constant).Value;
            }

            return flag;
        }

        public IExpression Clone()
        {
            return new Constant(Value);
        }

        public IEnumerable<string> GetContainedVariables()
        {
            return new string[] { };
        }

        public IEnumerable<double> GetContainedConstants()
        {
            return new double[] { Value };
        }
    }
}
