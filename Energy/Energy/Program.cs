
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Energy
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tick> ticks = new List<Tick>
            {
                new Tick("Computer 1", 0.005),
                new Tick("Computer 1, Lamp 1", 0.0051),
                new Tick("Computer 1, Lamp 1, Freese 1", 0.2051),
                new Tick("Computer 1, Lamp 1, Freese 1, Condition 1", 0.3051),
                new Tick("Computer 1, Lamp 1, Freese 1, Condition 1, DishWasher 1", 0.8051)
            };
            Fill(ticks);
            InputData inputData = new InputData(ticks);


            for (int i = 0; i < 50; i++)
            {
                Manager.Update(inputData);
                Thread.Sleep(100);
            }

            var form = new Menu();
            Application.Run(form);

            Console.ReadLine();
        }

        private static void Fill(List<Tick> ticks)
        {
            ticks.Add(new Tick("Computer 1, Lamp 1, Freese 1, Condition 1, DishWasher 1", 0.8051));
            ticks.Add(new Tick("Computer 1, Lamp 1, Freese 1, Condition 1, DishWasher 2", 1.4051));
            ticks.Add(new Tick("Computer 1, Freese 1, Condition 1, DishWasher 2", 1.4050));
            ticks.Add(new Tick("Computer 1, Freese 3, Condition 1, DishWasher 2", 1.9056));
            ticks.Add(new Tick("Computer 2, Freese 3, Condition 1, DishWasher 2", 1.9116));
            ticks.Add(new Tick("Computer 3, Freese 3, Condition 1, DishWasher 2", 1.9196));
            ticks.Add(new Tick("Computer 3, Lamp 5, Freese 3, Condition 1, DishWasher 2", 1.9206));
            ticks.Add(new Tick("Computer 3, Lamp 5, Freese 3, Condition 1", 0.9206));
            ticks.Add(new Tick("Computer 3, Lamp 5, Freese 3", 0.8206));
            ticks.Add(new Tick("Computer 3, Lamp 5, Freese 3, Condition 1", 0.9106));
            ticks.Add(new Tick("Computer 2, Lamp 5, Freese 3, Condition 1", 0.9016));
            ticks.Add(new Tick("Lamp 5, Freese 3, Condition 1", 0.8920));
            ticks.Add(new Tick("Lamp 5, Freese 4, Condition 1", 1.0920));
            ticks.Add(new Tick("Lamp 5, Freese 4, Condition 2", 1.2920));
        }
    }
}
