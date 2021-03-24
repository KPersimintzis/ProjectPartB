using System;
using ProjectPartB.Services;
using ProjectPartB.ViewModels;
using ProjectPartB.Entities;
using ProjectPartB.Data;
using System.Collections.Generic;

namespace ProjectPartB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            new Menu().MainMenu();
        }
    }
}
