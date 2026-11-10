using System.Net.Sockets;
using System.Net;
using Microsoft.Extensions.Configuration;
using SemanticSearchLibrary.Classes;
using RemoteDataTransfer.Classes;
using RemoteDataTransfer.Enums;

public class Program
{
    static string query = string.Empty;
    static string paragraph = string.Empty;
    static MyDic? dic = null;
    
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Ошибка! Для запуска необходимо 2 параметра: \n" +
                "1) номер узла от единицы \n" +
                "2) y/n - показывать или нет подробный ход решения.");
            return;
        }

        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Получаем анализатор
        Analyzer analyzer = new Analyzer(config);

        // Получаем номер узла из 1-ого аргумента.
        var nodeNumber = int.Parse(args[0]);
        Console.WriteLine("Номер узла: " + nodeNumber);
        int port = 10000 + nodeNumber;

        // Показываем ли расчеты в консоли
        
        var showArg = args[1].ToLower().Trim();
        var showConsoleResults = showArg.Equals("y"); 



        IPHostEntry ipHost = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = ipHost.AddressList[0];
        IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

        Socket socketListner = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        // Связывает созданный сокет с конкретным локальным адресом.
        socketListner.Bind(ipEndPoint);
        socketListner.Listen(10);

        // Добавить комманду завершения работы
        while (true)
        {
            if (showConsoleResults)
                Console.WriteLine($"Ожидаем подключение к узлу {nodeNumber}...");
            Socket communicationSocket = socketListner.Accept(); // Ожидание подключения.
            if (showConsoleResults)
                Console.WriteLine($"Подключено к узлу {nodeNumber}!");

            // Получение команды.
            byte[] commandBytes = new byte[1];
            communicationSocket.Receive(commandBytes);
            var sendCommandCase = (SendCommandCase)commandBytes[0];

            switch (sendCommandCase)
            {
                case SendCommandCase.SEND_QUERY:
                    if (showConsoleResults)
                        Console.WriteLine("Получена команда пересылки запроса!");
                    NodeWorker.sendAnswerOK(communicationSocket);
                    query = NodeWorker.getString(socketListner);
                    if (showConsoleResults)
                        Console.WriteLine("Получен запрос: " + query);
                    break;

                case SendCommandCase.SEND_PARAGRAPH:
                    if (showConsoleResults)
                        Console.WriteLine("Получена команда пересылки параграфа!");
                    NodeWorker.sendAnswerOK(communicationSocket);
                    paragraph = NodeWorker.getString(socketListner);
                    if (showConsoleResults)
                        Console.WriteLine("Получен параграф: " + paragraph);

                    // Получение всех слов
                    var words = analyzer.GetManyWithoutDB(paragraph);
                    // Словарь со всеми словами
                    dic = analyzer.DivisionFromDB(words);
                    break;

                case SendCommandCase.GET_VALUE:
                    if (dic == null)
                    {
                        Console.WriteLine("Параграф не получен и(или) словарь не создан!");
                        break;
                    }
                    if (showConsoleResults)
                        Console.WriteLine("Получена команда расчёта значения!");
                    int r = analyzer.GetCompareParagraph(query, paragraph, dic);
                    NodeWorker.sendAnswerInt(communicationSocket, r);
                    if (showConsoleResults)
                        Console.WriteLine($"Отправлен рейтинг параграфа: {r} от узла {nodeNumber}");
                    break;

                default:
                    //sendAnswerOK(communicationSocket);
                    break;
            }
        }
    }
}