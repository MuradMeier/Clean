using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AllProject.Model
{
    class Stat
    {
        public Dictionary<string, int> products { get; set; }
        public Dictionary<string, int> services { get; set; }
        public int prodQuant { get; set; }
        public int servQuant { get; set; }
        public double money { get; set; }
        public Dictionary<int, Stat> stat { get; set; }
        public Stat()
        {
        }
        public Stat GetDayFirstStat(int y, int m, int d)
        {
            Stat s = new Stat();
            var date = App.db.OrderCompositions.Where(x => x.Order.CompilationDate.Year == y & x.Order.CompilationDate.Month == m & x.Order.CompilationDate.Day == d).ToList();
            if (date.Any())
            {
                foreach (var item in date.ToList())
                {
                    if (s.products.ContainsKey(item.Product.Name))
                    {
                        s.products[item.Product.Name]++;
                    }
                    else
                    {
                        s.products.Add(item.Product.Name, 1);
                    }
                    if (s.services.ContainsKey(item.Service.ServiceName))
                    {
                        s.services[item.Service.ServiceName]++;
                    }
                    else
                    {
                        s.services.Add(item.Service.ServiceName, 1);
                    }
                    s.prodQuant += 1;
                    s.servQuant += 1;
                    s.money += item.Product.Price * item.Service.ServiceCoefficient * item.Quantity;
                }
            }
            return s;
        }

        public Stat GetStat(Stat s)
        {
            foreach(var p in s.products.Keys)
            {
                s.products[p] = s.products[p] / s.prodQuant * 100; 
            }
            foreach(var k in s.services.Keys)
            {
                s.services[k] = s.services[k] / s.servQuant * 100;
            }
            return s;
        }

        public Dictionary<string, int> CheckDict(Dictionary<string, int> d, Dictionary<string, int> dict)
        {
            foreach (var item in dict)
            {
                if (d.ContainsKey(item.Key))
                {
                    d[item.Key] = item.Value;
                }
                else
                {
                    d.Add(item.Key, item.Value);
                }
            }
            return d;
        }

        public Stat GetMonthFirstStat(int y, int m)
        {
            Stat s = new Stat();
            int d = DateTime.DaysInMonth(y, m);
            for (int i = 1; i <= d; i++)
            {
                var day = GetDayFirstStat(y, m, i);
                if (day.money > 0)
                {
                    s.services = CheckDict(s.services, day.services);
                    s.products = CheckDict(s.products, day.products);
                    s.prodQuant += 1;
                    s.servQuant += 1;
                    s.money += day.money;
                }
                s.stat.Add(i, GetStat(day));
            }
            return s;
        }
        public Stat YearStat(int y)
        {
            Stat s = new Stat();
            for (int i = 1; i <= 12; i++)
            {
                var month = GetMonthFirstStat(y, i);
                if (month.money != 0)
                {
                    s.services = CheckDict(s.services, month.services);
                    s.products = CheckDict(s.products, month.products);
                    money += month.money;
                    prodQuant += 1;
                    servQuant += 1;
                    s.stat.Add(i, GetStat(month));
                }
            }
            return GetStat(s);
        }
    }
}
