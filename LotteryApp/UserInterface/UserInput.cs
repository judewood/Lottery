using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LotteryApp.UserInterface
{
    public class UserInput : IUserInput
    {
        public string? GetUserInput(){
            return Console.ReadLine();
        }
    }
}