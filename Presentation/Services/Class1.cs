using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public static class o
    {


        public static async void a()
        {
            await Task.Run(() => { });
            ConsoleHelper.WriteWithColor(
                "╭━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━┳━━━┳━━━┳━━━┳━━━┳━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━╮",
                ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor(
                "╰━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━┻━━━┻━━━┻━━━┻━━━┻━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━╯",
                ConsoleColor.Blue);

        }
    }
}