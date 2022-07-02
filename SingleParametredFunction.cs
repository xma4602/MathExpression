using System;

namespace MathExpression
{
    /// <summary>
    /// Представляет математическую функцию с одним параметром, как узел дерева выражений.
    /// </summary>
    public class SingleParametredFunction : Function, IExpression
    {
        /// <summary>
        /// Математическая функция выражения.
        /// </summary>
        public Func<double, double> Function { get; }
        /// <summary>
        /// Выражение-аргумент функции.
        /// </summary>
        public IExpression Argument { get; set; }
        /// <summary>
        /// Тип математической функции
        /// </summary>
        public SingleParametredFunctionType Type { get; }

        /// <summary>
        /// Инициализирует выражение с нетиповой однопараметровой функцией.
        /// </summary>
        /// <param name="func">Математическая функция выражения.</param>
        /// <param name="argument">Выражение-аргумент функции.</param>
        public SingleParametredFunction(Func<double, double> func, IExpression argument)
             : this(func, argument, SingleParametredFunctionType.NotDefined)
        {
        }
        /// <summary>
        /// Инициализирует выражение с типовой однопараметровой функцией.
        /// </summary>
        /// <param name="type">Тип математиеской функции</param>
        /// <param name="argument">Выражение-аргумент функции.</param>
        public SingleParametredFunction(SingleParametredFunctionType type, IExpression argument)
            : this(GetFuctionBy(type), argument, type)
        {
        }
        public SingleParametredFunction(Func<double, double> func, IExpression argument, SingleParametredFunctionType type)
        {
            Argument = argument;
            Function = func;
            Type = type;
        }

        /// <summary>
        /// Возвращает типовую функцию в виде делегата.
        /// </summary>
        /// <param name="type">Тип математической функции.</param>
        /// <returns>Типовая математическая функция.</returns>
        protected static Func<double, double> GetFuctionBy(SingleParametredFunctionType type)
        {
            Func<double, double> func = null;
            switch (type)
            {
                //синусоподобные
                case SingleParametredFunctionType.Sin: func = Math.Sin; break;
                case SingleParametredFunctionType.Arcsin: func = Math.Asin; break;
                case SingleParametredFunctionType.Sh: func = Math.Sinh; break;
                case SingleParametredFunctionType.Arcsh: func = (x) => Math.Log(x + Math.Sqrt(x * x + 1)); break;
                //косинусоподобные
                case SingleParametredFunctionType.Cos: func = Math.Cos; break;
                case SingleParametredFunctionType.Arccos: func = Math.Acos; break;
                case SingleParametredFunctionType.Ch: func = Math.Cosh; break;
                case SingleParametredFunctionType.Arcch: func = (x) => Math.Log(x + Math.Sqrt(x * x - 1)); break;
                //тангенсоподобные
                case SingleParametredFunctionType.Tg: func = Math.Tan; break;
                case SingleParametredFunctionType.Arctg: func = Math.Atan; break;
                case SingleParametredFunctionType.Th: func = Math.Tanh; break;
                case SingleParametredFunctionType.Arcth: func = (x) => 0.5 * Math.Log((1 + x) / (1 - x)); break;
                //котангенсоподобные
                case SingleParametredFunctionType.Ctg: func = (x) => 1 / Math.Tan(x); break;
                case SingleParametredFunctionType.Arcctg: func = (x) => 1 / Math.Atan(x); break;
                case SingleParametredFunctionType.Cth: func = (x) => 1 / Math.Tanh(x); break;
                case SingleParametredFunctionType.Arccth: func = (x) => 0.5 * Math.Log((x + 1) / (x - 1)); break;
                //логарифмы
                case SingleParametredFunctionType.Ln: func = Math.Log; break;
                case SingleParametredFunctionType.Log2: func = (x) => Math.Log(x, 2); break;
                case SingleParametredFunctionType.Log10: func = Math.Log10; break;

                case SingleParametredFunctionType.Abs: func = Math.Abs; break;
                case SingleParametredFunctionType.Sqr: func = (x) => Math.Pow(x, 2); break;
                case SingleParametredFunctionType.Sqrt: func = Math.Sqrt; break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), $"Параметр должен принадлежать типу {nameof(SingleParametredFunctionType)}.");
            }
            return func;
        }

        public override string ToString()
        {
            if (SingleParametredFunctionType.Sin < Type && Type < SingleParametredFunctionType.Sqrt) return $"{Type}({Argument})";
            else throw new ArgumentOutOfRangeException(nameof(Type), $"Параметр должен принадлежать типу {nameof(SingleParametredFunctionType)}.");
        }

        public double GetValue(string[] names, double[] values)
        {
            return Function(Argument.GetValue(names, values));
        }

        public override void SetValuesForVariables(string[] names, double[] values)
        {
            if (Argument is Variable) Argument = SetValuesForVariables((Variable)Argument, names, values);
            else if (Argument is Function) ((Function)Argument).SetValuesForVariables(names, values);
        }

        public bool Equals(IExpression other)
        {
            bool flag = false;

            if (other is SingleParametredFunction)
            {
                var otherFunction = (SingleParametredFunction)other;
                flag = Type == otherFunction.Type && Argument.Equals(otherFunction.Argument);
            }

            return flag;
        }
    }
}
