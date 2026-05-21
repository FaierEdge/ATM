// ===== TASK LIST =====
// 4. Сделать подтвержение выхода из программы с возможностью отменить действие
// 6. Сделать админ панель, где можно будет удалить все операции, изменить баланс и т.д. (по желанию) - пароль для входа 1487, Очистка всех операций, изменение баланса
// 7. Добавить переменную окружения Account

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



                    ErrorCheck();
                    //Console.ForegroundColor = ConsoleColor.Red;
                    //Console.Write("Ошибка! ");
                    //Console.ForegroundColor = ConsoleColor.White;
                    //Console.WriteLine("Неверный ввод.");
                    //Console.WriteLine();
                    //Console.Write("Нажмите Enter, чтобы начать сначала...");
                    //Console.ReadKey();



                    continue;
				}
				if (ActionChoice < 0 || ActionChoice > 4)
				{
                    //Console.ForegroundColor = ConsoleColor.Red;
                    //Console.Write("Ошибка! ");
                    //Console.ForegroundColor = ConsoleColor.White;
                    //Console.WriteLine("Неверный ввод.");
                    //Console.WriteLine();
                    //Console.Write("Нажмите Enter, чтобы начать сначала...");
                    //Console.ReadKey();
                    ErrorCheck();
                }
				
				Console.Clear();
				if (ActionChoice == 1) GetCash();
				else if (ActionChoice == 2) TopUp();
				else if (ActionChoice == 3) OperationHistory();
				else if (ActionChoice == 4) TransferBetweenAccouts();
				else if (ActionChoice == 0) return;
			}
		}

		static void ErrorCheck()
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Ошибка! ");
			Console.ForegroundColor = ConsoleColor.White;

            // Метод Main
            if (!int.TryParse(Console.ReadLine(), out ActionChoice)) Console.WriteLine("Неверный ввод.");
            else if (ActionChoice < 0 || ActionChoice > 4) Console.WriteLine("Неверный ввод.");

            // Метод GetCash
            else if (!decimal.TryParse(Console.ReadLine(), out CashSum)) Console.WriteLine("Неверный ввод.");
            else if (CashSum < 0) Console.WriteLine("Вы не можете снять отрицательную сумму.");
			else if ((CashSum % 100 != 0) && (CashSum > 0)) Console.WriteLine("Сумма должна быть кратна 100.");
			else if (CashSum > Balances[AccountChoice]) Console.WriteLine("Недостаточно средств.");

			// Метод TopUp
			else if (TopUpSum < 0) Console.WriteLine("Вы не можете пополнить счет на сумму меньше чем 0 рублей.");

			// Метод TransferBetweenAccouts
			else if (TransferAccount == AccountChoice) Console.WriteLine("Вы не можете перевести средства на тот же счет.");
			else if (TransferAccount < 0 || TransferAccount > 2) Console.WriteLine("Такого счета не существует.");
            else if (TransferSum < 0) Console.WriteLine("Вы не можете перевести отрицательную сумму.");
            else if (TransferSum > Balances[AccountChoice]) Console.WriteLine("Вы не можете перевести больше чем у вас есть.");
            
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
			if ((!decimal.TryParse(Console.ReadLine(), out CashSum)) || (CashSum < 0) || (CashSum % 100 != 0) || (CashSum > Balances[AccountChoice]))
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
			Console.Write("Ваш выбор: ");

			// Проверка валидности значений
			if (!int.TryParse(Console.ReadLine(), out TransferAccount) || (!decimal.TryParse(Console.ReadLine(), out TransferSum)))
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Ошибка! ");
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("Неверный ввод.");
				Console.Write("Нажмите Enter, чтобы начать сначала...");
				Console.ReadKey();
				return;
			}
			if ((TransferAccount == AccountChoice) || (TransferAccount < 0) || (TransferAccount > 2) || (TransferSum < 0) || (TransferSum > Balances[AccountChoice]))
			{
                ErrorCheck();
                return;
            }
			Console.Write("Введите сумму для перевода: ");

			

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