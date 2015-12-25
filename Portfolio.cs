using System;
using System.Collections.Generic;

namespace valmac.LukerjaBeatMicex
{
    /// <summary>
    /// �������� �����
    /// </summary>
    public class Portfolio
    {
        #region Consts

        
        /// <summary>
        /// ������ � �������� 
        /// </summary>
        private const double INITIAL_CASH = 1000000;

        #endregion Consts

        #region Fields

        /// <summary>
        /// ������� � ������
        /// </summary>
        readonly Dictionary<Stock,uint> _positions = new Dictionary<Stock,uint>();

        /// <summary>
        /// ��� ������� ��� ������������ 
        /// </summary>
        private readonly double _positionWeight;

        #endregion Fields

        #region Properties

        /// <summary>
        /// ������ ��������
        /// </summary>
        public double Cash { get; private set; } 

        /// <summary>
        /// ������� ��������
        /// </summary>
        public Dictionary<Stock, uint> Positions
        {
            get { return new Dictionary<Stock, uint>(_positions); }
        }

        /// <summary>
        /// ��������� �������� �� t0
        /// </summary>
        public double Value0 { get; private set; }

        /// <summary>
        /// ��������� �������� �� t1
        /// </summary>
        public double Value1 { get; private set; }

        /// <summary>
        /// ��������� �������� � %
        /// </summary>
        public double Yield { get; private set; }

        public uint PositionsCount { get; private set; }
        #endregion Properties

        public Portfolio(uint positionsCount, double cash = 1000000)
        {
            PositionsCount = positionsCount;
            _positionWeight = 1.0 / PositionsCount;
            Cash = cash;
        }

        /// <summary>
        /// ������� �������� ����
        /// </summary>
        /// <param name="stock">����</param>
        /// <returns>��/���</returns>
        private bool CanAdd(Stock stock)
        {
            return Cash > GetQty(stock)*stock.Price0;
        }

        /// <summary>
        /// �������� ����
        /// </summary>
        /// <param name="stock">����</param>
        private void Add(Stock stock)
        {
            if(_positions.ContainsKey(stock) || !CanAdd(stock))
                return;
            uint value = GetQty(stock);
            _positions.Add(stock,value);
            Cash -= value * stock.Price0;
        }

        /// <summary>
        /// �������� ������ ������
        /// </summary>
        /// <param name="stockList">������ ������</param>
        public void Add(List<Stock> stockList)
        {
            foreach (var stock in stockList)
            {
                Add(stock);
            }
            Calculate();
        }

        /// <summary>
        /// ���������� ���������� ���-�� ��� �����
        /// </summary>
        /// <param name="stock">����</param>
        /// <returns>���������� ���-�� ��</returns>
        private uint GetQty(Stock stock)
        {
            return (uint)Math.Floor(INITIAL_CASH * _positionWeight / stock.Price0);
        }

        /// <summary>
        /// ��������� ��������
        /// </summary>
        private void Calculate()
        {
            Value0 = Cash;
            Value1 = Cash;
            foreach(var pair in _positions)
            {
                Value0+= pair.Key.Price0 * pair.Value;
                Value1+= pair.Key.Price1 * pair.Value;
            }
            Yield = Math.Round(Value1 / Value0 - 1)*100;
        }
    }
}