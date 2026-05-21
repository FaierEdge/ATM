namespace Банкомат
{
	internal class Program
	{
		// Глобальные переменные
		static int ActionChoice = -1;
		static int AccountChoice = new Random().Next(0, 2);
		static string[] AccountNames = { "Иван Петров", "Мария Сидорова", "Алексей Козлов" };
		static decimal[] Balances = { 15000m, 8500m, 32000m };
		static string[] History = new string[256];

		// Переменные методов
		static decimal CashSum;		// GetCash
		static decimal TopUpSum;	// TopUP

		static void Main(string[] args)
		{
			// Настройки окна
			Console.Title = "МОД СБЕРБАНК МНОГО ДЕНЕГ";
			Console.ForegroundColor = ConsoleColor.White;

			// Основной цикл программы
			while (ActionChoice != 0)
			{
				// Начало программы
				Console.Clear();
				Console.WriteLine($"===== Здавствуйте, {AccountNames[AccountChoice]} ===== ");
				Console.WriteLine($"Баланс: {Balances[AccountChoice]}");
				Console.WriteLine();
				Console.WriteLine("Выберите действие:");
				Console.WriteLine("1. Снять наличные");
				Console.WriteLine("2. Пополнить счет");
				Console.WriteLine("3. История операций");
				Console.WriteLine("4. Перевод между счетами");
				Console.WriteLine("0. Выход");
				Console.WriteLine();
				Console.WriteLine("====================");
				Console.Write("Ваш выбор: ");

				// Проверка валидности значения
				if (!int.TryParse(Console.ReadLine(), out ActionChoice))
				{
					ActionChoice = -1;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write("Ошибка! ");
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine("Неверный ввод.");
					Console.Write("Нажмите Enter, чтобы начать сначала...");
					Console.ReadKey();
					continue;
				}
				if (ActionChoice < 0 || ActionChoice > 4) ErrorCheck();
				
				Console.Clear();
				if (ActionChoice == 1) GetCash();
				else if (ActionChoice == 2) TopUp();
				else if (ActionChoice == 3) Console.WriteLine("InDev"); // OperationHistory();
				else if (ActionChoice == 4) Console.WriteLine("InDev"); // TransferBetweenAccouts();
				else if (ActionChoice == 0) return;
				Console.ReadKey();
			}
		}

		static void ErrorCheck()
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Ошибка! ");
			Console.ForegroundColor = ConsoleColor.White;
			
			// Метод Main
			if (ActionChoice < 0 || ActionChoice > 4) Console.WriteLine("Неверный ввод.");

			// Метод GetCash
			if (CashSum < 0) Console.WriteLine("Вы не можете снять отрицательную сумму.");
			if ((CashSum % 100 != 0) && (CashSum > 0)) Console.WriteLine("Сумма должна быть кратна 100.");
			if (CashSum > Balances[AccountChoice]) Console.WriteLine("Недостаточно средств.");

            // Метод TopUp
            if (TopUpSum < 0) Console.WriteLine("Вы не можете пополнить счет на сумму меньше чем 0 рублей.");

            Console.WriteLine();
			Console.Write("Нажмите Enter, чтобы начать сначала...");
			Console.ReadKey();
		}

		static void GetCash()
		{
			Console.WriteLine("===== СНЯТИЕ НАЛИЧНЫХ =====");
			Console.WriteLine($"Баланс: {Balances[AccountChoice]}");
			Console.WriteLine();
			Console.Write("Введите сумму для снятия (сумма должна быть кратна 100): ");

			// Проверка валидности значения
			if (!decimal.TryParse(Console.ReadLine(), out CashSum))
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Ошибка! ");
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("Неверный ввод.");
				Console.Write("Нажмите Enter, чтобы начать сначала...");
				Console.ReadKey();
				return;
			}
			else if ((CashSum < 0) || (CashSum % 100 != 0) || (CashSum > Balances[AccountChoice]))
			{
				ErrorCheck();
				return;
			}

			// Успешное снятие наличных
			Balances[AccountChoice] -= CashSum;
			Console.Clear();
			Console.WriteLine("===== СНЯТИЕ НАЛИЧНЫХ =====");
			Console.WriteLine($"Баланс: {Balances[AccountChoice]}");
			Console.WriteLine();
			Console.WriteLine($"Введите сумму для снятия (сумма должна быть кратна 100): {CashSum}");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Вы успешно сняли {CashSum} рублей.");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine();
			Console.Write("Нажмите Enter для продолжения...");
			Console.ReadKey();

			// Запись операции в историю
			for (int i = 0; i < 256; i++)
			{
				if (History[i] == null)
				{
					History[i] = $"[{DateTime.Now.ToLongTimeString()}] Снятие -{CashSum},00 Р. (остаток: {Balances[AccountChoice]})";
					break;
				}
			}
		}

		static void TopUp()
		{
			Console.WriteLine("===== ПОПОЛНЕНИЕ СЧЕТА =====");
			Console.WriteLine($"Баланс: {Balances[AccountChoice]}");
			Console.WriteLine();
			Console.Write("Введите сумму для пополнения: ");

            // Проверка валидности значения
            if (!decimal.TryParse(Console.ReadLine(), out TopUpSum))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Ошибка! ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Неверный ввод.");
                Console.Write("Нажмите Enter, чтобы начать сначала...");
                Console.ReadKey();
                return;
            }
			if (TopUpSum < 0)
			{
                ErrorCheck();
                return;
            }

			// Успешное пополнение счета
			Balances[AccountChoice] += TopUpSum;
			Console.Clear();
			Console.WriteLine("===== ПОПОЛНЕНИЕ СЧЕТА =====");
			Console.WriteLine($"Баланс: {Balances[AccountChoice]}");
			Console.WriteLine();
			Console.WriteLine($"Введите сумму для пополнения: {TopUpSum}");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Вы успешно пополнили счет на {TopUpSum} рублей.");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine();
			Console.Write("Нажмите Enter для продолжения...");
			Console.ReadKey();

			// Запись операции в историю
			for (int i = 0; i < 256; i++)
			{
				if (History[i] == null)
				{
					History[i] = $"[{DateTime.Now.ToLongTimeString()}] Пополнение +{TopUpSum},00 Р. (остаток: {Balances[AccountChoice]})";
					break;
				}
			}
		}





	}
}