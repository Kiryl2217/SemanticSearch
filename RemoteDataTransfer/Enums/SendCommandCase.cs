using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteDataTransfer.Enums
{
    public enum SendCommandCase
    {
        // Неопределено.
        NONE = 0,

        // Команда пересылки строки-запроса пользователя.
        SEND_QUERY = 1, //  Ответ должен быть OK.

        // Команда пересылки параграфа.
        SEND_PARAGRAPH = 2, //  Ответ должен быть OK.

        // Команда расчёта релевантности.
        GET_VALUE = 3 //  Ответ должен быть 4 байта с int-числом.
    }
}
