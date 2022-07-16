using System;
using System.Collections.Generic;
using static MathExpressionTree.SingleParametredFunctionType;

namespace MathExpressionTree
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

        protected SingleParametredFunction(Func<double, double> func, IExpression argument, SingleParametredFunctionType type)
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
                case Sin: func = Math.Sin; break;
                case Arcsin: func = Math.Asin; break;
                case Sh: func = Math.Sinh; break;
                case Arcsh: func = (x) => Math.Log(x + Math.Sqrt(x * x + 1)); break;
                //косинусоподобные
                case Cos: func = Math.Cos; break;
                case Arccos: func = Math.Acos; break;
                case Ch: func = Math.Cosh; break;
                case Arcch: func = (x) => Math.Log(x + Math.Sqrt(x * x - 1)); break;
                //тангенсоподобные
                case Tg: func = Math.Tan; break;
                case Arctg: func = Math.Atan; break;
                case Th: func = Math.Tanh; break;
                case Arcth: func = (x) => 0.5 * Math.Log((1 + x) / (1 - x)); break;
                //котангенсоподобные
                case Ctg: func = (x) => 1 / Math.Tan(x); break;
                case Arcctg: func = (x) => 1 / Math.Atan(x); break;
                case Cth: func = (x) => 1 / Math.Tanh(x); break;
                case Arccth: func = (x) => 0.5 * Math.Log((x + 1) / (x - 1)); break;
                //логарифмы
                case Ln: func = Math.Log; break;
                case Log2: func = (x) => Math.Log(x, 2); break;
                case Log10: func = Math.Log10; break;
                //остальные
                case Abs: func = Math.Abs; break;
                case Sqr: func = (x) => Math.Pow(x, 2); break;
                case Sqrt: func = Math.Sqrt; break;

                //неопределенная
                case NotDefined:
                    throw new ArgumentException(nameof(type), $"Невозвожно определить функцию для значения {type}.");
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
            if (Argument is Variable variable) Argument = SetValuesForVariables(variable, names, values);
            else if (Argument is Function function) function.SetValuesForVariables(names, values);
        }

        public bool Equals(IExpression other)
        {
            bool flag = false;

            if (other is SingleParametredFunction otherFunction)
            {
                flag = Type == otherFunction.Type && Argument.Equals(otherFunction.Argument);
            }

            return flag;
        }

        public IExpression Clone()
        {
            return new SingleParametredFunction(Type, Argument.Clone());
        }

        public IEnumerable<string> GetContainedVariables()
        {
            return Argument.GetContainedVariables();
        }

        public IEnumerable<double> GetContainedConstants()
        {
            return Argument.GetContainedConstants();
        }

        public IExpression GetPartialDifferentialBy(string variableName)
        {
            throw new NotImplementedException();
        }

        protected static class SingleParametredDifferentialFunction
        {
            //синусоподобные
            public static IExpression Sin(IExpression argument, string variableName)
            {
                return new Operation(
                    MathOperation.Multiplication,
                    new SingleParametredFunction(SingleParametredFunctionType.Cos, argument),
                    argument.GetPartialDifferentialBy(variableName)
                    );
            }
            public static IExpression Arcsin(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            public static IExpression Sh(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            public static IExpression Arcsh(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            //косинусоподобные
            public static IExpression Cos(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            public static IExpression Arccos(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            public static IExpression Ch(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            public static IExpression Arcch(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            //тангенсоподобные
            public static IExpression Tg(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            public static IExpression Arctg(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            public static IExpression Th(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            public static IExpression Arcth(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            public static IExpression Ctg(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            public static IExpression Arcctg(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            public static IExpression Cth(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            public static IExpression Arccth(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            //логарифмы
            public static IExpression Ln(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            public static IExpression Log2(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            public static IExpression Log10(IExpression argument, string variableName)
            {
                throw new NotImplementedException();
            }
            //остальные
            public static IExpression Abs(IExpression argument, string variableName)
            {
                var x = argument.GetPartialDifferentialBy(variableName);
                return new Operation(
                       MathOperation.Division,
                       x,
                       new SingleParametredFunction(SingleParametredFunctionType.Abs, x)
                       );
            }
            public static IExpression Sqr(IExpression argument, string variableName)
            {
                return new Operation(
                   MathOperation.Multiplication,
                   new Constant(2),
                   argument.GetPartialDifferentialBy(variableName)
                   );
            }
            public static IExpression Sqrt(IExpression argument, string variableName)
            {
                return new Operation(
                        MathOperation.Multiplication,
                        new Constant(2),
                        argument.GetPartialDifferentialBy(variableName)
                        );
            }
        }

    }
}
