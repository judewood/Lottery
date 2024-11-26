using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LotteryApp.UserInterface
{
    public interface IUserInput
    {
        string? GetUserInput();
    }
}