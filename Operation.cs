using System;

namespace MathExpression
{
    /// <summary>
    /// Представляет математическую операцию, как узел дерева выражений.
    /// </summary>
    public class Operation : Function, IExpression, IEquatable<Operation>
    {
        /// <summary>
        /// Тип математической операции.
        /// </summary>
        public MathOperation Type { get; }
        /// <summary>
        /// Левый операнд математической операции.
        /// </summary>
        public IExpression LeftOperand { get; set; }
        /// <summary>
        /// Правый операнд математической операции.
        /// </summary>
        public IExpression RightOperand { get; set; }

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

        public double GetValue(string[] names, double[] values)
        {
            double result;
            var left = LeftOperand.GetValue(names, values);
            var right = RightOperand.GetValue(names, values);

            switch (Type)
            {
                case MathOperation.Addition:
                    result = left + right; break;
                case MathOperation.Substructing:
                    result = left - right; break;
                case MathOperation.Multiplication:
                    result = left * right; break;
                case MathOperation.Division:
                    result = left / right; break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), "Параметр должен принадлежать типу MathOperation.");
            }

            return result;

        }

        public override void SetValuesForVariables(string[] names, double[] values)
        {
            if (LeftOperand is Variable) LeftOperand = SetValuesForVariables((Variable)LeftOperand, names, values);
            else if (LeftOperand is Function) ((Function)LeftOperand).SetValuesForVariables(names, values);

            if (RightOperand is Variable) RightOperand = SetValuesForVariables((Variable)RightOperand, names, values);
            else if (RightOperand is Function) ((Function)RightOperand).SetValuesForVariables(names, values);
        }
    }
}
