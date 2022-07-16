using System;
using System.Collections.Generic;

namespace MathExpressionTree
{
    /// <summary>
    /// Представляет математическое выражение в виде дерева выражений.
    /// </summary>
    public class MathExpression : Function, IExpression
    {
        /// <summary>
        /// Начально математического выражения.
        /// </summary>
        IExpression Start { get; set; }

        /// <summary>
        /// Инициализирует дерево выражений.
        /// </summary>
        /// <param name="start">Начало дерева выражений.</param>
        public MathExpression(IExpression start)
        {
            Start = start ?? throw new ArgumentNullException("Элемент формулы не может быть null", nameof(start));
        }

        public MathExpression(string varibleName)
        {
            Start = new Variable((string)varibleName.Clone());
        }
        public MathExpression(double value)
        {
            Start = new Constant(value);
        }
        public MathExpression(MathOperation type, IExpression leftOperand, IExpression rightOperand)
        {
            Start = new Operation(type, leftOperand, rightOperand);
        }
        public MathExpression(SingleParametredFunctionType type, IExpression argument)
        {
            Start = new SingleParametredFunction(type, argument);
        }
        public MathExpression(DoubleParametredFunctionType type, IExpression lowArgument, IExpression highArgument)
        {
            Start = new DoubleParametredFunction(type, lowArgument, highArgument);
        }

        #region методы интерфейса IExpression

        public double GetValue(string[] names, double[] values) => Start.GetValue(names, values);

        public override void SetValuesForVariables(string[] names, double[] values)
        {
            if (Start is Variable variable) Start = SetValuesForVariables(variable, names, values);
            else if (Start is Function function) function.SetValuesForVariables(names, values);
        }

        public bool Equals(IExpression other)
        {
            return Start.Equals(other);
        }

        public IExpression Clone()
        {
            return new MathExpression(Start.Clone());
        }

        public IEnumerable<string> GetContainedVariables()
        {
            return Start.GetContainedVariables();
        }

        public IEnumerable<double> GetContainedConstants()
        {
            return Start.GetContainedConstants();
        }

        public IExpression GetPartialDifferentialBy(string variableName)
        {
            return Start.GetPartialDifferentialBy(variableName);
        }


        #endregion

        #region методы задания дерева

        /// <summary>
        /// Задает новое дерево выражений с корнем-операцией и операндами,
        /// одно из которых - данное дерево, другое - переданный параметр.
        /// Если параметр side будет true, поместит второй операнд в левое поддерево, иначе - в правое поддерево.
        /// </summary>
        /// <param name="type">Вид математической операции нового корня.</param>
        /// <param name="secondOperand">Второй операнд нового дерева.</param>
        /// <param name="side">Сторона дерева, в которую будет перемещен второй операнд.</param>
        public void Set(MathOperation type, IExpression secondOperand, bool side)
        {
            Start = side ? new Operation(type, secondOperand, Start) : new Operation(type, Start, secondOperand);
        }
        /// <summary>
        /// Задает новое дерево выражений с корнем-функцией, аргумент которой - данное дерево.
        /// </summary>
        /// <param name="type">Тип функции, которая станет корнем дерева.</param>
        public void Set(SingleParametredFunctionType type)
        {
            Start = new SingleParametredFunction(type, Start);
        }
        /// <summary>
        /// Задает новое дерево выражений с корнем-функцией, один аргумент которой - данное дерево, второй - переданный параметр.
        /// Если параметр side будет true, поместит второй операнд в левое поддерево, иначе - в правое поддерево.
        /// </summary>
        /// <param name="type">Тип функции, которая станет корнем дерева.</param>
        /// <param name="secondOperand">Второй операнд нового дерева.</param>
        /// <param name="side">Сторона дерева, в которую будет перемещен второй операнд.</param>
        public void Set(DoubleParametredFunctionType type, IExpression secondOperand, bool side)
        {
            Start = side ? new DoubleParametredFunction(type, secondOperand, Start) : new DoubleParametredFunction(type, Start, secondOperand);
        }

        /// <summary>
        /// Задает новое дерево выражений с корнем-функцией, аргумент которой - данное дерево.
        /// </summary>
        /// <param name="function">Функция, которая станет корнем дерева.</param>
        public void Set(Func<double, double> function)
        {
            Start = new SingleParametredFunction(function, Start);
        }
        /// <summary>
        /// Задает новое дерево выражений с корнем-функцией, один аргумент которой - данное дерево, второй - переданный параметр.
        /// Если параметр side будет true, поместит второй операнд в левое поддерево, иначе - в правое поддерево.
        /// </summary>
        /// <param name="function">Функция, которая станет корнем дерева.</param>
        /// <param name="secondOperand">Второй операнд нового дерева.</param>
        /// <param name="side">Сторона дерева, в которую будет перемещен второй операнд.</param>
        public void Set(Func<double, double, double> function, IExpression secondOperand, bool side)
        {
            Start = side ? new DoubleParametredFunction(function, secondOperand, Start) :
                new DoubleParametredFunction(function, Start, secondOperand);
        }

        #endregion

        #region Операторы

        /// <summary>
        /// Задает новое дерево выражений с корнем-операцией суммирования.
        /// </summary>
        /// <param name="left">Операнд, который будет перемещен в левое поддерево.</param>
        /// <param name="right">Операнд, который будет перемещен в правое поддерево.</param>
        /// <returns>Новое дерево выражений.</returns>
        public static MathExpression operator +(MathExpression left, MathExpression right)
        {
            return new MathExpression(new Operation(MathOperation.Addition, left.Start, right.Start));
        }
        /// <summary>
        /// Задает новое дерево выражений с корнем-операцией вычитания.
        /// </summary>
        /// <param name="left">Операнд, который будет перемещен в левое поддерево.</param>
        /// <param name="right">Операнд, который будет перемещен в правое поддерево.</param>
        /// <returns>Новое дерево выражений.</returns>
        public static MathExpression operator -(MathExpression left, MathExpression right)
        {
            return new MathExpression(new Operation(MathOperation.Substructing, left.Start, right.Start));
        }
        /// <summary>
        /// Задает новое дерево выражений с корнем-операцией умножения.
        /// </summary>
        /// <param name="left">Операнд, который будет перемещен в левое поддерево.</param>
        /// <param name="right">Операнд, который будет перемещен в правое поддерево.</param>
        /// <returns>Новое дерево выражений.</returns>
        public static MathExpression operator *(MathExpression left, MathExpression right)
        {
            return new MathExpression(new Operation(MathOperation.Multiplication, left.Start, right.Start));
        }
        /// <summary>
        /// Задает новое дерево выражений с корнем-операцией деления.
        /// </summary>
        /// <param name="left">Операнд, который будет перемещен в левое поддерево.</param>
        /// <param name="right">Операнд, который будет перемещен в правое поддерево.</param>
        /// <returns>Новое дерево выражений.</returns>
        public static MathExpression operator /(MathExpression left, MathExpression right)
        {
            return new MathExpression(new Operation(MathOperation.Division, left.Start, right.Start));
        }

        #endregion
    }
}
