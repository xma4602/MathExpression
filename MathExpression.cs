using System;

namespace MathExpression
{
    /// <summary>
    /// Представляет математическое выражение в виде дерева выражений.
    /// </summary>
    public class MathExpression
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

        /// <summary>
        /// Вычисляет значение математического выражения, исходя из значений переданных аргументов.
        /// </summary>
        /// <param name="arguments">Аргументы переменных математического выражения.</param>
        /// <returns>Результат вычисления.</returns>
        public double GetValue(params double[] arguments) => Start.GetValue(arguments);
        /// <summary>
        /// Компелирует выражение, содержащееся в дереве, в делегат.
        /// </summary>
        /// <returns>Делегал, эквивалентный структуре дерева выражений.</returns>
        public Func<double[], double> Compile => Start.Compile();

        /// <summary>
        /// Задает новое дерево выражений с корнем-операцией и операндами,
        /// одно из которых - данное дерево, другое - переданный параметр.
        /// Если параметр side будет true, поместит второй операнд в левое поддерево, иначе - в правое поддерево.
        /// </summary>
        /// <param name="type">Вид математической операции нового корня.</param>
        /// <param name="SecondOperand">Второй операнд нового дерева.</param>
        /// <param name="side">Сторона дерева, в которую будет перемещен второй операнд.</param>
        public void Set(MathOperation type, IExpression SecondOperand, bool side)
        {
            Start = side ? new Operation(type, SecondOperand, Start) : new Operation(type, Start, SecondOperand);
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
        /// <param name="function">Функция, которая станет корнем дерева.</param>
        /// <param name="SecondOperand">Второй операнд нового дерева.</param>
        /// <param name="side">Сторона дерева, в которую будет перемещен второй операнд.</param>
        public void Set(Func<double, double, double> function, IExpression SecondOperand, bool side)
        {
            Start = side ? new DoubleParametredFunction(function, SecondOperand, Start) : new DoubleParametredFunction(function, Start, SecondOperand);
        }
        /// <summary>
        /// Задает новое дерево выражений с корнем-функцией, один аргумент которой - данное дерево, второй - переданный параметр.
        /// Если параметр side будет true, поместит второй операнд в левое поддерево, иначе - в правое поддерево.
        /// </summary>
        /// <param name="type">Тип функции, которая станет корнем дерева.</param>
        /// <param name="SecondOperand">Второй операнд нового дерева.</param>
        /// <param name="side">Сторона дерева, в которую будет перемещен второй операнд.</param>
        public void Set(DoubleParametredFunctionType type, IExpression SecondOperand, bool side)
        {
            Start = side ? new DoubleParametredFunction(type, SecondOperand, Start) : new DoubleParametredFunction(type, Start, SecondOperand);
        }

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
