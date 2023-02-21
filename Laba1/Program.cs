using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int N = 20;

            var text = new Symbol[N];

            Random rnd = new Random();

            double sum = 0;
            for (int i = 0; i < N; i++)
            {
                double p = rnd.NextDouble();

                text[i] = new Symbol();

                text[i].frequency = p;
                sum += p;
            }

            foreach (Symbol s in text)
                s.frequency /= sum;

            Array.Sort(text, new CompareByfrequency());

            ShennonFano(text.ToList());

            foreach (Symbol s in text)
            {
                foreach (bool b in s.code)
                {
                    if (b)
                        Console.Write("1 ");
                    else
                        Console.Write("0 ");
                }
                Console.WriteLine("частота: " + s.frequency);
            }
        }

        public static void ShennonFano(List<Symbol> t)
        {
            double s1 = t[0].frequency;
            int i1 = 0;
            double s2 = t[t.Count - 1].frequency;
            int i2 = t.Count - 1;

            while (i2 - i1 > 1)
            {
                if (s1 > s2)
                {
                    i2--;
                    s2 += t[i2].frequency;
                }
                else
                {
                    i1++;
                    s1 += t[i1].frequency;
                }
            }

            List<Symbol> t1 = new List<Symbol>();
            List<Symbol> t2 = new List<Symbol>();

            for (int i = 0; i <= i1; i++)
            {
                t[i].code.Add(true);
                t1.Add(t[i]);
            }
            for (int i = i2; i < t.Count; i++)
            {
                t[i].code.Add(false);
                t2.Add(t[i]);
            }

            if (t1.Count > 1)
                ShennonFano(t1);
            if (t2.Count > 1)
                ShennonFano(t2);
        }
    }

    public class CompareByfrequency : IComparer<Symbol>
    {
        public int Compare(Symbol s1, Symbol s2)
        {
            return s2.frequency.CompareTo(s1.frequency);
        }
    }

    public class Symbol
    {
        public double frequency;

        public List<bool> code = new List<bool>();
    }
}

