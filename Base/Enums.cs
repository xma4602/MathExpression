using System;

namespace MathExpressionTree
{
    /// <summary>
    /// Представляет перечисление основных математических функций с одним параметром.
    /// </summary>
    public enum SingleParametredFunctionType
    {
        NotDefined,
        Sin, Arcsin, Sh, Arcsh,
        Cos, Arccos, Ch, Arcch,
        Tg, Arctg, Th, Arcth,
        Ctg, Arcctg, Cth, Arccth,
        Ln, Log2, Log10,
        Abs, Sqr, Sqrt,
    }

    /// <summary>
    /// Представляет перечисление основных математических функций с двумя параметрами.
    /// </summary>
    public enum DoubleParametredFunctionType { NotDefined, Log, Pow }

    /// <summary>
    /// Представляет перечисление базовых арифметитечких операций: сложение, вычитание, умножение, деление.
    /// </summary>
    public enum MathOperation { Addition, Substructing, Multiplication, Division }
}
