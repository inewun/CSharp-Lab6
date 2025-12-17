using System.Text.RegularExpressions;

namespace Laboratory6.Cat
{
    /// <summary>
    /// Класс представляет кота с методом мяуканья, реализующий интерфейс <see cref="IMeowable"/>.
    /// </summary>
    public class Cat : IMeowable
    {
        private string _name;

        /// <summary>
        /// Имя кота с валидацией формата, длины и содержимого.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// ВВозникает, если имя пустое, состоит только из пробелов или содержит недопустимые символы.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Возникает, если длина имени меньше 2 или больше 50 символов.
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
                    throw new ArgumentException("Имя не может быть пустым или содержать только пробелы.", nameof(Name));
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
        /// Инициализирует новый экземпляр класса <see cref="Cat"/> с указанным именем.
        /// </summary>
        /// <param name="name">
        /// Имя кота. Должно содержать от 2 до 50 символов и включать только буквы и пробелы.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Имя пустое, состоит только из пробелов или содержит недопустимые символы.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Длина имени меньше 2 или больше 50 символов.
        /// </exception>
        public Cat(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Выводит в консоль строку "{Имя}: мяу!".
        /// </summary>
        public void Meow()
        {
            Console.WriteLine($"{Name}: мяу!");
        }

        /// <summary>
        /// Выводит в консоль строку "{Имя}: мяу-мяу-...-мяу!" с указанным количеством мяуканий.
        /// </summary>
        /// <param name="times">Количество мяуканий (минимум 1).</param>
        /// <exception cref="ArgumentOutOfRangeException">Брошено при times меньше 1.</exception>
        public void Meow(int times) 
        {
            if (times < 1)
            {
                throw new ArgumentOutOfRangeException("Количество мяуканий должно быть не меньше 1.", nameof(times));
            }

            string meows = string.Join("-", Enumerable.Repeat("мяу", times));
            Console.WriteLine($"{Name}: {meows}!");
        }

        /// <summary>
        /// Возвращает строковое представление кота в формате "кот: {Имя}".
        /// </summary>
        /// <returns>Строку вида "кот: {Имя кота}".</returns>
        public override string ToString()
        {
            return $"кот: {Name}";
        }
    }
}
