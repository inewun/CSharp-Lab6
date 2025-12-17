namespace Laboratory6.Cat
{
    /// <summary>
    /// Предоставляет методы для запуска мяуканья у объектов, реализующих <see cref="IMeowable"/>.
    /// </summary>
    internal class Sounder
    {
        /// <summary>
        /// Последовательно вызывает метод <see cref="IMeowable.Meow"/> для всех переданных объектов
        /// и логирует по одному мяу для каждой сущности.
        /// </summary>
        /// <param name="meowers">Последовательность объектов, которые умеют мяукать.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="meowers"/> равен null.</exception>
        public static void Meow(IEnumerable<IMeowable> meowers)
        {
            if (meowers is null)
                throw new ArgumentNullException(nameof(meowers));

            foreach (var meower in meowers)
            {
                if (meower is null)
                    continue; // или бросить исключение, если так удобнее

                meower.Meow();
                MeowLogger.LogMeow(meower); // тут не важно, Cat, Dog или что-то ещё
            }
        }
    }
}