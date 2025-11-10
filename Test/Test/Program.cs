using System;
using System.Runtime.CompilerServices;
using System.Threading;
class SimpleThreadApp
{

    private static int counter = 0;
    private static int maxCounter;
    private int id;
    
    public SimpleThreadApp(int id)
    {
        this.id = id;
    }

    public void WorkerThreadMethod()
    {
        for(int i = 0; i < maxCounter; i++)
        {
            counter++;
            if (counter % id == 0)
            {
                Console.WriteLine("ID: {0,3} Counter: {1,8} Modulo: {2}", id, counter, (counter % id));
            }
        }
    }
    public static void Main(String[] args)
    {
        try
        {
            maxCounter = int.Parse(args[1]);
            for (int i = 2; i <= int.Parse(args[0]); i++)
            {
                SimpleThreadApp sta = new SimpleThreadApp(i);
                new Thread(new ThreadStart(sta.WorkerThreadMethod)).Start();
                Console.WriteLine("Main - Have requested the start of worker thread {0}", i);
            }
        } catch (Exception e)
        {
            Console.WriteLine("Failed to parse a console argument as integer. Exception: {0}", e);
        }
    }
}