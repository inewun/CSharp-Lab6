using Laboratory6;
using Laboratory6.Cat;
using Laboratory6.Fraction;

start:
var defaultColor = Console.ForegroundColor;
Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.WriteLine("Лабораторная работа 6.");
Console.WriteLine("1 - Кошки\n2 - Дроби\n0 - Выход");
Console.ForegroundColor = defaultColor;
int choice = Checker.InputIntegerWithValidation("Выбор", 0, 2); ;

switch (choice)
{
    case 0:
        Console.WriteLine("Спасибо!");
        return;

    case 1:
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nЗадание Кот.");
        Console.ForegroundColor = defaultColor;

        // Имя для кота
        string catName = Checker.InputStringWithValidation("Имя для кота", "Барсик");
        Cat cat = new Cat(catName);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Создан {cat}!");
        Console.ForegroundColor = defaultColor;
        Console.WriteLine();

        // Мяуканье
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Вызов cat.Meow(): ");
        Console.ForegroundColor = defaultColor;
        cat.Meow();
        Console.WriteLine();
        int meowsCount = Checker.InputIntegerWithValidation("Сколько раз мяукнуть?", 1, 100, 3);
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"Вызов cat.Meow({meowsCount}): ");
        Console.ForegroundColor = defaultColor;
        cat.Meow(meowsCount);

        // Имя для собаки
        Console.WriteLine();
        Console.WriteLine("Можно проверить на повторение имён разных сущностей при вызове мяуканья");
        string dogName = Checker.InputStringWithValidation("Имя для собаки", "Барсик");
        Dog dog = new Dog(dogName);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Создана {dog}!");
        Console.ForegroundColor = defaultColor;

        // Интерфейс мяуканья
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Интерфейс мяуканья");
        Console.ForegroundColor = defaultColor;

        int groupSize = Checker.InputIntegerWithValidation("Сколько всего мяукающих?", 1, 10, 3);

  
        var meowers = new List<IMeowable>();

        // Первый кот и собака всегда в списке
        meowers.Add(cat);
        meowers.Add(dog);

        // При желании добавить ещё котов
        Console.WriteLine();
        string moreCats = Checker.InputStringWithValidation(
            "Введите имена дополнительных котов через запятую (или просто Enter, чтобы пропустить)",
            ""
        );

        foreach (string name in moreCats.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            string trimmed = name.Trim();
            if (!string.IsNullOrWhiteSpace(trimmed))
            {
                meowers.Add(new Cat(trimmed));
            }
        }

        while (meowers.Count < groupSize)
        {
            meowers.Add(new Cat("Котик"));
        }

        // Вызываем метод, принимающий всех мяукающих
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Вызов Sounder.Meow(meowers):");
        Console.ForegroundColor = defaultColor;

        Sounder.Meow(meowers);
        Console.WriteLine();

        // Статистика
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Статистика по мяуканьям для первого кота по ссылке:");
        Console.ForegroundColor = defaultColor;
        MeowLogger.PrintStatisticsFor(cat);
        Console.WriteLine();

        string statName = Checker.InputStringWithValidation("Поиск по имени в статистике: ", "Барсик");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Вывод количества мяуканий по имени:");
        Console.ForegroundColor = defaultColor;
        MeowLogger.PrintStatisticsFor(statName);
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Статистика всех мяукающих, если закоментировать очищение статистики она будет копиться:");
        Console.ForegroundColor = defaultColor;
        MeowLogger.PrintAllStatistics();
        Console.WriteLine();
        MeowLogger.ClearStatistics();

        goto start;

    case 2:
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nЗадание Дроби.");
        Console.ForegroundColor = defaultColor;

        Console.WriteLine("Создаём дроби f1, f2, f3 и f4:");
        Fraction f1 = new Fraction(1, 3);
        Fraction f2 = new Fraction(2, 3);
        Fraction f3 = new Fraction(-4, 5);
        Fraction f4 = new Fraction(5); // 5/1
        Console.WriteLine($"f1 = {f1}\nf2 = {f2}\nf3 = {f3}\nf4 = {f4}");
        Console.WriteLine();

        Console.WriteLine("Арифметические операции между дробями:");
        Fraction add = f1 + f2;
        Console.WriteLine($"{f1} + {f2} = {add}");

        Fraction sub = f2 - f1;
        Console.WriteLine($"{f2} - {f1} = {sub}");

        Fraction mul = f1 * f2;
        Console.WriteLine($"{f1} * {f2} = {mul}");

        Fraction div = f1 / f2;
        Console.WriteLine($"{f1} / {f2} = {div}");
        Console.WriteLine();

        Console.WriteLine("Операция дроби с целым числом:");
        Fraction addInt = f1 + 2;
        Console.WriteLine($"{f1} + 2 = {addInt}");
        Console.WriteLine();

        Console.WriteLine("Цепочка операций f1.sum(f2).div(f3).minus(5):");
        Fraction chain = f1.Sum(f2).Div(f3).Minus(5);
        Console.WriteLine($"{f1}.sum({f2}).div({f3}).minus(5) = {chain}");
        Console.WriteLine();

        Console.WriteLine("Сравнение дробей (==):");
        Fraction c1 = new Fraction(2, 4);
        Fraction c2 = new Fraction(1, 2);
        Console.WriteLine($"{c1} == {c2} | {(c1 == c2)}");
        Console.WriteLine();

        Console.WriteLine("Клонирование дроби (ICloneable):");
        Fraction original = new Fraction(7, 8);
        Fraction clone = (Fraction)original.Clone();
        Console.WriteLine($"original = {original}, clone = {clone}");
        Console.WriteLine();

        Console.WriteLine("Работа с вещественным значением и изменением полей:");
        Console.WriteLine($"{f3} как double = {f3.ToDouble()}");
        Console.WriteLine($"f3 = {f3}, установим числитель = 1 и знаменатель = 2:");
        f3.SetNumerator(1);
        f3.SetDenominator(2);
        Console.WriteLine($"После set: {f3}, как double = {f3.ToDouble()}");
        Console.WriteLine();

        goto start;

}



