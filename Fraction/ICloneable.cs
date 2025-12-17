namespace Laboratory6.Fraction
{
    /// <summary>
    /// Определяет метод для создания поверхностной копии объекта.
    /// </summary>
    public interface ICloneable
    {
        /// <summary>
        /// Создаёт новый объект, который является копией текущего экземпляра.
        /// </summary>
        /// <returns>Новый объект, который является копией этого экземпляра.</returns>
        object Clone();
    }

}
