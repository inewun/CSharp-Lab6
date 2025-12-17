using System.Text.RegularExpressions;

namespace Laboratory6
{
    public class Checker
    {
        /// <summary>
        /// Запрашивает у пользователя целое число с консоли и проверяет корректность ввода.
        /// </summary>
        /// <param name="message">Сообщение перед вводом.</param>
        /// <param name="left">Минимальное значение. По умолчанию <see cref="int.MinValue"/>.</param>
        /// <param name="right">Максимальное значение. По умолчанию <see cref="int.MaxValue"/>.</param>
        /// <param name="defaultValue">Значение по умолчанию при Enter (null = без значения по умаолчанию).</param>
        /// <returns>Корректное число в диапазоне.</returns>
        static public int InputIntegerWithValidation(string message, int left = int.MinValue, int right = int.MaxValue, int? defaultValue = null)
        {
            if ((defaultValue.HasValue) && ((defaultValue.Value < left) || (defaultValue.Value > right)))
            {
                throw new ArgumentOutOfRangeException(nameof(defaultValue), $"Значение по умолчанию {defaultValue.Value} вне диапазона [{left}; {right}]");
            }

            bool ok;
            int value;
            do
            {
                Console.Write((defaultValue.HasValue ? $"{message} (Enter = {defaultValue.Value}) " : message) + ": ");

                string input = Console.ReadLine() ?? string.Empty;
                input = input.Trim();

                if (string.IsNullOrEmpty(input) && defaultValue.HasValue)
                {
                    var tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Использовано значение по умолчанию: {defaultValue}");
                    Console.ForegroundColor = tmp;
                    return defaultValue.Value;
                }

                ok = int.TryParse(input, out value);
                if (ok && (value < left || value > right))
                {
                    ok = false;
                }

                if (!ok)
                {
                    ConsoleColor tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nВведенные данные имеют неверный формат или не принадлежат диапазону [{left}; {right}]");
                    Console.WriteLine("Повторите ввод\n");
                    Console.ForegroundColor = tmp;
                }
            } while (!ok);

            return value;
        }


        private const string  defaultAllowedChars = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюяABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_ ,";
        
        /// <summary>
        /// Запрашивает строку у пользователя с валидацией по списку допустимых значений и значением по умолчанию.
        /// </summary>
        /// <param name="message">Сообщение для вывода перед запросом ввода.</param>
        /// <param name="allowedValues">Массив допустимых строковых значений.</param>
        /// <param name="defaultValue">Значение по умолчанию при нажатии Enter (должно быть в <paramref name="allowedValues"/>).</param>
        /// <returns>Введенная и проверенная строка или <paramref name="defaultValue"/> при пустом вводе.</returns>
        static public string InputStringWithValidation(string message, string? defaultValue = null, string allowedChars = defaultAllowedChars)
        {
            if (!string.IsNullOrEmpty(defaultValue))
            {
                foreach (char c in defaultValue)
                {
                    if (!allowedChars.Contains(c))
                        throw new ArgumentException($"Значение по умолчанию '{defaultValue}' содержит недопустимый символ '{c}'");
                }
            }

            string input;
            do
            {
                Console.Write(message);
                if (!string.IsNullOrEmpty(defaultValue))
                {
                    Console.Write($" (Enter={defaultValue})");
                }
                Console.Write(": ");

                input = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(input))
                {
                    if (!string.IsNullOrEmpty(defaultValue))
                    {
                        var tmp = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Использовано значение по умолчанию: {defaultValue}");
                        Console.ForegroundColor = tmp;
                        return defaultValue;
                    }
                }
                else
                {
                    input = input.Trim();
                }

                bool ok = true;
                foreach (char c in input)
                {
                    if (!allowedChars.Contains(c))
                    {
                        ok = false;
                        break;
                    }
                }

                if (!ok)
                {
                    var tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nРазрешённые символы: {defaultValue}");
                    Console.WriteLine("Повторите ввод\n");
                    Console.ForegroundColor = tmp;
                } 
                else
                {
                    return input;
                }
            }
            while (true);

            //throw new InvalidOperationException("Не должно дойти сюда");
        }


        static string valid = @"^[a-z]";

        static public string Valid(string message, string? stringDefault = null, string allowedChars = defaultAllowedChars)
        {
            Regex regex = new Regex(valid, RegexOptions.IgnoreCase);
            return "";
        }
    }
}