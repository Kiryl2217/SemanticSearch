using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteDataTransfer.Enums
{
    public enum AnswerCommandCase
    {
        // Неопределено.
        NONE = 0,

        // Команда пересылки строки-запроса пользователя.
        OK = 1,

        // Команда пересылки параграфа.
        ERROR = 2
    }
}
