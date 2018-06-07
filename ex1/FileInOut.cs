using System;
using System.IO;

namespace ex1
{
    class FileInOut
    {
        private string[] _obj;      // строки, которые нужно считать
        private int _position;      // позиция считываемо элемента

        // ввод из файла
        public FileInOut(string name)
        {
            var reader = new StreamReader(name + ".txt");
            var strings = reader.ReadToEnd();
            _obj = strings.Split('\n', ' ');
            reader.Close();
        }

        // получение элемента из строки, разделенной пробелами
        public string Next()
        {
            try
            {
                return _obj[_position++];
            }
            catch (Exception)
            {
                return " ";
            }
        }

        // вывод в файл
        public static void ToFile(string name, string obj)
        {
            var f = new StreamWriter(name + ".txt");
            var strings = obj.Split('\n', '\r');
            foreach (var t in strings)
                f.WriteLine(t);
            f.Close();
        }
    }
}
