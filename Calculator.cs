using System;
using System.Text;

namespace valmac.LukerjaBeatMicex
{
    /// <summary>
    /// ����� ����������
    /// </summary>
    public class Calculator
    {

        #region Fields

        /// <summary>
        /// ���-�� ��������� ���� ������� ����������
        /// </summary>
        private ulong _yieldBelow;
        /// <summary>
        /// ���-�� ��������� ���� ������� ����������
        /// </summary>
        private ulong _yieldAbove;
        /// <summary>
        /// ������� ���������� ��������
        /// </summary>
        private readonly double _targetYield;
        /// <summary>
        /// ��������� �� ��������� 
        /// </summary>
        private readonly int _iterationMesage;
        /// <summary>
        /// ������������� ����� �������� ��������
        /// </summary>
        private Combinations _combinations;
        /// <summary>
        /// ��������� ������ �� ������� ����
        /// </summary>
        private readonly MicexIndex _index = new MicexIndex();

        #endregion Fields

        /// <summary>
        /// ����������� ���������� ����������
        /// </summary>
        /// <param name="n"></param>
        /// <returns>��������</returns>
        private static double Factorial(int n)
        {
            if (n > 1)
                return n*Factorial(n - 1);
            return 1;
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="targetYield">������� ����������</param>
        /// <param name="iterationMessage">��������� �� ������ �������� �����...</param>
        public Calculator(double targetYield, int iterationMessage)
        {
            _targetYield = targetYield;
            _iterationMesage = iterationMessage;
        }

        /// <summary>
        /// ����������������  ����� ����������
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
        /// ��������� ������
        /// </summary>
        /// <param name="count">���� ����� ��������</param>
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
        /// ��������� ��������� ��������
        /// </summary>
        /// <param name="i">����� ��������</param>
        /// <param name="stockVector">������ ����� �� ������� ����</param>
        /// <param name="count">����� ���-�� �������� (��� ���������� ����������)</param>
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
        /// ������ ����������
        /// </summary>
        /// <param name="i">����� ��������</param>
        /// <param name="count">���������� ��������</param>
        /// <returns>������ ����������</returns>
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
        /// ��������� ������������� ������� ������� �����
        /// </summary>
        /// <param name="vector">������</param>
        /// <returns>������</returns>
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