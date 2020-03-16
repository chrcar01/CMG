using CMG.Tools.Evaluators.Interfaces;
using CMG.Tools.Evaluators.Sensors;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CMG.Tools.Evaluators
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSensorEvaluator(this IServiceCollection services, bool registerBuiltInSensors = true)
        {
            return AddSensorEvaluator(services, registerBuiltInSensors, null);
        }

        public static IServiceCollection AddSensorEvaluator(this IServiceCollection services, Action<ISensorFactory> addSensors)
        {
            return AddSensorEvaluator(services, true, addSensors);
        }

        public static IServiceCollection AddSensorEvaluator(this IServiceCollection services, bool registerBuiltInSensors, Action<ISensorFactory> addSensors)
        {
            services.AddSingleton<ICalculate, MathNetCalculator>();
            services.AddSingleton<ISensorFactory, SensorFactory>();
            services.AddSingleton<ISensorLogParser, StringSensorLogParser>();
            services.AddSingleton<ISensorEvaluator, SensorEvaluator>();
            var provider = services.BuildServiceProvider();
            var sensorFactory = provider.GetService<ISensorFactory>();
            if (registerBuiltInSensors)
            {
                RegisterBuiltInSensors(sensorFactory);
            }

            addSensors?.Invoke(sensorFactory);

            SensorEvaluator.Initialize(provider.GetService<ISensorEvaluator>());

            return services;
        }

        private static void RegisterBuiltInSensors(ISensorFactory sensorFactory)
        {
            sensorFactory.RegisterSensor("thermometer", (name, refs, calc) => new ThermometerSensor(name, refs["temperature"], calc));
            sensorFactory.RegisterSensor("humidity", (name, refs, calc) => new HumiditySensor(name, (decimal)refs["humidity"]));
            sensorFactory.RegisterSensor("monoxide", (name, refs, calc) => new MonoxideSensor(name, (int)refs["ppm"]));
        }
    }
}
