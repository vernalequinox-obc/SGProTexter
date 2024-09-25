using System;
using System.Timers;

namespace SGProTexter
{
    internal class CountDownTimerOptions
    {
        private static System.Timers.Timer aTimer;
        private static int CountdownEvent = 5;

        public static int StartCountDown()
        {
            int ReturnOption = FileHostData.ExitToSystemInt;
            Console.Clear();
            Console.WriteLine("\n\n\tSilent mode started in " + CountdownEvent + " seconds.");
            Console.WriteLine("\t\tPress any key for menu.\n");
            SetTimer();
            while (CountdownEvent > 0)
            {
                if (Console.KeyAvailable)
                {
                    ReturnOption = FileHostData.RunConsoleMainMenuInt;
                    break;
                }
                else
                {
                    ReturnOption = FileHostData.RunSilientModeInt;
                }
            }
            aTimer.Stop();
            aTimer.Dispose();
            return ReturnOption;
        }

        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("\tSilent Mode in: " + CountdownEvent);
            --CountdownEvent;
        }
    }
}
