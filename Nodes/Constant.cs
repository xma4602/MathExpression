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
        /// Определяет, является ли данная константа математической константой
        /// </summary>
        public bool IsPI { get; }
        /// <summary>
        /// Определяет, является ли данная константа математической константой е.
        /// </summary>
        public bool IsE { get; }

        /// <summary>
        /// Представляет математическую константу π.
        /// </summary>
        public static Constant PI
        {
            get { return new Constant(Math.PI, true, false); }
        }

        /// <summary>
        /// Представляет математическую константу е.
        /// </summary>
        public static Constant E
        {
            get { return new Constant(Math.E, false , true); }
        }

        /// <summary>
        /// Инициализирует константу, как узел дерева выражений.
        /// </summary>
        /// <param name="value">Значение константы.</param>
        public Constant(double value): 
            this(value,
            value == Math.PI,
            value == Math.E)
        {

        }

        /// <summary>
        /// Инициализирует константу, как узел дерева выражений.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pi"></param>
        /// <param name="e"></param>
        protected Constant(double value, bool pi, bool e)
        {
            this.Value = value;
            IsPI = pi;
            IsE = e;
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

        public IExpression GetPartialDifferentialBy(string variableName)
        {
            return new Constant(0);
        }
    }
}
