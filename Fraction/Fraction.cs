namespace Laboratory6.Fraction
{
    /// <summary>
    /// Представляет математическую дробь с числителем и знаменателем.
    /// Поддерживает арифметические операции, сравнение и кэширование значения.
    /// </summary>
    public class Fraction : ICloneable, IFraction
    {
        private int _numerator;
        private int _denominator;

        private double? _valueCache;
        private bool _isDirty;

        /// <summary>
        /// Получает или устанавливает числитель дроби.
        /// </summary>
        public int Numerator
        {
            get
            {
                return _numerator;
            }
            set
            {
                _numerator = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Получает или устанавливает знаменатель дроби.
        /// Знаменатель не может быть равен нулю и автоматически нормализуется.
        /// </summary>
        /// <exception cref="ArgumentException">Бросается при попытке установить знаменатель равный нулю.</exception>
        public int Denominator
        {
            get
            {
                return _denominator;
            }
            private set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Знаменатель не может быть нулевым.", nameof(Denominator));
                }

                if (value < 0)
                {
                    _numerator = -_numerator;
                    value = -value;
                }

                _denominator = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Инициализирует новую дробь с заданными числителем и знаменателем.
        /// Автоматически сокращает дробь и нормализует знак.
        /// </summary>
        /// <param name="numerator">Числитель дроби.</param>
        /// <param name="denominator">Знаменатель дроби (не может быть 0).</param>
        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
            NormalizeSign();
            Reduce();
        }

        /// <summary>
        /// Инициализирует дробь из целого числа (числитель = число, знаменатель = 1).
        /// </summary>
        /// <param name="integer">Целое число для создания дроби.</param>
        public Fraction(int integer) : this(integer, 1) { }

        /// <summary>
        /// Нормализует знак дроби: знаменатель всегда положительный.
        /// </summary>
        private void NormalizeSign()
        {
            if (Denominator < 0)
            {
                Denominator = -Denominator;
                Numerator = -Numerator;
            }
        }

        /// <summary>
        /// Сокращает дробь наибольшим общим делителем (НОД).
        /// </summary>
        private void Reduce()
        {
            int gcd = GCD(Numerator, Denominator);
            Numerator /= gcd;
            Denominator /= gcd;
        }

        /// <summary>
        /// Вычисляет наибольший общий делитель двух чисел (алгоритм Евклида).
        /// </summary>
        /// <param name="a">Первое число.</param>
        /// <param name="b">Второе число.</param>
        /// <returns>НОД чисел a и b.</returns>
        private static int GCD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        /// <summary>
        /// Возвращает строковое представление дроби в формате "числитель/знаменатель".
        /// </summary>
        /// <returns>Строка вида "3/4".</returns>
        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        /// <summary>
        /// Складывает две дроби.
        /// </summary>
        /// <param name="a">Первая дробь.</param>
        /// <param name="b">Вторая дробь.</param>
        /// <returns>Результат сложения в сокращенном виде.</returns>
        public static Fraction operator +(Fraction a, Fraction b)
        {
            return new Fraction
            (
                a.Numerator * b.Denominator + b.Numerator * a.Denominator,
                a.Denominator * b.Denominator
            );
        }

        /// <summary>
        /// Вычитает вторую дробь из первой.
        /// </summary>
        /// <param name="a">Первая дробь.</param>
        /// <param name="b">Вычитаемая дробь.</param>
        /// <returns>Результат вычитания в сокращенном виде.</returns>
        public static Fraction operator -(Fraction a, Fraction b)
        {
            return new Fraction
            (
                a.Numerator * b.Denominator - b.Numerator * a.Denominator,
                a.Denominator * b.Denominator
            );
        }

        /// <summary>
        /// Вычитает целое число из дроби.
        /// </summary>
        /// <param name="a">Дробь.</param>
        /// <param name="value">Вычитаемое целое число.</param>
        /// <returns>Результат вычитания в сокращенном виде.</returns>
        public static Fraction operator -(Fraction a, int value)
        {
            return a - new Fraction(value);
        }

        /// <summary>
        /// Умножает две дроби.
        /// </summary>
        /// <param name="a">Первая дробь.</param>
        /// <param name="b">Вторая дробь.</param>
        /// <returns>Результат умножения в сокращенном виде.</returns>
        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction
            (
                a.Numerator * b.Numerator,
                a.Denominator * b.Denominator
            );
        }

        /// <summary>
        /// Делит первую дробь на вторую.
        /// </summary>
        /// <param name="a">Делимая дробь.</param>
        /// <param name="b">Делитель (числитель не может быть 0).</param>
        /// <returns>Результат деления в сокращенном виде.</returns>
        /// <exception cref="DivideByZeroException">Бросается при делении на дробь с нулевым числителем.</exception>
        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b.Numerator == 0)
            {
                throw new DivideByZeroException("Деление на нулевую дробь.");
            }

            return new Fraction
            (
                a.Numerator * b.Denominator,
                a.Denominator * b.Numerator
            );
        }

        /// <summary>
        /// Неявное преобразование целого числа в дробь (знаменатель = 1).
        /// </summary>
        /// <param name="value">Целое число.</param>
        public static implicit operator Fraction(int value)
        {
            return new Fraction(value);
        }

        /// <summary>
        /// Складывает текущую дробь с другой.
        /// </summary>
        /// <param name="other">Складываемая дробь.</param>
        /// <returns>Результат сложения.</returns>
        public Fraction Sum(Fraction other)
        {
            return this + other;
        }

        /// <summary>
        /// Вычитает другую дробь из текущей.
        /// </summary>
        /// <param name="other">Вычитаемая дробь.</param>
        /// <returns>Результат вычитания.</returns>
        public Fraction Minus(Fraction other)
        {
            return this - other;
        }

        /// <summary>
        /// Вычитает целое число из текущей дроби.
        /// </summary>
        /// <param name="value">Вычитаемое целое число.</param>
        /// <returns>Результат вычитания.</returns>
        public Fraction Minus(int value)
        {
            return this - value;
        }

        /// <summary>
        /// Умножает текущую дробь на другую.
        /// </summary>
        /// <param name="other">Умножаемая дробь.</param>
        /// <returns>Результат умножения.</returns>
        public Fraction Mult(Fraction other)
        {
            return this * other;
        }

        /// <summary>
        /// Делит текущую дробь на другую.
        /// </summary>
        /// <param name="other">Делитель.</param>
        /// <returns>Результат деления.</returns>
        public Fraction Div(Fraction other)
        {
            return this / other;
        }

        /// <summary>
        /// Определяет, равны ли две дроби.
        /// </summary>
        /// <param name="obj">Объект для сравнения.</param>
        /// <returns>true, если дроби равны; иначе false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not Fraction other)
            {
                return false;
            }

            return Numerator == other.Numerator && Denominator == other.Denominator;
        }

        /// <summary>
        /// Вычисляет хеш-код дроби.
        /// </summary>
        /// <returns>Хеш-код на основе числителя и знаменателя.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }

        /// <summary>
        /// Сравнивает текущую дробь с другой по значению.
        /// </summary>
        /// <param name="other">Дробь для сравнения.</param>
        /// <returns>0 если равны, 1 если текущая больше, -1 если меньше.</returns>
        public int CompareTo(Fraction other)
        {
            long left = (long)Numerator * other.Denominator;
            long right = (long)other.Numerator * Denominator;
            return left.CompareTo(right);
        }

        /// <summary>
        /// Проверяет равенство двух дробей.
        /// </summary>
        /// <param name="a">Первая дробь.</param>
        /// <param name="b">Вторая дробь.</param>
        /// <returns>true, если дроби равны.</returns>
        public static bool operator ==(Fraction a, Fraction b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        /// <summary>
        /// Проверяет неравенство двух дробей.
        /// </summary>
        /// <param name="a">Первая дробь.</param>
        /// <param name="b">Вторая дробь.</param>
        /// <returns>true, если дроби не равны.</returns>
        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Создает точную копию текущей дроби.
        /// </summary>
        /// <returns>Новая копия дроби.</returns>
        public object Clone()
        {
            return new Fraction(Numerator, Denominator);
        }

        /// <summary>
        /// Преобразует дробь в double с использованием кэширования.
        /// </summary>
        /// <returns>Дробное значение типа double.</returns>
        public double ToDouble()
        {
            if (_isDirty || !_valueCache.HasValue)
            {
                _valueCache = (double)_numerator / _denominator;
                _isDirty = false;
            }
            return _valueCache.Value;
        }

        /// <summary>
        /// Устанавливает новый числитель и сокращает дробь.
        /// </summary>
        /// <param name="value">Новый числитель.</param>
        public void SetNumerator(int value)
        {
            Numerator = value;
            Reduce();
        }

        /// <summary>
        /// Устанавливает новый знаменатель, нормализует знак и сокращает дробь.
        /// </summary>
        /// <param name="value">Новый знаменатель (не может быть 0).</param>
        public void SetDenominator(int value)
        {
            Denominator = value;
            NormalizeSign();
            Reduce();
        }
    }
}
