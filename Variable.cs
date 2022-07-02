using System;

namespace MathExpression
{
    /// <summary>
    /// Представляет переменную, как узел дерева выражений.
    /// </summary>
    public class Variable : IExpression
    {
        /// <summary>
        /// Порядковый индекс переменной.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Инициализирует переменную с именем, как узел дерева выражений.
        /// </summary>
        /// <param name="name">Имя переменной.</param>
        public Variable(string name)
        {
            Name = name;
        }

        /*
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
        */

        public override string ToString()
        {
            return $"{Name}";
        }

        public double GetValue(string[] names, double[] values)
        {
            int index = GetMassiveIndex(names, values);
            if (index < 0) throw new InvalidOperationException();
            else return values[index];
        }

        private int GetMassiveIndex(string[] names, double[] values)
        {
            int index = 0;
            for (; Name != names[index] && index < names.Length; index++) { }

            if (index >= names.Length) return -1;
            else return index;
        }

        public bool Equals(IExpression other)
        {
            bool flag = false;
            if (other is Variable)
            {
                flag = Name == (other as Variable).Name;
            }

            return flag;
        }
    }
}
