using System;
using System.Linq;

namespace TelegramBotConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Инициализация переменных
            string userName = string.Empty;
            bool isNameEntered = false;
            bool waitingForName = false;
            bool isRunning = true;
            List<string> tasks = new List<string>();
            #endregion

            #region Приветствие и список команд
            Console.WriteLine("Добро пожаловать в бот!\n");
            Console.WriteLine("Доступные команды:");
            Console.WriteLine("/start - начать работу и ввести имя");
            Console.WriteLine("/help - показать справку");
            Console.WriteLine("/info - информация о программе");
            Console.WriteLine("/echo [текст] - повторить введенный текст");
            Console.WriteLine("/addtask - добавить задачу");
            Console.WriteLine("/showtasks - показать задачи");
            Console.WriteLine("/removetask - удалить задачу");
            Console.WriteLine("/exit - выход из программы");
            Console.WriteLine();
            #endregion

            while (isRunning)
            {
                #region Ввод команды
                if (!waitingForName)
                {
                    Console.Write("Введите команду: ");
                }

                string input = Console.ReadLine();
                #endregion

                #region Обработка ввода имени
                if (waitingForName)
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("\nИмя не может быть пустым. Попробуйте снова:");
                        continue;
                    }

                    if (input.StartsWith("/"))
                    {
                        Console.WriteLine($"\nОшибка: '{input}' - это команда, а не имя. Пожалуйста, введите ваше имя (без /):");
                        continue;
                    }

                    if (input.Length < 2)
                    {
                        Console.WriteLine("\nОшибка: имя должно содержать хотя бы 2 символа. Попробуйте снова:");
                        continue;
                    }

                    if (!input.All(c => char.IsLetter(c)))
                    {
                        Console.WriteLine("\nОшибка: имя должно содержать только буквы. Попробуйте снова:");
                        continue;
                    }

                    userName = input;
                    isNameEntered = true;
                    waitingForName = false;
                    Console.WriteLine($"\nПриятно познакомиться, {userName}!");
                    Console.WriteLine("Подсказка: теперь доступна команда /echo [текст]\n");
                    continue;
                }
                #endregion

                #region Проверка на пустой ввод
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Пожалуйста, введите команду.\n");
                    continue;
                }
                #endregion

                #region Обработка /echo
                if (input.StartsWith("/echo "))
                {
                    if (isNameEntered)
                    {
                        string echoText = input.Substring(6);
                        if (!string.IsNullOrWhiteSpace(echoText))
                        {
                            Console.WriteLine($"{userName}, вы написали: \"{echoText}\"\n");
                        }
                        else
                        {
                            Console.WriteLine($"{userName}, вы не ввели текст. Используйте: /echo текст\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Сначала введите /start, чтобы начать работу.\n");
                    }
                    continue;
                }
                #endregion

                #region Обработка команд
                switch (input)
                {
                    #region Команда /start
                    case "/start":
                        if (!isNameEntered)
                        {
                            waitingForName = true;
                            Console.WriteLine();
                            Console.Write("Введите ваше имя: ");
                        }
                        else
                        {
                            Console.WriteLine($"{userName}, вы уже ввели имя. Используйте другие команды.\n");
                        }
                        break;
                    #endregion

                    #region Команда /help
                    case "/help":
                        Console.WriteLine();
                        Console.WriteLine("Справочная информация");
                        Console.WriteLine("/start - начать работу и ввести имя");
                        Console.WriteLine("/help - показать эту справку");
                        Console.WriteLine("/info - информация о программе");
                        Console.WriteLine("/echo [текст] - повторить введенный текст (доступна после ввода имени)");
                        Console.WriteLine("/addtask - добавить новую задачу");
                        Console.WriteLine("/showtasks - показать список всех задач");
                        Console.WriteLine("/removetask - удалить задачу по номеру");
                        Console.WriteLine("/exit - выход из программы");
                        PrintPersonalizedMessage(isNameEntered, userName, "это вся доступная информация на данный момент.");
                        break;
                    #endregion

                    #region Команда /info
                    case "/info":
                        Console.WriteLine();
                        Console.WriteLine("Информация о программе");
                        Console.WriteLine("Версия: 2.0.0");
                        Console.WriteLine("Дата создания: 26.02.2026");
                        Console.WriteLine("Автор: Dorofeeva Daria");
                        PrintPersonalizedMessage(isNameEntered, userName, "спасибо, что пользуетесь ботом!");
                        break;
                    #endregion

                    #region Команда /echo
                    case "/echo":
                        Console.WriteLine();
                        if (isNameEntered)
                        {
                            Console.WriteLine($"{userName}, вы использовали команду /echo без текста. Используйте: /echo [текст]\n");
                        }
                        else
                        {
                            Console.WriteLine("Сначала введите /start, чтобы начать работу.\n");
                        }
                        break;
                    #endregion

                    #region Команда /addtask
                    case "/addtask":
                        Console.WriteLine();
                        if (isNameEntered)
                        {
                            Console.Write("Пожалуйста, введите описание задачи: ");
                            string taskDescription = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(taskDescription))
                            {
                                tasks.Add(taskDescription);
                                Console.WriteLine($"Задача \"{taskDescription}\" добавлена.\n");
                            }
                            else
                            {
                                Console.WriteLine("Описание задачи не может быть пустым.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Сначала введите /start, чтобы начать работу.\n");
                        }
                        break;
                    #endregion

                    #region Команда /showtasks
                    case "/showtasks":
                        Console.WriteLine();
                        if (isNameEntered)
                        {
                            if (tasks.Count == 0)
                            {
                                Console.WriteLine("Список задач пуст.\n");
                            }
                            else
                            {
                                Console.WriteLine("Ваш список задач:");
                                for (int i = 0; i < tasks.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {tasks[i]}");
                                }
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Сначала введите /start, чтобы начать работу.\n");
                        }
                        break;
                    #endregion

                    #region Команда /removetask
                    case "/removetask":
                        Console.WriteLine();
                        if (isNameEntered)
                        {
                            if (tasks.Count == 0)
                            {
                                Console.WriteLine("Список задач пуст. Нечего удалять.\n");
                                break;
                            }

                            Console.WriteLine("Вот ваш список задач:");
                            for (int i = 0; i < tasks.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {tasks[i]}");
                            }

                            Console.Write("Введите номер задачи для удаления: ");
                            string numberInput = Console.ReadLine();

                            if (int.TryParse(numberInput, out int taskNumber))
                            {
                                if (taskNumber >= 1 && taskNumber <= tasks.Count)
                                {
                                    string removedTask = tasks[taskNumber - 1];
                                    tasks.RemoveAt(taskNumber - 1);
                                    Console.WriteLine($"Задача \"{removedTask}\" удалена.\n");
                                }
                                else
                                {
                                    Console.WriteLine($"Ошибка: введите номер от 1 до {tasks.Count}.\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ошибка: введите корректный номер задачи.\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Сначала введите /start, чтобы начать работу.\n");
                        }
                        break;
                    #endregion

                    #region Команда /exit
                    case "/exit":
                        Console.WriteLine();
                        if (isNameEntered)
                            Console.WriteLine($"До свидания, {userName}!");
                        else
                            Console.WriteLine("До свидания!");

                        Console.WriteLine("Программа завершена.");
                        isRunning = false;
                        break;
                    #endregion

                    #region Неизвестная команда
                    default:
                        Console.WriteLine("Неизвестная команда. Введите /help для списка команд.\n");
                        break;
                        #endregion
                }
                #endregion
            }
        }
        #region Вспомогательные методы
        static void PrintPersonalizedMessage(bool isNameEntered, string userName, string message)
        {
            if (isNameEntered)
                Console.WriteLine($"{userName}, {message}\n");
            else
                Console.WriteLine($"Совет: сначала введите /start\n");
        }
        #endregion
    }
}