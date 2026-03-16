
using System;
using System.Collections.Generic;
using System.Numerics;

namespace ParallelIdentificationProtocol
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ПАРАЛЛЕЛЬНАЯ СХЕМА ПРОТОКОЛА ИДЕНТИФИКАЦИИ С НУЛЕВОЙ ПЕРЕДАЧЕЙ ДАННЫХ\n");

            // Параметры протокола
            int k = 5;  // Количество ключей
            int t = 5;  // Количество раундов
            int bitLength = 256; // Длина простых чисел в битах

            Console.WriteLine($"Параметры протокола: K = {k}, t = {t}");
            Console.WriteLine($"Вероятность обмана: (1/2)^{k * t} = 1/{Math.Pow(2, k * t)}\n");

            // Генерация ключей
            var keys = GenerateKeys(k, bitLength);

            Console.WriteLine($"\nЗначение n: {keys.n}");
            Console.WriteLine("\nОткрытые ключи (V):");
            for (int i = 0; i < keys.V.Count; i++)
            {
                Console.WriteLine($"  V[{i + 1}] = {keys.V[i]}");
            }

            Console.WriteLine("\nСекретные ключи (S):");
            for (int i = 0; i < keys.S.Count; i++)
            {
                Console.WriteLine($"  S[{i + 1}] = {keys.S[i]}");
            }

            // Успешная идентификация
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ПРОЦЕСС ИДЕНТИФИКАЦИИ (Честная Алиса)");
            Console.WriteLine(new string('=', 60));

            bool result = RunProtocol(keys.n, keys.V, keys.S, t);
            Console.WriteLine($"\nРезультат идентификации: {(result ? "УСПЕШНО" : "ПРОВАЛ")}");

            // Попытка обмана
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ПРОЦЕСС ИДЕНТИФИКАЦИИ (Злоумышленница Лена)");
            Console.WriteLine(new string('=', 60));

            bool cheatResult = RunCheatProtocol(keys.n, keys.V, t);
            Console.WriteLine($"\nРезультат идентификации: {(cheatResult ? "УСПЕШНО (обман удался)" : "ПРОВАЛ (обман раскрыт)")}");
        }

        struct KeyPair
        {
            public BigInteger n;
            public List<BigInteger> V; // Открытые ключи
            public List<BigInteger> S; // Секретные ключи
            public BigInteger p;
            public BigInteger q;
        }
        static KeyPair GenerateKeys(int k, int bitLength)
        {
            Console.WriteLine("\n1. ГЕНЕРАЦИЯ КЛЮЧЕЙ:");

            var keys = new KeyPair();
            keys.V = new List<BigInteger>();
            keys.S = new List<BigInteger>();

            // Генерация двух больших простых чисел
            Console.WriteLine("   Генерация простых чисел p и q...");
            keys.p = GeneratePrime(bitLength);
            keys.q = GeneratePrime(bitLength);
            keys.n = keys.p * keys.q;

            Console.WriteLine($"   p = {keys.p}");
            Console.WriteLine($"   q = {keys.q}");
            Console.WriteLine($"   n = p * q = {keys.n}");

            Random random = new Random();

            for (int i = 0; i < k; i++)
            {
                Console.WriteLine($"\n   Генерация пары ключей {i + 1}/{k}:");

                while (true)
                {
                    // Генерируем случайный кандидат в секретные ключи
                    byte[] bytes = new byte[bitLength / 8];
                    random.NextBytes(bytes);
                    BigInteger sCandidate = new BigInteger(bytes);
                    sCandidate = BigInteger.Abs(sCandidate) % keys.n;

                    if (sCandidate < 2) continue;

                    // Проверяем взаимную простоту с n
                    if (BigInteger.GreatestCommonDivisor(sCandidate, keys.n) != 1)
                        continue;
                    // Вычисляем s^2 mod n
                    BigInteger sSquared = (sCandidate * sCandidate) % keys.n;

// Проверяем обратимость
                    if (BigInteger.GreatestCommonDivisor(sSquared, keys.n) != 1)
                        continue;

                    try
                    {
                        // Вычисляем V = (s^2)^{-1} mod n
                        BigInteger v = ModInverse(sSquared, keys.n);

                        keys.V.Add(v);
                        keys.S.Add(sCandidate);

                        Console.WriteLine($"     S[{i + 1}] = {sCandidate}");
                        Console.WriteLine($"     V[{i + 1}] = {v}");
                        Console.WriteLine($"     Проверка: (S^2 * V) mod n = {(sSquared * v) % keys.n} (должно быть 1)");

                        break;
                    }
                    catch
                    {
                        // Если не удалось найти обратное, пробуем другое число
                        continue;
                    }
                }
            }

            return keys;
        }

        /// Генерация простого числа заданной битовой длины
  
        static BigInteger GeneratePrime(int bitLength)
        {
            Random random = new Random();
            while (true)
            {
                byte[] bytes = new byte[bitLength / 8];
                random.NextBytes(bytes);
                BigInteger candidate = new BigInteger(bytes);
                candidate = BigInteger.Abs(candidate);

                // Устанавливаем старший и младший биты для обеспечения нечетности и нужной длины
                candidate |= BigInteger.One << (bitLength - 1);
                candidate |= BigInteger.One;

                if (IsPrime(candidate, 10))
                    return candidate;
            }
        }
        /// Проверка числа на простоту (тест Миллера-Рабина)
        static bool IsPrime(BigInteger n, int k)
        {
            if (n < 2) return false;
            if (n == 2 || n == 3) return true;
            if (n % 2 == 0) return false;

            // Представляем n-1 в виде d*2^s
            BigInteger d = n - 1;
            int s = 0;
            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            Random random = new Random();
            for (int i = 0; i < k; i++)
            {
                BigInteger a = RandomBigInteger(2, n - 2);
                BigInteger x = BigInteger.ModPow(a, d, n);

                if (x == 1 || x == n - 1);
                    continue;

            bool composite = true;
            for (int r = 0; r < s - 1; r++)
            {
                x = (x * x) % n;
                if (x == n - 1)
                {
                    composite = false;
                    break;
                }
            }

            if (composite)
                return false;
        }

            return true;
        }
        static BigInteger RandomBigInteger(BigInteger min, BigInteger max)
        {
            Random random = new Random();
            byte[] bytes = max.ToByteArray();
            BigInteger result;

            do
            {
                random.NextBytes(bytes);
                bytes[bytes.Length - 1] &= 0x7F; // Устанавливаем знаковый бит в 0
                result = new BigInteger(bytes);
            } while (result < min || result >= max);

            return result;
        }

        /// <summary>
        /// Вычисление обратного элемента по модулю (расширенный алгоритм Евклида)
        /// </summary>
        static BigInteger ModInverse(BigInteger a, BigInteger n)
    {
        BigInteger i = n, v = 0, d = 1;
        while (a > 0)
        {
            BigInteger t = i / a, x = a;
            a = i % x;
            i = x;
            x = d;
            d = v - t * x;
            v = x;
        }
        v %= n;
        if (v < 0) v = (v + n) % n;
        return v;
    }

    /// <summary>
    /// Запуск протокола идентификации (честная сторона)
    /// </summary>
    static bool RunProtocol(BigInteger n, List<BigInteger> V, List<BigInteger> S, int t)
    {
        int k = V.Count;
        Random random = new Random();

        for (int round = 1; round <= t; round++)
        {
            Console.WriteLine($"\n   РАУНД {round}:");

            // Шаг 1: Алиса выбирает случайное r и вычисляет x = r^2 mod n
            BigInteger r = RandomBigInteger(1, n - 1);
            BigInteger x = (r * r) % n;
            Console.WriteLine($"   [Сторона А -> Сторона В] x = {x}");

            // Шаг 2: Егор отправляет случайный битовый вектор
            List<int> b = new List<int>();
            for (int i = 0; i < k; i++)
            {
                b.Add(random.Next(2));
            }
            Console.WriteLine($"   [Сторона В -> Сторона А] b = [{string.Join(", ", b)}]");

            // Шаг 3: Алиса вычисляет y = r * (S1^b1 * S2^b2 * ... * Sk^bk) mod n
            BigInteger product = 1;
            for (int i = 0; i < k; i++)
            {
                if (b[i] == 1)
                {
                    product = (product * S[i]) % n;
                }
            }
            BigInteger y = (r * product) % n;
            Console.WriteLine($"   [Сторона А -> Сторона В] y = {y}");

            // Шаг 4: Егор проверяет x == y^2 * (V1^b1 * V2^b2 * ... * Vk^bk) mod n
            BigInteger productV = 1;
            for (int i = 0; i < k; i++)
            {
                if (b[i] == 1)
                {
                    productV = (productV * V[i]) % n;
                }
            }

            BigInteger ySquared = (y * y) % n;
            BigInteger checkValue = (ySquared * productV) % n;
            bool isValid = (x == checkValue);

            Console.WriteLine($"   [Сторона В] Проверка: x == y^2 * prod(Vi) mod n?");
            Console.WriteLine($"      x = {x}");
            Console.WriteLine($"      y^2 * prod(Vi) mod n = {checkValue}");
            Console.WriteLine($"      Результат: {(isValid ? "ПРИНЯТО" : "ОТКЛОНЕНО")}");

            if (!isValid)
            {
                Console.WriteLine($"\n   *** Идентификация не пройдена на раунде {round} ***");
                return false;
            }
        }

        Console.WriteLine($"\n   *** Идентификация успешно пройдена после {t} раундов ***");
        return true;
    }

    /// <summary>
    /// Запуск протокола с попыткой обмана (Лена не знает секретные ключи)
    /// </summary>
    static bool RunCheatProtocol(BigInteger n, List<BigInteger> V, int t)
    {
        int k = V.Count;
        Random random = new Random();

        for (int round = 1; round <= t; round++)
        {
            Console.WriteLine($"\n   РАУНД {round}:");

            // Стратегия Евы: она пытается угадать битовый вектор Боба заранее
            // и подготовить соответствующий x

            // Лена предполагает, какой битовый вектор пошлет Егор
            List<int> guessedB = new List<int>();
            for (int i = 0; i < k; i++)
            {
                guessedB.Add(random.Next(2));
            }

// Если Лена угадает вектор, она сможет вычислить правильный y
// Для этого она выбирает случайный y и вычисляет x = y^2 * prod(Vi) mod n
                BigInteger y = RandomBigInteger(1, n - 1);
            BigInteger productV = 1;
            for (int i = 0; i < k; i++)
            {
                if (guessedB[i] == 1)
                {
                    productV = (productV * V[i]) % n;
                }
            }

            BigInteger ySquared = (y * y) % n;
            BigInteger x = (ySquared * productV) % n;

            Console.WriteLine($"   [Лена -> Егор] x = {x} (подготовлено для предполагаемого вектора [{string.Join(", ", guessedB)}])");

            // Егор отправляет реальный случайный битовый вектор
            List<int> actualB = new List<int>();
            for (int i = 0; i < k; i++)
            {
                actualB.Add(random.Next(2));
            }
            Console.WriteLine($"   [Егор -> Лена] b = [{string.Join(", ", actualB)}]");

            // Проверяем, угадала ли Лена вектор
            bool guessedCorrectly = true;
            for (int i = 0; i < k; i++)
            {
                if (guessedB[i] != actualB[i])
                {
                    guessedCorrectly = false;
                    break;
                }
            }

            if (guessedCorrectly)
            {
                // Если угадала, она может отправить подготовленный y
                Console.WriteLine($"   [Лена -> Егор] y = {y} (Лена угадала вектор!)");

                // Проверка Егорва
                productV = 1;
                for (int i = 0; i < k; i++)
                {
                    if (actualB[i] == 1)
                    {
                        productV = (productV * V[i]) % n;
                    }
                }

                BigInteger checkValue = (ySquared * productV) % n;
                bool isValid = (x == checkValue);

                Console.WriteLine($"   [Егор] Проверка: {x} == {checkValue} -> {(isValid ? "ПРИНЯТО" : "ОТКЛОНЕНО")}");

                if (!isValid)
                {
                    Console.WriteLine($"\n   *** Обман раскрыт на раунде {round} ***");
                    return false;
                }
            }
            else
            {
                // Если не угадала, она не может ответить правильно
                Console.WriteLine($"   [Лена -> Егор] y = ??? (Лена НЕ угадала вектор и не может ответить)");
                Console.WriteLine($"   [Егор] Проверка: ОТКЛОНЕНО (Лена не прислала ответ)");
                Console.WriteLine($"\n   *** Обман раскрыт на раунде {round} ***");
                return false;
            }
        }

        // Если Лена угадала все t раундов (вероятность (1/2)^(k*t)), обман удался
        Console.WriteLine($"\n   *** ВНИМАНИЕ: Еве удалось обмануть Боба! ***");
        Console.WriteLine($"   Вероятность этого: (1/2)^{k * t} = 1/{Math.Pow(2, k * t)}");
        return true;
    }
}
}