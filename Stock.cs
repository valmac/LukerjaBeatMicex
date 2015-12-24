namespace valmac.LukerjaBeatMicex
{
    /// <summary>
    /// ����� ��������� ������ ������.
    /// ���, ���� ���������(0) � ��������(1), ���������� ���� � %
    /// </summary>
    public class Stock
    {
        #region Properties 

        /// <summary>
        /// �����
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// ���� t0
        /// </summary>
        public double Price0 { get; }

        /// <summary>
        /// ���� t1
        /// </summary>
        public double Price1 { get; }

        /// <summary>
        /// ��������� ����
        /// </summary>
        public double Chg { get; }

        #endregion Properties 

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="name">�����</param>
        /// <param name="price0">���� t0</param>
        /// <param name="price1">���� t1</param>
        public Stock(string name, double price0, double price1)
        {
            Name = name;
            Price0 = price0;
            Price1 = price1;
            Chg = (Price1 / Price0 - 1 )* 100;
        }

       

        /// <summary>
        /// ��������� �������������
        /// </summary>
        /// <returns>������</returns>
        public override string ToString()
        {
            return string.Concat("Stock:", Name, " ", Price0, "->", Price1,"=", Chg.ToString("N1"),"%");
        }

        #region Equality

        /// <summary>
        /// ������������ 
        /// </summary>
        /// <param name="other">������ ����</param>
        /// <returns></returns>
        public bool Equals(Stock other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.Name, Name) && other.Price0 == Price0 && other.Chg == Chg && other.Price1 == Price1;
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="obj">������ ������</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof(Stock))
            {
                return false;
            }
            return Equals((Stock)obj);
        }

        /// <summary>
        /// ���-���
        /// </summary>
        /// <returns>���</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Name != null ? Name.GetHashCode() : 0);
                result = (result * 397) ^ Price0.GetHashCode();
                result = (result * 397) ^ Chg.GetHashCode();
                result = (result * 397) ^ Price1.GetHashCode();
                return result;
            }
        }

        #endregion Equality
    }
}