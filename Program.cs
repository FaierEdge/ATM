namespace Банкомат
{
	internal class Program
	{
		// Обозначение переменных
		static int ActionChoice = -1;
		static int AccountChoice = new Random().Next(0, 2);
        static string[] AccountNames = { "Иван Петров", "Мария Сидорова", "Алексей Козлов" };
		static decimal[] Balances = { 15000m, 8500m, 32000m };
		static string[] History = new string[256];
		
		static void Main(string[] args)
		{
            // Настройки окна
            Console.Title = "МОД СБЕРБАНК МНОГО ДЕНЕГ";
            Console.ForegroundColor = ConsoleColor.White;

            // Начало программы
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

            while (ActionChoice != 0)
            {
                // Проверка валидности значения
                try
                {
                    ActionChoice = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Ошибка! ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(ex.Message);
                    Console.Write("Нажмите Enter, чтобы начать сначала...");
                    Console.ReadKey();
                    //return;
                }
                //ErrorCheck();

                Console.Clear();
                switch (ActionChoice)
                {
                    case 1:
                        //GetCash(AccountChoice, Balances, History);
                        break;
                    case 2:
                        //TopUp(AccountChoice, Balances, History);
                        break;
                    case 3:
                        //OperationHistory(History);
                        break;
                    case 4:
                        //TransferBetweenAccouts(AccountNames, AccountChoice, Balances, History);
                        break;
                    case 0:
                        //Тут сделать подтверждение выхода из программы с возможностью отменить действие
                        Environment.Exit(0);
                        break;

                }

                //goto 
            }




        }


        static void ErrorCheck()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Ошибка! ");
            Console.ForegroundColor = ConsoleColor.White;




            // Метод Main
            if (ActionChoice < 0 || ActionChoice > 4) Console.WriteLine("Неверный ввод");



            




            Console.WriteLine();
            Console.Write("Нажмите Enter, чтобы начать сначала...");
            Console.ReadKey();
            Console.Clear();



        }




    }
}