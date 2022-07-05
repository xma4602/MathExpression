using System;
using System.Collections.Generic;

namespace MathExpressionTree
{
    /// <summary>
    /// Представляет математическую операцию, как узел дерева выражений.
    /// </summary>
    public class Operation : Function, IExpression
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
            if (LeftOperand is Variable variable) LeftOperand = SetValuesForVariables(variable, names, values);
            else if (LeftOperand is Function function) function.SetValuesForVariables(names, values);

            if (RightOperand is Variable variable1) RightOperand = SetValuesForVariables(variable1, names, values);
            else if (RightOperand is Function function) function.SetValuesForVariables(names, values);
        }

        public bool Equals(IExpression other)
        {
            bool flag = false;

            if (other is Operation otherOperation)
            {
                if (Type.Equals(otherOperation.Type))
                {
                    if (Type == MathOperation.Addition || Type == MathOperation.Multiplication)
                    {
                        flag = (LeftOperand.Equals(otherOperation.LeftOperand) && RightOperand.Equals(otherOperation.RightOperand)) ||
                             (LeftOperand.Equals(otherOperation.RightOperand) && RightOperand.Equals(otherOperation.LeftOperand));
                    }
                    else
                    {
                        flag = LeftOperand.Equals(otherOperation.LeftOperand) && RightOperand.Equals(otherOperation.RightOperand);
                    }
                }
            }

            return flag;
        }

        public IExpression Clone()
        {
            return new Operation(Type, LeftOperand.Clone(), RightOperand.Clone());
        }

        public IEnumerable<string> GetContainedVariables()
        {
            List<string> vars1 = (List<string>)LeftOperand.GetContainedVariables();
            List<string> vars2 = (List<string>)RightOperand.GetContainedVariables();

            foreach (string var in vars2)
                if (!vars1.Contains(var)) vars1.Add(var);

            return vars1;
        }
    }
}
