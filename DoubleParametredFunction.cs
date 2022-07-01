using System;

namespace MathExpression
{

    /// <summary>
    /// Представляет математическую функцию с двумя параметрами, как узел дерева выражений.
    /// </summary>
    public class DoubleParametredFunction : Function, IExpression, IEquatable<DoubleParametredFunction>
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

        public DoubleParametredFunction(Func<double, double, double> func, DoubleParametredFunctionType type, IExpression lowArgument, IExpression highArgument)
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

        public bool Equals(DoubleParametredFunction other)
        {
            return LowArgument.Equals(other) && LowArgument.Equals(other) && Type.Equals(other);
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
            if (LowArgument is Variable) LowArgument = SetValuesForVariables((Variable)LowArgument, names, values);
            else if (LowArgument is Function) ((Function)LowArgument).SetValuesForVariables(names, values);

            if (HighArgument is Variable) HighArgument = SetValuesForVariables((Variable)HighArgument, names, values);
            else if (HighArgument is Function) ((Function)HighArgument).SetValuesForVariables(names, values);
        }
    }
}

