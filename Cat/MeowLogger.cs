using System.Text;

namespace Laboratory6.Cat
{
    /// <summary>
    /// Предоставляет методы для логирования количества мяуканий сущностей, реализующих <see cref="IMeowable"/>, в текстовый файл.
    /// </summary>
    internal static class MeowLogger
    {
        // Имя файла, в который сохраняется статистика мяуканий.
        private static readonly string ConfigFileName = "meow.log";

        // Полный путь к файлу со статистикой мяуканий в каталоге текущего домена приложения.
        private static readonly string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName);

        /// <summary>
        /// Регистрирует факт мяуканий указанной сущности заданное количество раз и обновляет статистику в файле.
        /// </summary>
        /// <param name="meower">Сущность, которая издаёт мяуканье, реализующая интерфейс <see cref="IMeowable"/>.</param>
        /// <param name="times">Количество мяуканий, которое необходимо добавить в статистику. По умолчанию 1.</param>
        /// <exception cref="ArgumentNullException">
        /// Возвращает, если параметр <paramref name="meower"/> имеет значение <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Возвращает, если значение параметра <paramref name="times"/> меньше 1.</exception>
        public static void LogMeow(IMeowable meower, int times = 1)
        {
            if (meower is null)
            {
                throw new ArgumentNullException(nameof(meower));
            }

            if (times < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(times), times, "Количество мяуканий должно быть не меньше 1.");
            }

            string type = meower.GetType().Name;
            string name = GetEntityName(meower);
            string key = $"{type}|{name}";

            var stats = ReadStatistics();
            if (stats.ContainsKey(key))
            {
                stats[key] += times;
            }
            else
            {
                stats[key] = times;
            }

            SaveStatistics(stats);
        }

        /// <summary>
        /// Сохраняет переданную статистику мяуканий в файл в виде строк формата «Тип|Имя;Количество».
        /// </summary>
        /// <param name="stats">Словарь статистики мяуканий, где ключ — строка «Тип|Имя», а значение — общее количество мяуканий.</param>
        private static void SaveStatistics(Dictionary<string, int> stats)
        {
            var lines = new List<string>();

            foreach (var kv in stats)
            {
                string line = kv.Key + ";" + kv.Value;
                lines.Add(line);
            }

            File.WriteAllLines(LogFilePath, lines, Encoding.UTF8);
        }

        /// <summary>
        /// Возвращает отображаемое имя сущности, реализующей <see cref="IMeowable"/>, на основе её строкового представления.
        /// </summary>
        /// <param name="meower">Сущность, для которой требуется получить имя.</param>
        /// <returns>
        /// Строка с именем сущности. Если <see cref="object.ToString"/> возвращает строку вида «Тип: Имя»,
        /// то возвращается часть после двоеточия; иначе возвращается результат <see cref="object.ToString"/> или имя типа.
        /// </returns>
        private static string GetEntityName(IMeowable meower)
        {
            var text = meower.ToString() ?? meower.GetType().Name;
            int idx = text.IndexOf(": ");
            return idx >= 0 ? text[(idx + 2)..] : text;
        }

        /// <summary>
        /// Считывает статистику мяуканий из файла и формирует словарь с суммарными значениями.
        /// </summary>
        /// <returns>
        /// Словарь, где ключ — строка формата «Тип|Имя», а значение — суммарное количество мяуканий.
        /// Если файл статистики отсутствует или содержит некорректные строки, такие записи пропускаются.
        /// </returns>
        public static Dictionary<string, int> ReadStatistics()
        {
            var result = new Dictionary<string, int>();

            if (!File.Exists(LogFilePath))
            {
                return result;
            }

            foreach (var line in File.ReadAllLines(LogFilePath, Encoding.UTF8))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var parts = line.Split(';');

                if (parts.Length != 2)
                {
                    continue;
                }

                string key = parts[0];

                if (!int.TryParse(parts[1], out int count))
                {
                    continue;
                }

                if (result.ContainsKey(key))
                {
                    result[key] += count;
                }
                else
                {
                    result[key] = count;
                }
            }

            return result;
        }

        /// <summary>
        /// Выводит в консоль полную статистику мяуканий для всех зарегистрированных сущностей.
        /// </summary>
        public static void PrintAllStatistics()
        {
            var stats = ReadStatistics();

            if (stats.Count == 0)
            {
                Console.WriteLine("Статистика мяуканий отсутствует.");
                return;
            }

            Console.WriteLine("Статистика мяуканий:");

            foreach (var pair in stats)
            {
                string key = pair.Key;
                string[] parts = key.Split('|');

                if (parts.Length == 2)
                {
                    string type = parts[0];
                    string name = parts[1];
                    Console.WriteLine(type + " | " + name + ": " + pair.Value);
                }
                else
                {
                    Console.WriteLine(key + ": " + pair.Value);
                }
            }
        }

        /// <summary>
        /// Выводит в консоль статистику мяуканий для указанной сущности.
        /// </summary>
        /// <param name="meower">Сущность, для которой требуется показать статистику мяуканий.</param>
        public static void PrintStatisticsFor(IMeowable meower)
        {
            if (meower is null)
            {
                Console.WriteLine("Сущность не задана.");
                return;
            }

            string type = meower.GetType().Name;
            string name = GetEntityName(meower);
            string key = type + "|" + name;

            var stats = ReadStatistics();

            int count;
            if (stats.TryGetValue(key, out count))
            {
                Console.WriteLine(type + " | " + name + ": " + count);
            }
            else
            {
                Console.WriteLine("Для \"" + type + " " + name + "\" статистика не найдена.");
            }
        }

        /// <summary>
        /// Выводит в консоль статистику мяуканий для всех сущностей с указанным именем.
        /// </summary>
        /// <param name="name">Имя сущности, для которой необходимо показать статистику.</param>
        public static void PrintStatisticsFor(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Имя сущности не задано.");
                return;
            }

            var stats = ReadStatistics();
            bool found = false;

            foreach (var pair in stats)
            {
                string key = pair.Key;
                string[] parts = key.Split('|');

                if (parts.Length != 2)
                    continue;

                string type = parts[0];
                string entityName = parts[1];

                if (string.Equals(entityName, name, StringComparison.Ordinal))
                {
                    Console.WriteLine(type + " | " + entityName + ": " + pair.Value);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Для \"" + name + "\" статистика не найдена.");
            }
        }

        /// <summary>
        /// Очищает статистику мяуканий, перезаписывая файл пустым содержимым.
        /// </summary>
        public static void ClearStatistics()
        {
            if (File.Exists(LogFilePath))
            {
                File.WriteAllText(LogFilePath, string.Empty, Encoding.UTF8);
            }
        }
    }
}
