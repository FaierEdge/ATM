// ===== TASK LIST =====
// 1. Сделать подтвержение выхода из программы с возможностью отменить действие
// 2. Сделать админ панель, где можно будет удалить все операции, изменить баланс и т.д. (по желанию) - пароль для входа 1487, Очистка всех операций, изменение баланса

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
		static int TransferAccount; // TransferBetweenAccouts
		static decimal TransferSum; // TransferBetweenAccouts

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
					ErrorShow("Неверный ввод.");
					continue;
				}
				if (ActionChoice < 0 || ActionChoice > 4) ErrorShow("Неверный ввод.");
				
				Console.Clear();
				if (ActionChoice == 1) GetCash();
				else if (ActionChoice == 2) TopUp();
				else if (ActionChoice == 3) OperationHistory();
				else if (ActionChoice == 4) TransferBetweenAccouts();
				else if (ActionChoice == 0) return;
			}
		}
		
		static void ErrorShow(string ErrorMessage)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Ошибка! ");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(ErrorMessage);
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
				ErrorShow("Неверный ввод.");
				return;
			}
			if (CashSum < 0)
			{
				ErrorShow("Вы не можете снять отрицательную сумму.");
			}
			if (CashSum % 100 != 0)
			{
				ErrorShow("Сумма должна быть кратна 100.");
				return;
			}
			if (CashSum > Balances[AccountChoice])
			{
                ErrorShow("Недостаточно средств.");
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
                ErrorShow("Неверный ввод.");
                return;
			}
			if (TopUpSum < 0)
			{
				ErrorShow("Вы не можете пополнить счет на сумму меньше чем 0 рублей.");
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

		static void OperationHistory()
		{
			Console.WriteLine("===== ИСТОРИЯ ОПЕРАЦИЙ =====");
			if (History[0] == null)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Операций пока нет.");
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine();
				Console.Write("Нажмите Enter для продолжения...");
				Console.ReadKey();
				return;
			}
			else
			{
				for (int i = 0; i < 256; i++)
				{
					if (History[i] == null) break;
					Console.WriteLine($"{i + 1}. {History[i]}");
				}
			}
			Console.WriteLine();
			Console.Write("Нажмите Enter для продолжения...");
			Console.ReadKey();
		}

		static void TransferBetweenAccouts()
		{
			Console.WriteLine("===== ПЕРЕВОД МЕЖДУ СЧЕТАМИ =====");
			Console.WriteLine($"Баланс: {Balances[AccountChoice]}");
			Console.WriteLine();
			Console.WriteLine("Выберите счет для перевода:");

			// Показ всех счетов, кроме текущего
			for (int i = 0; i < 3; i++)
			{
				if (i != AccountChoice) Console.WriteLine($"{i + 1}. {AccountNames[i]}");
			}
			Console.WriteLine();

            // Ввод значений и проверка их валидности
            Console.Write("Ваш выбор: ");
            if (!int.TryParse(Console.ReadLine(), out TransferAccount))
			{
				ErrorShow("Неверный ввод.");
				return;
			}
			TransferAccount--;
            if (TransferAccount == AccountChoice)
			{
				ErrorShow("Вы не можете перевести средства на тот же счет.");
				return;
			}
            if (TransferAccount < 0 || TransferAccount > 2)
            {
                ErrorShow("Такого счета не существует.");
                return;
            }
			Console.Write("Введите сумму для перевода: ");
            if (!decimal.TryParse(Console.ReadLine(), out TransferSum))
            {
                ErrorShow("Неверный ввод.");
                return;
            }
            if (TransferSum < 0)
			{
				ErrorShow("Вы не можете перевести отрицательную сумму.");
				return;
			}
            if (TransferSum > Balances[AccountChoice])
            {
                ErrorShow("Недостаточно средств.");
                return;
            }
            // Успешный перевод между счетами
            Balances[AccountChoice] -= TransferSum;
			Balances[TransferAccount] += TransferSum;
			Console.Clear();
			Console.WriteLine("===== ПЕРЕВОД МЕЖДУ СЧЕТАМИ =====");
			Console.WriteLine($"Баланс: {Balances[AccountChoice]}");
			Console.WriteLine();
			Console.WriteLine("Выберите счет для перевода:");
			for (int i = 0; i < 3; i++)
			{
				if (i != AccountChoice) Console.WriteLine($"{i + 1}. {AccountNames[i]}");
			}
			Console.WriteLine();
			Console.WriteLine($"Ваш выбор: {TransferAccount + 1}");
			Console.WriteLine($"Введите сумму для перевода: {TransferSum}");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Вы успешно перевели {TransferSum} на счет \"{AccountNames[TransferAccount]}\".");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine();
			Console.Write("Нажмите Enter для продолжения...");
			Console.ReadKey();

			// Запись операции в историю
			for (int i = 0; i < 256; i++)
			{
				if (History[i] == null)
				{
					History[i] = $"[{DateTime.Now.ToLongTimeString()}] Перевод -{TransferSum},00 Р. --> {AccountNames[TransferAccount]} (остаток: {Balances[AccountChoice]})";
					break;
				}
			}
		}
	}
}