using System;

namespace MathExpression
{
    public abstract class Function 
    {
        /// <summary>
        /// Задает переменным значения.
        /// Кажому элементу массива имен переменных поиндексно соответствует элемент массива значений.
        /// При нарушении этого соответствия выдаст ошибку.
        /// </summary>
        /// <param name="names">Набор имен переменных, которым будут присвоены значения.</param>
        /// <param name="values">Набор знаечний, которые будут присвоены переменным.</param>
        public abstract void SetValuesForVariables(string[] names, double[] values);

        /// <summary>
        /// Возвращает результат замены переменной на значение.
        /// </summary>
        /// <param name="v">Переменная для замены</param>
        /// <param name="names">Набор имен переменных, которым будут присвоены значения.</param>
        /// <param name="values">Набор знаечний, которые будут присвоены переменным.</param>
        /// <returns>Если переменная содержалась в наборе заменяемых переменных,
        /// возвращает константу-значение в виде узла дерева.
        /// Если переменная НЕ содержалась в наборе заменяемых переменных,
        /// возвращает эту же переменную в виде узла дерева.</returns>
        protected IExpression SetValuesForVariables(Variable v, string[] names, double[] values)
        {
            int index = GetMassiveIndex(v, names);

            if (index < 0) return v;
            else return new Constant(values[index]);
        }

        /// <summary>
        /// Ищет индекс переменной в массиве переменных.
        /// </summary>
        /// <param name="v">Переменная для замены</param>
        /// <param name="names">Набор имен переменных, которым будут присвоены значения.</param>
        /// <returns>Индекс переменной в массиве переменных, если она содержится там, и -1 в ротивном случае.</returns>
        protected int GetMassiveIndex(Variable v, string[] names)
        {
            int index = 0;
            for (; v.Name != names[index] && index < names.Length; index++) { }

            if (index >= names.Length) return -1;
            else return index;
        }
    }
}
