using System;
using System.Collections.Generic;
using System.Text;

namespace MathExpression
{
    /// <summary>
    /// Определяет метод получения значения выражения, исходя из значений переданных аргументов.
    /// </summary>
    public interface IExpression
    {
        /// <summary>
        /// Вычисляет значение математического выражения, исходя из значений переданных аргументов.
        /// </summary>
        /// <param name="arguments">Аргументы переменных математического выражения.</param>
        /// <returns>Результат вычисления.</returns>
        double GetValue(params double[] arguments);
    }
}
