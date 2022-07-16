using System;
using System.Collections.Generic;

namespace MathExpressionTree
{
    /// <summary>
    /// Представляет переменную, как узел дерева выражений.
    /// </summary>
    public class Variable : IExpression
    {
        /// <summary>
        /// Порядковый индекс переменной.
        /// </summary>
        public string Name { get;}

        /// <summary>
        /// Инициализирует переменную с именем, как узел дерева выражений.
        /// </summary>
        /// <param name="name">Имя переменной.</param>
        public Variable(string name)
        {
            Name = name;
        }

        public double GetValue(string[] names, double[] values)
        {
            int index = GetMassiveIndex(names);
            if (index < 0) throw new InvalidOperationException();
            else return values[index];
        }

        private int GetMassiveIndex(string[] names)
        {
            int index = 0;
            for (; Name != names[index] && index < names.Length; index++) { }

            if (index >= names.Length) return -1;
            else return index;
        }

        public bool Equals(IExpression other)
        {
            bool flag = false;
            if (other is Variable)
            {
                flag = Name == (other as Variable).Name;
            }

            return flag;
        }

        public IExpression Clone()
        {
            return new Variable((string)Name.Clone());
        }

        public IEnumerable<string> GetContainedVariables()
        {
            return new string[] { (string)Name.Clone() };
        }

        public IEnumerable<double> GetContainedConstants()
        {
            return new double[] { };
        }

        public IExpression GetPartialDifferentialBy(string variableName)
        {
            return Name == variableName ? new Constant(1) : new Constant(0);
        }

        public override string ToString()
        {
            return (string)Name.Clone();
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region

        public static explicit operator Variable(string name)
        {
            return new Variable(name);
        }

        /// <summary>
        /// Проверяет две перененные на равенство, сравнивая их имена.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если имена равны, и false в противном случае.</returns>
        public static bool operator ==(Variable left, Variable right)
        {
            return left.Equals(right);
        }
        /// <summary>
        /// Проверяет два объекта на равенство, сравнивая их деревья выражений.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если деревья выражений равны, и false в противном случае.</returns>
        public static bool operator ==(IExpression left, Variable right)
        {
            return left.Equals(right);
        }
        /// <summary>
        /// Проверяет два объекта на равенство, сравнивая их деревья выражений.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если деревья выражений равны, и false в противном случае.</returns>
        public static bool operator ==(Variable left, IExpression right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Проверяет две перененные на неравенство, сравнивая их имена.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если имена не равны, и false в противном случае.</returns>
        public static bool operator !=(Variable left, Variable right)
        {
            return !left.Equals(right);
        }
        /// <summary>
        /// Проверяет два объекта на неравенство, сравнивая их деревья выражений.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если деревья выражений не равны, и false в противном случае.</returns>
        public static bool operator !=(IExpression left, Variable right)
        {
            return !left.Equals(right);
        }
        /// <summary>
        /// Проверяет два объекта на неравенство, сравнивая их деревья выражений.
        /// </summary>
        /// <param name="left">Левый операнд сравления.</param>
        /// <param name="right">Правый операнд сравления.</param>
        /// <returns>Возвращает true, если деревья выражений не равны, и false в противном случае.</returns>
        public static bool operator !=(Variable left, IExpression right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Производит операцию сложения двух выражений.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Дерево суммы двух выражений.</returns>
        public static IExpression operator +(Variable left, Variable right)
        {
            return new Operation(MathOperation.Addition, left, right);
        }
        /// <summary>
        /// Производит операцию сложения двух выражений.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Дерево суммы двух выражений.</returns>
        public static IExpression operator +(IExpression left, Variable right)
        {
            return new Operation(MathOperation.Addition, left, right);
        }
        /// <summary>
        /// Производит операцию сложения двух выражений.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Дерево суммы двух выражений.</returns>
        public static IExpression operator +(Variable left, IExpression right)
        {
            return new Operation(MathOperation.Addition, left, right);
        }

        /// <summary>
        /// Производит операцию инветирования переменной.
        /// </summary>
        /// <param name="variable">Инвертируемая переменная.</param>
        /// <returns>Деверо выражения вычитания переменной.</returns>
        public static IExpression operator -(Variable variable)
        {
            return new Operation(MathOperation.Substructing, Constant.Zero, variable);
        }

        /// <summary>
        /// Производит операцию вычитания двух выражений.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Дерево разности двух выражений.</returns>
        public static IExpression operator -(Variable left, Variable right)
        {
            return new Operation(MathOperation.Substructing, left, right);
        }
        /// <summary>
        /// Производит операцию вычитания двух выражений.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Дерево разности двух выражений.</returns>
        public static IExpression operator -(IExpression left, Variable right)
        {
            return new Operation(MathOperation.Substructing, left, right);
        }
        /// <summary>
        /// Производит операцию вычитания двух выражений.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Дерево разности двух выражений.</returns>
        public static IExpression operator -(Variable left, IExpression right)
        {
            return new Operation(MathOperation.Substructing, left, right);
        }

        /// <summary>
        /// Производит операцию умножения двух выражений.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Дерево умножения двух выражений.</returns>
        public static IExpression operator *(Variable left, Variable right)
        {
            return new Operation(MathOperation.Multiplication, left, right);
        }
        /// <summary>
        /// Производит операцию умножения двух выражений.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Дерево умножения двух выражений.</returns>
        public static IExpression operator *(IExpression left, Variable right)
        {
            return new Operation(MathOperation.Multiplication, left, right);
        }
        /// <summary>
        /// Производит операцию умножения двух выражений.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Дерево умножения двух выражений.</returns>
        public static IExpression operator *(Variable left, IExpression right)
        {
            return new Operation(MathOperation.Multiplication, left, right);
        }

        /// <summary>
        /// Производит операцию деления двух выражений.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Дерево деления двух выражений.</returns>
        public static IExpression operator /(Variable left, Variable right)
        {
            return new Operation(MathOperation.Division, left, right);
        }
        /// <summary>
        /// Производит операцию деления двух выражений.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Дерево деления двух выражений.</returns>
        public static IExpression operator /(IExpression left, Variable right)
        {
            return new Operation(MathOperation.Division, left, right);
        }
        /// <summary>
        /// Производит операцию деления двух выражений.
        /// </summary>
        /// <param name="left">Левый операнд операции.</param>
        /// <param name="right">Правый операнд операции.</param>
        /// <returns>Дерево деления двух выражений.</returns>
        public static IExpression operator /(Variable left, IExpression right)
        {
            return new Operation(MathOperation.Division, left, right);
        }
        #endregion
    }
}