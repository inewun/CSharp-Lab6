namespace Laboratory6.Fraction
{
    /// <summary>
    /// Определяет расширенную функциональность для работы с дробями.
    /// </summary>
    internal interface IFraction
    {
        /// <summary>
        /// Получает вещественное (числовое) представление дроби.
        /// </summary>
        /// <returns>Вещественное значение, полученное путём деления числителя на знаменатель.</returns>
        double ToDouble();

        /// <summary>
        /// Устанавливает новое значение числителя дроби.
        /// </summary>
        /// <param name="value">Новое значение числителя.</param>
        void SetNumerator(int value);

        /// <summary>
        /// Устанавливает новое значение знаменателя дроби.
        /// </summary>
        /// <param name="value">Новое значение знаменателя (не может быть нулевым).</param>
        void SetDenominator(int value);
    }

}
