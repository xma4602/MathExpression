using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Создает экземпляр с аналогичными значениями данного экземпляра.
        /// </summary>
        /// <returns>Клон экземпляра.</returns>
        IExpression Clone();
    }
}
