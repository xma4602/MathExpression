using System;
using System.Collections.Generic;

namespace MathExpressionTree
{

    /// <summary>
    /// Представляет математическую функцию с двумя параметрами, как узел дерева выражений.
    /// </summary>
    public class DoubleParametredFunction : Function, IExpression
    {
        /// <summary>
        /// Математическая функция выражения.
        /// </summary>
        public Func<double, double, double> Function { get; }
        /// <summary>
        /// Нижний аргумент функции.
        /// </summary>
        public IExpression LowArgument { get; set; }
        /// <summary>
        /// Верхний аргумент функции.
        /// </summary>
        public IExpression HighArgument { get; set; }
        /// <summary>
        /// Тип математической функции.
        /// </summary>
        public DoubleParametredFunctionType Type { get; }

        /// <summary>
        /// Инициализирует выражение с нетиповой двухпараметровой функцией.
        /// </summary>
        /// <param name="func">Математическая функция выражения.</param>
        /// <param name="lowArgument">Нижний аргумент функции.</param>
        /// <param name="highArgument">Верхний аргумент функции.</param>
        public DoubleParametredFunction(Func<double, double, double> func, IExpression lowArgument, IExpression highArgument)
             : this(func, DoubleParametredFunctionType.NotDefined, lowArgument, highArgument)
        {
        }
        /// <summary>
        /// Инициализирует выражение с типовой двухпараметровой функцией.
        /// </summary>
        /// <param name="type">Тип математической функции</param>
        /// <param name="lowArgument">Нижний аргумент функции.</param>
        /// <param name="highArgument">Верхний аргумент функции.</param>
        public DoubleParametredFunction(DoubleParametredFunctionType type, IExpression lowArgument, IExpression highArgument)
            : this (GetFuctionBy(type), type, lowArgument, highArgument)
        {
        }

        protected DoubleParametredFunction(Func<double, double, double> func, DoubleParametredFunctionType type, IExpression lowArgument, IExpression highArgument)
        {
            Type = type;
            LowArgument = lowArgument;
            HighArgument = highArgument;
            Function = func;
        }

        /// <summary>
        /// Возвращает типовую функцию в виде делегата.
        /// </summary>
        /// <param name="type">Тип математической функции.</param>
        /// <returns>Типовая математическая функция.</returns>
        protected static Func<double, double, double> GetFuctionBy(DoubleParametredFunctionType type)
        {
            Func<double, double, double> func = null;
            switch (type)
            {
                case DoubleParametredFunctionType.Pow: func = Math.Pow; break;
                case DoubleParametredFunctionType.Log: func = Math.Log; break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), $"Параметр должен принадлежать типу {nameof(DoubleParametredFunctionType)}.");
            }
            return func;
        }

        public override string ToString()
        {
            if (Type == DoubleParametredFunctionType.Pow) return $"({LowArgument})^({HighArgument})";
            else if (Type == DoubleParametredFunctionType.Log) return $"Log({LowArgument},{HighArgument})";
            else if (Type == DoubleParametredFunctionType.NotDefined) return $"{nameof(DoubleParametredFunctionType.NotDefined)}({LowArgument},{HighArgument})";
            else throw new ArgumentOutOfRangeException(nameof(Type), $"Параметр должен принадлежать типу {nameof(DoubleParametredFunctionType)}.");
        }

        public double GetValue(string[] names, double[] values)
        {
            return Function(LowArgument.GetValue(names, values), HighArgument.GetValue(names, values));
        }

        public override void SetValuesForVariables(string[] names, double[] values)
        {
            if (LowArgument is Variable variable) LowArgument = SetValuesForVariables(variable, names, values);
            else if (LowArgument is Function function) function.SetValuesForVariables(names, values);

            if (HighArgument is Variable variable1) HighArgument = SetValuesForVariables(variable1, names, values);
            else if (HighArgument is Function function) function.SetValuesForVariables(names, values);
        }

        public bool Equals(IExpression other)
        {
            bool flag = false;

            if (other is DoubleParametredFunction otherFunction)
            {
                flag = Type == otherFunction.Type && LowArgument.Equals(otherFunction.LowArgument) && HighArgument.Equals(otherFunction.HighArgument);
            }

            return flag;
        }

        public IExpression Clone()
        {
            return new DoubleParametredFunction(Type, LowArgument.Clone(), HighArgument.Clone());
        }

        public IEnumerable<string> GetContainedVariables()
        {
            List<string> varsLow = (List<string>)LowArgument.GetContainedVariables();
            List<string> varsHigh = (List<string>)HighArgument.GetContainedVariables();

            foreach (string var in varsHigh)
                if (!varsLow.Contains(var)) varsLow.Add(var);

            return varsLow;
        }

        public IEnumerable<double> GetContainedConstants()
        {
            List<double> varsLow = (List<double>)LowArgument.GetContainedConstants();
            List<double> varsHigh = (List<double>)HighArgument.GetContainedConstants();

            foreach (double var in varsHigh)
                if (!varsLow.Contains(var)) varsLow.Add(var);

            return varsLow;
        }

        public IExpression GetPartialDifferentialBy(string variableName)
        {
            throw new NotImplementedException();
        }

        protected static class DoubleParametredDifferentialFunction
        {
            public static IExpression Pow(IExpression lowArgument, IExpression highArgument, string variableName) 
            {
                throw new NotImplementedException();
            }

            public static IExpression Log(IExpression lowArgument, IExpression highArgument, string variableName) 
            {
                throw new NotImplementedException();
            }
        }
    }
}

