using Freed.Rabbit.MessageConsumer.MessageConsumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freed.Rabbit.MessageConsumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region 生产者消费者
                ProductionConsumer.Show();
                #endregion
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
