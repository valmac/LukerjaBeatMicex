using System;
using System.Text;

namespace valmac.LukerjaBeatMicex
{
    /// <summary>
    /// класс вычислений
    /// </summary>
    public class Calculator
    {

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
        private static double Factorial(int n)
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
        public Calculator(double targetYield, int iterationMessage)
        {
            _targetYield = targetYield;
            _iterationMesage = iterationMessage;
        }

        /// <summary>
        /// Инициализировать  класс комбинаций
        /// </summary>
        private void InitCombinations()
        {
            var v = new object[30];
            for (var i = 0; i < v.Length; i++)
                v[i] = i;
            _combinations = new Combinations(ref v, 8, 30);
        }

        public void Start()
        {
            Start(Convert.ToUInt64(Factorial(30) / (Factorial(8) * Factorial(30 - 8))));
        }

        /// <summary>
        /// Запустить расчет
        /// </summary>
        /// <param name="count">макс колво итераций</param>
        public void Start(ulong count)
        {
            Console.WriteLine("Start test for {0:N0} iterations",count);
            InitCombinations();
            object[] stockVector;
            if(!_combinations.FirstCombination(out stockVector))return;

            for (ulong i = 0; i < count; i++)
            {
                CalculatePortfolio(i, stockVector,count);

                if(!_combinations.NextCombination(out stockVector))
                {
                    CalculatePortfolio(i, stockVector, count);
                    break;
                }
            }

            Console.WriteLine(StatString(count,count));
        }

        /// <summary>
        /// Вычислить параметры портфеля
        /// </summary>
        /// <param name="i">номер итерации</param>
        /// <param name="stockVector">вектор бумаг из индекса ММВБ</param>
        /// <param name="count">общее кол-во итераций (для показателя готовности)</param>
        private void CalculatePortfolio(ulong i, object[] stockVector, ulong count)
        {
            var p = new Portfolio();
            p.Add(_index.GetItems(stockVector));
            if (p.Yield > _targetYield)
                _yieldAbove++;
            else
                _yieldBelow++;

            if((int)i%_iterationMesage==0)
                Console.WriteLine(StatString(i, count));
        }

        /// <summary>
        /// строка статистики
        /// </summary>
        /// <param name="i">номер итерации</param>
        /// <param name="count">количество итераций</param>
        /// <returns>строка статистики</returns>
        public string StatString(ulong i, ulong count)
        {
            //return string.Format("#{0} TargetYield={1:N2}\tAbove={2}({3:N1}%)\tBelow={4}({5:N1}%)\tcomplete={6:N2}%",
            //    i,
            //    _targetYield,
            //    _yieldAbove,
            //    (double)_yieldAbove / i * 100,
            //    _yieldBelow,
            //    (double)_yieldBelow / i * 100,
            //    (double)i / count *100);
            return string.Concat(
                "#", i,
                " TargetYield=", _targetYield.ToString("N2"),
                " \tAbove=", _yieldAbove,
                " )", ((double)_yieldAbove / i * 100).ToString("N2"),
                " %)\tBelow=", _yieldBelow,
                " (", ((double)_yieldBelow / i * 100).ToString("N2"),
                " %)\tcomplete=", ((double)i / count * 100).ToString("N2"), "%"
                );
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