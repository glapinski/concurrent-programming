using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Data
{
    internal class DAO : IDisposable
    {
        private BlockingCollection<String> buffer = new BlockingCollection<String>();
        private Task fileWritter;
        private StreamWriter sw;

        public DAO()
        {
            fileWritter = new Task(() => writter());
            fileWritter.Start();
        }

        public void addToBuffer(Ball ball)
        {
            string log = " Ball "
                    + ball.Id
                    + " moved: "
                    + " PositionX: "
                    + Math.Round(ball.PositionX, 4)
                    + " PositionY: "
                    + Math.Round(ball.PositionY, 4)
                    + " SpeedX: "
                    + Math.Round(ball.SpeedX, 4)
                    + " SpeedY: "
                    + Math.Round(ball.SpeedY, 4);

            buffer.Add(log);
        }

        public void Dispose()
        {
            sw.Dispose();
            fileWritter.Dispose();
        }

        public void writter()
        {
            //sw = new StreamWriter("../../../../../Data/log.txt", append: true);
            sw = new StreamWriter("../../../../Data/log.txt", append: true);
            try
            {
                foreach (string i in buffer.GetConsumingEnumerable())
                {
                    sw.WriteLine(i);
                }
            }
            finally
            {
                Dispose();
            }
        }

        public void wrtiteToFile(string log)
        {
            try
            {
                sw.WriteLine(log);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file cannot be found.");
            }
            catch (IOException)
            {
                Console.WriteLine("An I/O error has occurred.");
            }
        }
    }
}