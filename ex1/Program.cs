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
            PrM[0] = new Point[MaxN];              // первая вершина прямоугольника (меньшая)
            PrM[1] = new Point[MaxN];              // вторая вершина прямоугольника (большая)
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

        // сортировка методом Хоара координат a с одновременной перестановкой координат b
        public static void SortAB(int[] Oa, int[] Ob, int lf, int rg)
        {
            int i = lf, j = rg, x = Oa[(lf + rg) / 2];
            do
            {
                while (x > Oa[i]) i++;
                while (Oa[j] > x) j--;
                if (i <= j)
                {
                    if (i < j)
                    {
                        Swap(ref Oa[i], ref Oa[j]);
                        Swap(ref Ob[i], ref Ob[j]);
                    }
                    i++;
                    j--;
                }
            } while (i <= j);
            if (lf < j)
                SortAB(Oa, Ob, lf, j);
            if (i < rg)
                SortAB(Oa, Ob, i, rg);
        }

        // определение площади заданной фигуры
        public static int Solve()
        {
            Sort(ref Ox, 0, N * 2 - 1);     // сортировка координат x по возрастанию
            int m = 0;                      // длина сечения
            int res = 0;                    // площадь

            //прибавление площади рассматриваемого сечения
            for (int i = 0; i < N * 2; i++)
            {
                if (i != 0) res = res + Math.Abs((Ox[i] - Ox[i - 1]) * m);
                if ((i == 0) || Ox[i] != Ox[i - 1])
                    Change(Ox[i], ref m);   //определение нового значения длины сечения
            }
            return res;
        }

        // пересечение прямоугольников
        public static bool Peres(int k, int x)
        {
            if (PrM[0][k].X <= x && PrM[1][k].X > x) return true;
            else return false;
        }

        // изменение длины сечения для соответствующей координаты
        public static void Change(int x, ref int res)
        {
            int m = 0;
            for (int i = 0; i < N; i++)
                if (Peres(i, x))                // если есть пересечение
                {
                    // формирование массива ординат для данной координаты х
                    Oy[m] = PrM[0][i].Y;
                    Oy[m + 1] = PrM[1][i].Y;
                    // определение признака начала или конца отрезка
                    FOy[m] = 1; FOy[m + 1] = -1;
                    m += 2;
                }
            if (m == 0) res = 0;
            else
            {
                SortAB(Oy, FOy, 0, m - 1);          // сортировка Oy с перестановкой FOy
                res = Math.Abs(Calc(Oy, FOy, m));   // новая длина сечения
            }
        }

        // определение новой длины сечения
        public static int Calc(int[] Ox, int[] SOx, int n)
        {
            int sc = 0;                 // сечение
            int priznak = 0;            // значение признака
            if (SOx[0] > 0) priznak++;
            for (int i = 1; i < n; i++)
            {
                if (priznak > 0) sc = sc + Ox[i] - Ox[i - 1];
                priznak += SOx[i];
            }
            return sc;
        }

        // результат выполнения программы
        public static void Done(int square)
        {
            FileInOut.ToFile("output", square.ToString());
        }

        static void Main(string[] args)
        {
            Init();
            Done(Solve());
        }
    }
}
