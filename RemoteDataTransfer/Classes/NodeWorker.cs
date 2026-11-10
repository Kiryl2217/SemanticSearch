using System.Net.Sockets;
using System.Net;
using RemoteDataTransfer.Enums;

namespace RemoteDataTransfer.Classes
{
    public static class NodeWorker
    {
        // Как клиент подключаемся к удалённому узлу.
        public static void SendBytesToOperateNode(int port, byte[] sendBytes, ref byte[] getBytes)
        {
            IPHostEntry ipHost = Dns.GetHostEntry("localhost"); // Удалённый узел сервера.
            IPAddress ipAddress = ipHost.AddressList[0]; // IP-адрес удаленного узла сервера.
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port); // Удалённая точка подключения к серверу.

            // Наш сокет клиента.
            Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Соединяем.
            sender.Connect(ipEndPoint);

            // Отправляем байты.
            sender.Send(sendBytes);

            // Получаем байты.
            sender.Receive(getBytes);

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }

        // Пересылка команды.
        // Возвращает true, если ответ OK.
        public static bool SendCommand(int port, SendCommandCase sendCommandCase)
        {
            var sendBytes = new byte[1] { (byte)sendCommandCase };
            var getBytes = new byte[1] { 0 };

            SendBytesToOperateNode(port, sendBytes, ref getBytes);

            var answerCommandCase = (AnswerCommandCase)getBytes[0];
            return answerCommandCase == AnswerCommandCase.OK;
        }

        // Пересылка текста.
        // Возвращает true, если ответ OK.
        public static bool SendString(int port, string text)
        {
            var sendBytes = System.Text.Encoding.UTF8.GetBytes(text);
            var getBytes = new byte[1] { 0 };

            SendBytesToOperateNode(port, sendBytes, ref getBytes);

            var answerCommandCase = (AnswerCommandCase)getBytes[0];
            return answerCommandCase == AnswerCommandCase.OK;
        }

        // Пересылка команды с целью получить в ответ число int.
        public static int RequestInt(int port, SendCommandCase sendCommandCase)
        {
            var sendBytes = new byte[1] { (byte)sendCommandCase };
            var getBytes = new byte[sizeof(int)];
            var ints = new int[1];

            SendBytesToOperateNode(port, sendBytes, ref getBytes);

            // Из массива байт получаем массив int.
            Buffer.BlockCopy(getBytes, 0, ints, 0, sizeof(int));

            // Нужен только первый int (и единственный).
            return ints[0];
        }

        public static void sendAnswerOK(Socket socket)
        {
            var sendBytes = new byte[1] { (byte)AnswerCommandCase.OK };
            socket.Send(sendBytes);
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        public static void sendAnswerInt(Socket socket, int number)
        {
            var sendBytes = new byte[sizeof(int)];
            var ints = new int[1] { number };
            Buffer.BlockCopy(ints, 0, sendBytes, 0, sizeof(int));
            socket.Send(sendBytes);
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        public static string getString(Socket socketListner)
        {
            var getBytes = new byte[100000];
            Socket communicationSocket = socketListner.Accept(); // Ожидание подключения.
            var countBytes = communicationSocket.Receive(getBytes);
            sendAnswerOK(communicationSocket);

            var bb = new byte[countBytes];
            Array.Copy(getBytes, 0, bb, 0, countBytes);
            return System.Text.Encoding.UTF8.GetString(bb);
        }
    }
}
