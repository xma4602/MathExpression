using System;

namespace MathExpressionTree
{
    /// <summary>
    /// Определяет методы для узла дерева выражений.
    /// </summary>
    public interface IExpression : IEquatable<IExpression>
    {
        /// <summary>
        /// Вычисляет значение математического выражения, исходя из значений переданных аргументов.
        /// </summary>
        /// <param name="arguments">Аргументы переменных математического выражения.</param>
        /// <returns>Результат вычисления.</returns>
        double GetValue(string[] names, double[] values);
    }
}
