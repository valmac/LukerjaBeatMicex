using System.Collections.Generic;

namespace valmac.LukerjaBeatMicex
{
    /// <summary>
    /// класс индекса ММВБ
    /// </summary>
    public class MicexIndex
    {
        #region Fiedlds

        private readonly List<Stock> _items = new List<Stock>();

        #endregion Fiedlds

        #region Property

        /// <summary>
        /// бумаги индекса ММВБ
        /// </summary>
        public List<Stock> Items
        {
            get { return new List<Stock>(_items); }
        }

        #endregion Property

        /// <summary>
        /// Ctor
        /// </summary>
        public MicexIndex()
        {
            _items.Add(new Stock("RTKM", 270.15, 145.89));
            _items.Add(new Stock("LKOH", 1048.09, 1751.77));
            _items.Add(new Stock("SNGS", 18.16, 30.467));
            _items.Add(new Stock("GAZP", 115.4, 196.31));
            _items.Add(new Stock("ROSN", 115.21, 219.88));
            _items.Add(new Stock("SIBN", 67.24, 129.33));
            _items.Add(new Stock("MTSI", 124.16, 253.27));
            _items.Add(new Stock("AFLT", 35.76, 81.38));
            _items.Add(new Stock("PLZL", 812.21, 1853.58));
            _items.Add(new Stock("SNGSP", 6.41, 15.285));
            _items.Add(new Stock("OGKE", 1.13, 2.786));
            _items.Add(new Stock("TATN", 60.37, 147.5));
            _items.Add(new Stock("RBCI", 16.94, 43.09));
            _items.Add(new Stock("HYDR", 0.63, 1.631));
            _items.Add(new Stock("VTBR", 0.03, 0.1025));
            _items.Add(new Stock("MSNG", 0.99, 3.185));
            _items.Add(new Stock("GMKN", 1872.48, 6719.13));
            _items.Add(new Stock("NLMK", 35.9, 137.21));
            _items.Add(new Stock("URKA", 48.85, 208.46));
            _items.Add(new Stock("URSI", 0.33, 1.436));
            _items.Add(new Stock("SBER", 22.99, 107.16));
            _items.Add(new Stock("MAGN", 7.22, 33.74));
            _items.Add(new Stock("OGKC", 0.35, 1.682));
            _items.Add(new Stock("TRNFP", 7716.97, 41441.85));
            _items.Add(new Stock("CHMF", 94.25, 510.5));
            _items.Add(new Stock("PMTL", 103.97, 586.66));
            _items.Add(new Stock("RASP", 32.38, 199.21));
            _items.Add(new Stock("NOTK", 48.99, 302.67));
            _items.Add(new Stock("VTEL", 17.78, 139.66));
            _items.Add(new Stock("SBERP", 9.7, 76.52));
        }

        /// <summary>
        /// Получить список бумаг из векторa с номерами
        /// </summary>
        /// <param name="vector">номера бумаг</param>
        /// <returns>список бумаг</returns>
        public List<Stock> GetItems(object[] vector )
        {
            var ret = new List<Stock>();
            for (int i = 0; i < vector.Length;i++ )
            {
                ret.Add(_items[(int)vector[i]]);
            }
            return ret;
        }
    }
}