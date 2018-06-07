using System;


namespace ex1
{
    class Program
    {
        public static int MaxN = 100;                   // Максимальное кол-во прямоугольников
        public static int N;                            // Заданное кол-во прямоугольников
        public static Point[][] PrM = new Point[2][];   // Массив, содержащий информацию о прямоугольниках (противоположные вершины)
        public static int[] Ox = new int[MaxN * 2];     // Координаты x
        public static int[] Oy = new int[MaxN * 2];     // Координаты y
        public static int[] FOy = new int[MaxN * 2];    // Признак начала и конца отрезка
        public static int[] SOx = new int[MaxN * 2];    // Отсортированные координаты x

        // перестановка
        public static void Swap(ref int a, ref int b)
        {
            int t;
            t = a; a = b; b = t;
        }

        // Инициализация
        public static void Init()
        {
            var input = new FileInOut("input");   // считыватель из файла
            N = Int32.Parse(input.Next());        // количество прямоугольников
            PrM[0] = new Point[100];              // первая вершина прямоугольника (меньшая)
            PrM[1] = new Point[100];              // вторая вершина прямоугольника (большая)
            for (int i = 0; i < N; i++)
            {
                PrM[0][i] = new Point(Int32.Parse(input.Next()), Int32.Parse(input.Next()));
                PrM[1][i] = new Point(Int32.Parse(input.Next()), Int32.Parse(input.Next()));
                if (PrM[0][i].X > PrM[1][i].X)
                    Swap(ref PrM[0][i].X, ref PrM[1][i].X);
                if (PrM[0][i].Y > PrM[1][i].Y)
                    Swap(ref PrM[0][i].Y, ref PrM[1][i].Y);
                Ox[i * 2] = PrM[0][i].X;
                Ox[i * 2 + 1] = PrM[1][i].X;
            }
        }

        // сортировка методом Хоара координат x
        public static void Sort(ref int[] Ox, int lf, int rg)
        {
            int i = lf, j = rg, x = Ox[(lf + rg) / 2];
            do
            {
                while (x > Ox[i]) i++;
                while (Ox[j] > x) j--;
                if (i <= j)
                {
                    if (i < j)
                        Swap(ref Ox[i], ref Ox[j]);
                    i++;
                    j--;
                }
            } while (i <= j);
            if (i < rg)
                Sort(ref Ox, i, rg);
            if (lf < j)
                Sort(ref Ox, lf, j);
        }

        static void Main(string[] args)
        {

        }
    }
}
