namespace valmac.LukerjaBeatMicex
{
    /// <summary>
    /// Класс описывает ценную бумагу.
    /// Имя, Цена начальная(0) и конечная(1), приращение цены в %
    /// </summary>
    public class Stock
    {
        #region Properties 

        /// <summary>
        /// тикер
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// цена t0
        /// </summary>
        public double Price0 { get; }

        /// <summary>
        /// цена t1
        /// </summary>
        public double Price1 { get; }

        /// <summary>
        /// Изменение цены
        /// </summary>
        public double Chg { get; }

        #endregion Properties 

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">тикер</param>
        /// <param name="price0">цена t0</param>
        /// <param name="price1">цена t1</param>
        public Stock(string name, double price0, double price1)
        {
            Name = name;
            Price0 = price0;
            Price1 = price1;
            Chg = (Price1 / Price0 - 1 )* 100;
        }

       

        /// <summary>
        /// строковое представление
        /// </summary>
        /// <returns>строка</returns>
        public override string ToString()
        {
            return string.Concat("Stock:", Name, " ", Price0, "->", Price1,"=", Chg.ToString("N1"),"%");
        }

        #region Equality

        /// <summary>
        /// идентичность 
        /// </summary>
        /// <param name="other">другой сток</param>
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
        /// идентичность
        /// </summary>
        /// <param name="obj">другой объект</param>
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
        /// хэш-код
        /// </summary>
        /// <returns>код</returns>
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