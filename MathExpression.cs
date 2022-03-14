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
        public double GetValue(double argument)
        {
            return Start.GetValue(argument);
        }

        public static MathExpression SetOperation(MathOperation type, IExpression leftOperand, IExpression rightOperand)
        {
            return new MathExpression(new Operation(type, leftOperand, rightOperand));
        }
        public static MathExpression SetOperation(MathOperation type, MathExpression leftOperand, MathExpression rightOperand)
        {
            return new MathExpression(new Operation(type, leftOperand.Start, rightOperand.Start));
        }

        public static MathExpression SetFunction(Func<double, double> func, MathExpression formula)
        {
            return new MathExpression(new SingleParametredFunction(func, formula.Start));
        }
        public static MathExpression SetFunction(SingleParametredFunctionType type, MathExpression formula)
        {
            return new MathExpression(new SingleParametredFunction(type, formula.Start));
        }
        public static MathExpression SetFunction(Func<double, double, double> func, MathExpression lowFormula, MathExpression highFormula)
        {
            return new MathExpression(new DoubleParametredFunction(func, lowFormula.Start, highFormula.Start));
        }
        public static MathExpression SetFunction(DoubleParametredFunctionType type, MathExpression lowFormula, MathExpression highFormula)
        {
            return new MathExpression(new DoubleParametredFunction(type, lowFormula.Start, highFormula.Start));
        }

        public static MathExpression operator +(MathExpression left, MathExpression right)
        {
            return SetOperation(MathOperation.Addition, left.Start, right.Start);
        }
        public static MathExpression operator -(MathExpression left, MathExpression right)
        {
            return SetOperation(MathOperation.Substructing, left.Start, right.Start);
        }
        public static MathExpression operator *(MathExpression left, MathExpression right)
        {
            return SetOperation(MathOperation.Multiplication, left.Start, right.Start);
        }
        public static MathExpression operator /(MathExpression left, MathExpression right)
        {
            return SetOperation(MathOperation.Division, left.Start, right.Start);
        }
    }
}
