using System;

namespace valmac.LukerjaBeatMicex
{
    /// <summary>
    /// класс вычислений
    /// </summary>
    public class Calculator
    {
        public const string FORMAT = "{0,-10}{1,-12}{2,-12:f2}{3,-12}{4,-12:f2}{5,-10:f2}";

        #region Fields

        /// <summary>
        /// Кол-во портфелей ниже целевой доходности
        /// </summary>
        private ulong _yieldBelow;
        /// <summary>
        /// кол-во портфелей выше целевой доходности
        /// </summary>
        private ulong _yieldAbove;
        /// <summary>
        /// уелевая доходность портфеля
        /// </summary>
        private readonly double _targetYield;
        /// <summary>
        /// сообщение об итерациях 
        /// </summary>
        private readonly int _iterationMesage;
        /// <summary>
        /// комбинаторный класс перебора варантов
        /// </summary>
        private Combinations _combinations;
        /// <summary>
        /// доступные бумаги из индекса ММВБ
        /// </summary>
        private readonly MicexIndex _index = new MicexIndex();

        #endregion Fields

        /// <summary>
        /// Рекурсивное вычисление факториала
        /// </summary>
        /// <param name="n"></param>
        /// <returns>значение</returns>
        private static double Factorial(uint n)
        {
            if (n > 1)
                return n*Factorial(n - 1);
            return 1;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="targetYield">целевая доходность</param>
        /// <param name="iterationMessage">сообщение на каждую итерацию номер...</param>
        public Calculator(double targetYield, int iterationMessage=1000000)
        {
            _targetYield = targetYield;
            _iterationMesage = iterationMessage;
        }

        /// <summary>
        /// Инициализировать  класс комбинаций
        /// </summary>
        private void InitCombinations(uint n, uint m)
        {
            var v = new object[m];
            for (var i = 0; i < v.Length; i++)
                v[i] = i;
            _combinations = new Combinations(ref v, n, m);
        }

        public static ulong CombinationsCount(uint n, uint m)
        {
            return Convert.ToUInt64(Factorial(m) / (Factorial(n) * Factorial(m - n)));
        }

        /// <summary>
        /// Запустить расчет
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        public void Start(uint n, uint m)
        {
            InitCombinations(n, m);
            var count = CombinationsCount(n, m);
            object[] stockVector;
            if (!_combinations.FirstCombination(out stockVector))
            {
                Console.WriteLine("ERROR: Can't get first vector of combinatons");
                return;
            }

            for (ulong i = 0; i < count; i++)
            {
                var yield = CalculatePortfolio(n, stockVector);
                if (yield > _targetYield)
                    _yieldAbove++;
                else
                    _yieldBelow++;

                if ((int)i % _iterationMesage == 0 && i > 0)
                    Console.WriteLine(StatString(i, count));

                if (!_combinations.NextCombination(out stockVector))
                {
                    CalculatePortfolio(n, stockVector);
                    break;
                }
            }

            Console.WriteLine(StatString(count,count));
        }

        /// <summary>
        /// Вычислить параметры портфеля
        /// </summary>
        /// <param name="i">номер итерации</param>
        /// <param name="portfolioSize"></param>
        /// <param name="stockVector">вектор бумаг из индекса ММВБ</param>
        private double CalculatePortfolio(uint portfolioSize, object[] stockVector)
        {
            var p = new Portfolio(portfolioSize);
            p.Add(_index.GetItems(stockVector));
            return p.Yield;
        }

        /// <summary>
        /// строка статистики
        /// </summary>
        /// <param name="i">номер итерации</param>
        /// <param name="count">количество итераций</param>
        /// <returns>строка статистики</returns>
        public string StatString(ulong i, ulong count)
        {
            return string.Format(FORMAT,
                    i,
                    //_targetYield,
                    _yieldAbove,
                    (double)_yieldAbove / i * 100,
                    _yieldBelow,
                    (double)_yieldBelow / i * 100,
                    (double)i / count * 100
                );
            //return string.Format("#{0} TargetYield={1:N2}\tAbove={2}({3:N1}%)\tBelow={4}({5:N1}%)\tcomplete={6:N2}%",
            //    i,
            //    _targetYield,
            //    _yieldAbove,
            //    (double)_yieldAbove / i * 100,
            //    _yieldBelow,
            //    (double)_yieldBelow / i * 100,
            //    (double)i / count *100);
            //return string.Concat(
            //    "#", i,
            //    " TargetYield=", _targetYield.ToString("N2"),
            //    " \tAbove=", _yieldAbove,
            //    " )", ((double)_yieldAbove / i * 100).ToString("N2"),
            //    " %)\tBelow=", _yieldBelow,
            //    " (", ((double)_yieldBelow / i * 100).ToString("N2"),
            //    " %)\tcomplete=", ((double)i / count * 100).ToString("N2"), "%"
            //    );
        }

        /// <summary>
        /// строковое представление вектора номеров бумаг
        /// </summary>
        /// <param name="vector">вектор</param>
        /// <returns>строка</returns>
        public static string ToString(object[] vector)
        {
            //var sb = new StringBuilder();
            //for (int i = 0; i < vector.Length; i++)
            //    sb.Append(vector[i]).Append(";");
            //return sb.ToString();
            return string.Join(";", (string[]) vector);
        }
    }
}