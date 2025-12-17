using System.Text.RegularExpressions;

namespace Laboratory6.Cat
{
    /// <summary>
    /// Класс представляет собаку с методом мяуканья, реализующий интерфейс <see cref="IMeowable"/>.
    /// </summary>
    internal class Dog : IMeowable
    {
        private string _name;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Dog"/> с указанным именем.
        /// </summary>
        /// <param name="name">
        /// Имя собаки. Должно содержать от 2 до 50 символов и включать только буквы и пробелы.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Имя пустое, состоит только из пробелов или содержит недопустимые символы.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Длина имени меньше 2 или больше 50 символов.
        /// </exception>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Имя не может быть пустым или содержать только пробелы.", nameof(value));
                }

                if (2 > value.Length || value.Length > 50)
                {
                    throw new ArgumentOutOfRangeException(nameof(Name), value, "Имя должно быть от 2 до 50 символов.");
                }

                /* Объяснение регулярного выражения
                 *     ^           начало строки
                 *     a-zA-Z      латинские буквы
                 *     а-яА-ЯёЁ    кириллица (включая ё/Ё)
                 *     \s          пробелы/табы
                 *     $           конец строки
                 *     +           квантификатор - означает «один или более раз»
                 */
                if (!Regex.IsMatch(value, @"^[a-zA-Zа-яА-ЯёЁ\s]+$"))
                {
                    throw new ArgumentException("Имя может содержать только буквы и пробелы.", nameof(Name));
                }

                _name = value;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Dog"/> с указанным именем.
        /// </summary>
        /// <param name="name">
        /// Имя собаки. Должно содержать от 2 до 50 символов и включать только буквы и пробелы.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Имя пустое, состоит только из пробелов или содержит недопустимые символы.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Длина имени меньше 2 или больше 50 символов.
        /// </exception>
        public Dog(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Выводит в консоль строку "{Имя}: гав! (пытается мяукать)".
        /// </summary>
        public void Meow()
        {
            Console.WriteLine($"{Name}: гав! (пытается мяукнуть)");
        }


        /// <summary>
        /// Возвращает строковое представление кота в формате "собака: {Имя}".
        /// </summary>
        /// <returns>Строку вида "собака: {Имя кота}".</returns>
        public override string ToString()
        {
            return $"собака: {Name}";
        }
    }
}
