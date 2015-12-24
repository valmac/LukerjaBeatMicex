using System;

namespace valmac.LukerjaBeatMicex
{
    /// <summary>
    /// http://symmetrica.net/algorithms/combinations.htm
    /// (c) 2007 ������ ���������, ��������: anb@symmetrica.net 
    /// 
    /// ���� ������ ������� ��������� �������� �� ���, 
    /// ��� � ������ �� n �����  ���������� 
    /// �nm ��������� ������������ (���������) �������������������. 
    /// ����� Combinations ������� ��� �������  ��������� 
    /// ���� ������ outv ����� m, ������� �������� ������ 
    /// �� ������� �� ������� V. 
    /// ��� ��������� ������� ��������� ����� ������� ����� FirstCombination. 
    /// ���� ��� ������ �������� ������ �������� ��� ���������, 
    /// ����� ���������� �������� true, � ��������� ������ � false. 
    /// ��� ��������� ����������� ��������� (���� ������� �������) 
    /// ���������� ����� NextCombination. 
    /// ���� ��� ������ �������� ������ �������� ��� ���������, 
    /// ����� ���������� �������� true, � ��������� ������ � false. 
    /// ����� ������� ����� NextCombination ���������� true �nm � 2 ��� 
    /// (�����, � ������ ������ FirstCombination ���������� �nm ���������). 
    /// <code>
    /// �������� ����� ������������� ������ Combinations �������� ���:
    /// ���������� ��������� �� 7 �� 4 �� ������� V
    /// Combinations combinations = new Combinations(ref V, 4, 7);
    /// object[] outv;
    /// if (combinations.FirstCombination(out outv))
    /// do
    /// {
    /// ... // ���������. ������ outv �������� ��������� ��������
    /// }
    /// while (combinations.NextCombination(out outv));
    /// ...��������� ���������� ���������
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
                throw new Exception("m �� ����� ���� ������ n");
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