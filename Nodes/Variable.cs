using System;

namespace MathExpressionTree
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

        public override string ToString()
        {
            return $"{Name}";
        }

        public double GetValue(string[] names, double[] values)
        {
            int index = GetMassiveIndex(names);
            if (index < 0) throw new InvalidOperationException();
            else return values[index];
        }

        private int GetMassiveIndex(string[] names)
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

        public IExpression Clone()
        {
            return new Variable((string)Name.Clone());
        }
    }
}
