using System;
using System.Collections.Generic;
using System.Linq;

namespace Railways.Manager
{
    public class ConsoleAppManager
    {
        public string CharacterCheck()
        {
            #region Değişkenler
            double val = 0;
            string row = "";
            bool isNumber = false;
            ConsoleKeyInfo character;
            #endregion

            do
            {
                character = Console.ReadKey(true);

                //eğer silme işlemi yapılmadıysa
                if (character.Key != ConsoleKey.Backspace)
                {
                    // ":" nokta kullanılmadan önce sadece harf girdisi eğer ":" kullanıldıysa sadece sayı girdisi işlemleri
                    if (!isNumber)
                    {
                        //harf kontrolu
                        bool kontrol = double.TryParse(character.KeyChar.ToString(), out val);
                        if (!kontrol)
                        {
                            row += character.KeyChar;
                            Console.Write(character.KeyChar);
                        }
                    }
                    else
                    {
                        //sayı kontrolu
                        bool kontrol = double.TryParse(character.KeyChar.ToString(), out val);
                        if (kontrol)
                        {
                            row += character.KeyChar;
                            Console.Write(character.KeyChar);
                        }
                    }

                }
                //eğer silme işlemi yapıldıysa
                else
                {
                    //eğer silme tuşuna basıldıysa ve satırda veri var ise
                    if (character.Key == ConsoleKey.Backspace && row.Length > 0)
                    {
                        //rowu düzenle ve consolu 1 adım silerek geri al
                        row = row.Substring(0, (row.Length - 1));
                        Console.Write("\b \b");
                    }
                }
                //eğer ":" tuşuna basıldıysa
                if (character.Key == ConsoleKey.OemPeriod)
                {
                    isNumber = true;
                }
            }
            while (character.Key != ConsoleKey.Enter);

            return row;
        }

        public void DataManagement(string row, List<string> ways)
        {
            //eğer girdi de query bulunuyorsa veriyi sorgula
            if (row.ToLower().Contains("query"))
            {
                //veri gösterme işlemleri
                DataShow(row, ways);
            }
            //veri sorgulama değilse veri girişidir.
            else
            {
                //veri işleme işlemleri
                DataOperations(row, ways);
            }
        }

        public void DataShow(string row, List<string> ways)
        {
            try
            {
                var rowStartIndex = row.IndexOf("(");
                var rowEndIndex = row.IndexOf(")");

                //query(a-b) sorgusundaki a-b 'yi alma
                var rowArea = row.Substring((rowStartIndex + 1), (rowEndIndex - rowStartIndex - 1));

                //verilerileri ":" noktaya kadar al ve kıyasla
                var result = ways.Where(x => x.Substring(0, x.IndexOf(":")) == rowArea).FirstOrDefault();

                //sonuç null değilse veriyi al null ise rowareaya ":-" ekle
                result = result ?? rowArea + ":-";

                Console.WriteLine("\nSonuç => " + result + "\n\nİşleminizi Giriniz");
                row = "";
            }
            catch (Exception e)
            {
                Console.WriteLine("\nquery sorgusunda format hatası bulunmaktadır.!\n\nİşleminizi Giriniz");
            }
        }

        public void DataOperations(string row, List<string> ways)
        {
            try
            {
                var rowArea = row.Substring(0, row.IndexOf(":"));
                //gelen veriyi dizi içinde ara var ise güncelleme yap yok ise ekleme yap.
                var itemIndex = ways.FindIndex(x => x.ToLower().Substring(0, rowArea.Length <= x.Length ? rowArea.Length : 0) == rowArea.ToLower());
                if (itemIndex >= 0)
                {
                    ways[itemIndex] = row;
                    Console.WriteLine("\nVeri Güncellendi.. \n\nİşleminizi Giriniz");
                }
                else
                {
                    //boşlukları silerek kaydet
                    ways.Add(row.Replace(" ", string.Empty));
                    Console.WriteLine("\nVeri Kaydedildi.. \n\nİşleminizi Giriniz");
                }
                row = "";
            }
            catch (Exception e)
            {
                Console.WriteLine("\nVeri girişinde format hatası bulunmaktadır.!\n\nİşleminizi Giriniz");
            }
        }
    }
}
