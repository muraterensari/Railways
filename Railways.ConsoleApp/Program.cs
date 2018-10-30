using Railways.Manager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Railways.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Değişkenler
            string row = "";
            List<string> ways = new List<string>();
            #endregion

            #region Managerler
            ConsoleAppManager _consoleAppManager = new ConsoleAppManager(); 
            #endregion

            Console.WriteLine("İşleminizi Giriniz ");

            //sonsuz döngü
            for (;;)
            {
                //veri girişi kontrolleri
                row = _consoleAppManager.CharacterCheck();

                //veri kaydetme işlemleri
                _consoleAppManager.DataManagement(row, ways);
            }
        }
    }
}
