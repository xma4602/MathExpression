using System;

namespace MathExpression
{
    /// <summary>
    /// Представляет математическую операцию, как узел дерева выражений.
    /// </summary>
    public class Operation : IExpression, IEquatable<Operation>
    {
        /// <summary>
        /// Тип математической операции.
        /// </summary>
        public MathOperation Type { get; }
        /// <summary>
        /// Левый операнд математической операции.
        /// </summary>
        public IExpression LeftOperand { get; }
        /// <summary>
        /// Правый операнд математической операции.
        /// </summary>
        public IExpression RightOperand { get; }

        /// <summary>
        /// Инициализирует экземпляр операции дерева выражений.
        /// </summary>
        /// <param name="type">Тип математической операции.</param>
        /// <param name="leftOperand">Левый операнд математической операции.</param>
        /// <param name="rightOperand">Правый операнд математической операции.</param>
        public Operation(MathOperation type, IExpression leftOperand, IExpression rightOperand)
        {
            Type = type;
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
        }

        public double GetValue(double[] arguments)
        {
            return Compile()(new double[] { LeftOperand.GetValue(arguments), RightOperand.GetValue(arguments) });
        }
        public Func<double[], double> Compile()
        {
            Func<double[], double> result;
            var left = LeftOperand.Compile();
            var right = RightOperand.Compile();

            switch (Type)
            {
                case MathOperation.Addition:
                    result = (double[] args) => left(args) + right(args); break;
                case MathOperation.Substructing:
                    result = (double[] args) => left(args) - right(args); break;
                case MathOperation.Multiplication:
                    result = (double[] args) => left(args) * right(args); break;
                case MathOperation.Division:
                    result = (double[] args) => left(args) / right(args); break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), "Параметр должен принадлежать типу MathOperation.");
            }

            return result;
        }

        public bool Equals(Operation other)
        {
            bool flag = Type.Equals(other.Type);

            if (flag)
            {
                if (Type == MathOperation.Addition || Type == MathOperation.Multiplication)
                {
                    flag = (LeftOperand.Equals(other.LeftOperand) && RightOperand.Equals(other.RightOperand)) ||
                         (LeftOperand.Equals(other.RightOperand) && RightOperand.Equals(other.LeftOperand));
                }
                else
                {
                    flag = LeftOperand.Equals(other.LeftOperand) && RightOperand.Equals(other.RightOperand);
                }
            }

            return flag;
        }
        public override string ToString()
        {
            string operation;

            switch (Type)
            {
                case MathOperation.Addition:
                    operation = "+"; break;
                case MathOperation.Substructing:
                    operation = "-"; break;
                case MathOperation.Multiplication:
                    operation = "*"; break;
                case MathOperation.Division:
                    operation = "/"; break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), $"Параметр должен принадлежать типу {nameof(MathOperation)}.");
            }

            return $"({LeftOperand} {operation} {RightOperand})";
        }
    }
}
