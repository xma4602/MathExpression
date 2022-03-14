using System;

namespace MathExpression
{
    /// <summary>
    /// Представляет перечисление основных математических функций с двумя параметрами.
    /// </summary>
    public enum DoubleParametredFunctionType { NotDefined, Log, Pow }

    /// <summary>
    /// Представляет математическую функцию с двумя параметрами, как узел дерева выражений.
    /// </summary>
    public class DoubleParametredFunction : IExpression, IEquatable<DoubleParametredFunction>
    {
        /// <summary>
        /// Математическая функция выражения.
        /// </summary>
        public Func<double, double, double> Function { get; }
        /// <summary>
        /// Нижний аргумент функции.
        /// </summary>
        public IExpression LowArgument { get; }
        /// <summary>
        /// Верхний аргумент функции.
        /// </summary>
        public IExpression HighArgument { get; }
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
        {
            Function = func;
            LowArgument = lowArgument;
            HighArgument = highArgument;
            Type = DoubleParametredFunctionType.NotDefined;
        }
        /// <summary>
        /// Инициализирует выражение с типовой двухпараметровой функцией.
        /// </summary>
        /// <param name="type">Тип математической функции</param>
        /// <param name="lowArgument">Нижний аргумент функции.</param>
        /// <param name="highArgument">Верхний аргумент функции.</param>
        public DoubleParametredFunction(DoubleParametredFunctionType type, IExpression lowArgument, IExpression highArgument)
        {
            Type = type;
            LowArgument = lowArgument;
            HighArgument = highArgument;
            Function = GetFuctionBy(type);
        }

        /// <summary>
        /// Возвращает типовую функцию в виде делегата.
        /// </summary>
        /// <param name="type">Тип математической функции.</param>
        /// <returns>Типовая математическая функция.</returns>
        public Func<double, double, double> GetFuctionBy(DoubleParametredFunctionType type)
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

        public double GetValue(double[] arguments)
        {
            return Compile()(arguments);
        }
        public Func<double[], double> Compile()
        {
            return (double[] args) => Function(LowArgument.Compile()(args), HighArgument.Compile()(args));
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
    }
}

