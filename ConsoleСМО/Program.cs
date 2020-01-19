using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleСМО
{
    class Program:Random
    {
        static void Main()
        {
            Program program = new Program();
            program.SMO();
            Console.WriteLine("Работа закончена");
        }

        void SMO()
        {

           /* //Входные данные
            Console.Write("Введите лямбду: ");
            double lym = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите ню: ");
            double nu = Convert.ToDouble(Console.ReadLine()); // Интенсивность
            Console.Write("Введите Imax: ");
            int imax = Convert.ToInt32(Console.ReadLine()); // Максимальное число заявок
            */

            double lym = 0.1;
            double nu = 1;
            int imax = 20;
            

            //Необходимые переменные
            List<double> T1 = new List<double>(); // Моменты времяни прихода заявки
            List<double> T2 = new List<double>(); // Длительность обслуживания
            List<double> T3 = new List<double>(); // Моменты времени, когда обслуженные заявки покидают систему
            List<double> T4 = new List<double>(); // Моменты времени, когда заявки теряются
            int i = 0; // Счётчик заявок
            double t = 0; // текущее время
            double t1 = new double(); // Время обслуживания

            for (; ; )
            {
                //Моделируем время прихода заявки
                double r = base.Sample(); // Случайное число от 0 до 1
                double t2 = -Math.Log(r) * 1 / lym; // Интервал между заявками
                T1.Add(t + t2);
                t = t + t2;

                // Моделируем обслуживание
                r = base.Sample();
                t1 = -1 / nu * Math.Log(r);
                T2.Add(t1);
                T3.Add(t + t1);
                t = t + t1;

                i++;
                if (i < imax)
                {
                    r = base.Sample();
                    t2 = -1 / nu * Math.Log(r);
                    if (t - t1 + t2 > t)
                    {
                        t = t - t1 + t2;

                        // Обслуживаем новую заявку
                        r = base.Sample();
                        t1 = -1 / nu * Math.Log(r);
                        T2.Add(t1);
                        T3.Add(t + t1);
                        t = t + t1;
                    }
                    else
                    {
                        //Обслуживающее устройство занято, заявка теряется
                        T4.Add(t - t1 + t2);
                        T1.Add(T4[T4.Count - 1]);
                    }
                }
                else
                {
                    

                    Console.WriteLine("Пришло " + (i) + " заявок");
                    Console.WriteLine("Потерялось " + T4.Count + " заявок");

                    break;
                }
            }
        }
    }
}
