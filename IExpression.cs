using System;

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
        /// <summary>
        /// Компелирует выражение, содержащееся в узле дерева, в делегат.
        /// </summary>
        /// <returns>Делегал, эквивалентный содержанию поддерева выражений.</returns>
        Func<double[], double> Compile();
    }
}
