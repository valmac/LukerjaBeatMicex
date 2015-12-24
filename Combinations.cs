using System;

namespace valmac.LukerjaBeatMicex
{
    /// <summary>
    /// http://symmetrica.net/algorithms/combinations.htm
    /// (c) 2007 Андрей Боровский, контакты: anb@symmetrica.net 
    /// 
    /// Суть работы данного алгоритма основана на том, 
    /// что в наборе из n чисел  существует 
    /// Сnm монотонно возрастающих (убывающих) последовательностей. 
    /// Класс Combinations создает для каждого  сочетания 
    /// свой вектор outv длины m, который содержит ссылки 
    /// на объекты из вектора V. 
    /// Для получения первого сочетания нужно вызвать метод FirstCombination. 
    /// Если при данных условиях выбора возможны еще сочетания, 
    /// метод возвращает значение true, в противном случае – false. 
    /// Для получения последующих сочетаний (если таковые имеются) 
    /// вызывается метод NextCombination. 
    /// Если при данных условиях выбора возможны еще сочетания, 
    /// метод возвращает значение true, в противном случае – false. 
    /// Таким образом метод NextCombination возвращает true Сnm – 2 раз 
    /// (всего, с учетом вызова FirstCombination получается Сnm сочетаний). 
    /// <code>
    /// Типичная схема использования класса Combinations выглядит так:
    /// Генерируем сочетания из 7 по 4 из вектора V
    /// Combinations combinations = new Combinations(ref V, 4, 7);
    /// object[] outv;
    /// if (combinations.FirstCombination(out outv))
    /// do
    /// {
    /// ... // Обработка. Вектор outv содержит очередное сочетаие
    /// }
    /// while (combinations.NextCombination(out outv));
    /// ...Обработка последнего сочетания
    ///</code>
    /// </summary>
    public class Combinations
    {

        #region Fields

        private readonly int[] _counters;
        private readonly uint _m;
        private readonly uint _n;
        private readonly object[] _vector;

        #endregion Fields

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        public Combinations(ref object[] vector, uint m, uint n)
        {
            if (m > n)
                throw new Exception("m не может быть больше n");
            _vector = vector;
            _m = m;
            _n = n;
            _counters = new int[m];
            for (int i = 0; i < _counters.Length; i++)
            {
                _counters[i] = i;
            }
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outv"></param>
        /// <returns></returns>
        public bool FirstCombination(out object[] outv)
        {
            outv = new object[_m];
            for (uint i = 0; i < _m; i++)
                outv[i] = _vector[_counters[i]];
            return (_m == _n) || (_m == 0) ? false : true;
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outv"></param>
        /// <returns></returns>
        public bool NextCombination(out object[] outv)
        {
            if (_counters[_m - 1] < _n-1)
                _counters[_m-1]++;
            else
            {
                for (uint i = _m - 2; i >= 0; i--)
                {
                    if (_counters[i] < _n - _m + i)
                    {
                        _counters[i]++;
                        for (uint j = i + 1; j < _m; j++)
                            _counters[j] = _counters[j - 1] + 1;
                        break;
                    }
                }
            }
            outv = new object[_m];
            for (uint i = 0; i < _m; i++)
                outv[i] = _vector[_counters[i]];
           
            if (_counters[0] == _n - _m)
                return false;
            return true;
        }
    }
}