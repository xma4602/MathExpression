using System;

namespace MathExpression
{
    /// <summary>
    /// Представляет переменную, как узел дерева выражений.
    /// </summary>
    public class Variable : IExpression, IEquatable<Variable>
    {
        /// <summary>
        /// Порядковый индекс переменной.
        /// </summary>
        public string Name { get; set; }
        public string[] Indexes { get; set; }

        /// <summary>
        /// Инициализирует переменную с именем, как узел дерева выражений.
        /// </summary>
        /// <param name="name">Имя переменной.</param>
        public Variable(string name): this(name, null)
        {

        }

        /// <summary>
        /// Инициализирует переменную с именем и индексами, как узел дерева выражений.
        /// </summary>
        /// <param name="name">Имя переменной.</param>
        /// <param name="indexes">Имена индексов переменной.</param>
        public Variable(string name, params string[] indexes)
        {
            Name = name;
            Indexes = indexes;
        }

        public double GetValue(double[] arguments)
        {
            return Compile()(arguments);
        }

        public Func<double[], double> Compile()
        {
            return (double[] args) => args[Name];
        }

        public bool Equals(Variable other)
        {
            return Name == other.Name;
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
