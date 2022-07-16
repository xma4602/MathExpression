using System;
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
        public bool IsPI => Value == Math.PI;
        /// <summary>
        /// Определяет, является ли данная константа математической константой е.
        /// </summary>
        public bool IsE => Value == Math.E;

        /// <summary>
        /// Представляет математическую константу π.
        /// </summary>
        public static Constant PI
        {
            get { return new Constant(Math.PI); }
        }

        /// <summary>
        /// Представляет математическую константу е.
        /// </summary>
        public static Constant E
        {
            get { return new Constant(Math.E); }
        }

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
            return other is Constant constant ? constant.Value == Value : false;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region операторы

        public static explicit operator double(Constant constant)
        {
            return constant.Value;
        }
        public static explicit operator Constant(double constant)
        {
            return new Constant(constant);
        }

        /// <summary>
        /// Проверяет два объекта на равенство, сравнивая их константные значения.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если константные значения равны, и false в противном случае.</returns>
        public static bool operator ==(Constant left, Constant right)
        {
            return left.Value == right.Value;
        }
        /// <summary>
        /// Проверяет два объекта на равенство, сравнивая их константные значения.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если константные значения равны, и false в противном случае.</returns>
        public static bool operator ==(IExpression left, Constant right)
        {
            return right.Equals(left);
        }
        /// <summary>
        /// Проверяет два объекта на равенство, сравнивая их константные значения.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если константные значения равны, и false в противном случае.</returns>
        public static bool operator ==(Constant left, IExpression right)
        {
            return left.Equals(right);
        }
        /// <summary>
        /// Проверяет два объекта на равенство, сравнивая их константные значения.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если константные значения равны, и false в противном случае.</returns>
        public static bool operator ==(double left, Constant right)
        {
            return left == right.Value;
        }
        /// <summary>
        /// Проверяет два объекта на равенство, сравнивая их константные значения.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если константные значения равны, и false в противном случае.</returns>
        public static bool operator ==(Constant left, double right)
        {
            return left.Value == right;
        }

        /// <summary>
        /// Проверяет два объекта на неравенство, сравнивая их константные значения.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если константные значения не равны, и false в противном случае.</returns>
        public static bool operator !=(Constant left, Constant right)
        {
            return left.Value != right.Value;
        }
        /// <summary>
        /// Проверяет два объекта на неравенство, сравнивая их константные значения.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если константные значения не равны, и false в противном случае.</returns>
        public static bool operator !=(IExpression left, Constant right)
        {
            return !right.Equals(left);
        }
        /// <summary>
        /// Проверяет два объекта на неравенство, сравнивая их константные значения.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если константные значения не равны, и false в противном случае.</returns>
        public static bool operator !=(Constant left, IExpression right)
        {
            return !left.Equals(right);
        }
        /// <summary>
        /// Проверяет два объекта на неравенство, сравнивая их константные значения.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если константные значения не равны, и false в противном случае.</returns>
        public static bool operator !=(double left, Constant right)
        {
            return left != right.Value;
        }
        /// <summary>
        /// Проверяет два объекта на неравенство, сравнивая их константные значения.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если константные значения не равны, и false в противном случае.</returns>
        public static bool operator !=(Constant left, double right)
        {
            return left.Value != right;
        }

        /// <summary>
        /// Инвертирует константу
        /// </summary>
        /// <param name="constant">Инверируемая костанта</param>
        /// <returns>Возвращает константу с противоположным значением.</returns>
        public static Constant operator -(Constant constant)
        {
            return new Constant(-constant.Value);
        }

        /// <summary>
        /// Производит операцию сложения двух констант.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Результат сложения константных значений.</returns>
        public static Constant operator +(Constant left, Constant right)
        {
            return new Constant(left.Value + right.Value);
        }
        /// <summary>
        /// Производит операцию вычитания двух констант.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Результат вычитания константных значений.</returns>
        public static Constant operator -(Constant left, Constant right)
        {
            return new Constant(left.Value - right.Value);
        }
        /// <summary>
        /// Производит операцию умножения двух констант.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Результат умножения константных значений.</returns>
        public static Constant operator *(Constant left, Constant right)
        {
            return new Constant(left.Value * right.Value);
        }
        /// <summary>
        /// Производит операцию деления двух констант.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Результат деления константных значений.</returns>
        public static Constant operator /(Constant left, Constant right)
        {
            return new Constant(left.Value / right.Value);
        }

        #endregion
    }
}
