using CMG.Tools.Evaluators;
using System;
using System.IO;
using CMG.SensorConsole.Sensors;
using Microsoft.Extensions.DependencyInjection;

namespace CMG.SensorConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSensorEvaluator(factory => factory.RegisterSensor("crazy", (name, refs, calc) => new CrazySensor(name)))
                .BuildServiceProvider();

            var logFile = File.ReadAllText(@"logs.txt");
            Console.WriteLine(SensorEvaluator.EvaluateLogFile(logFile));

        }
    }
}
