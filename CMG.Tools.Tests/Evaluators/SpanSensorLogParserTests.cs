﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using CMG.Tools.Evaluators;
using NUnit.Framework;

namespace CMG.Tools.Tests.Evaluators
{
    [TestFixture]
    public class SpanSensorLogParserTests
    {
        [Test]
        public void CanParse()
        {
            var calculator = new MathNetCalculator();
            var deviceFactory = new SensorFactory(calculator);
            var parser = new SpanSensorLogParser(deviceFactory);
            var content = GetInputData();
            parser.Parse(content);
            //var sensors = parser.Parse(content)?.ToDictionary(k => k.Name, v => v.GetStatus());
            //Console.WriteLine(JsonSerializer.Serialize(sensors, new JsonSerializerOptions { WriteIndented = true }));
        }



        private string GetInputData()
        {
            return @"reference 70.0 45.0 6
thermometer temp-1
2007-04-05T22:00 72.4
2007-04-05T22:01 76.0
2007-04-05T22:02 79.1
2007-04-05T22:03 75.6
2007-04-05T22:04 71.2
2007-04-05T22:05 71.4
2007-04-05T22:06 69.2
2007-04-05T22:07 65.2
2007-04-05T22:08 62.8
2007-04-05T22:09 61.4
2007-04-05T22:10 64.0
2007-04-05T22:11 67.5
2007-04-05T22:12 69.4
thermometer temp-2
2007-04-05T22:01 69.5
2007-04-05T22:02 70.1
2007-04-05T22:03 71.3
2007-04-05T22:04 71.5
2007-04-05T22:05 69.8
humidity hum-1
2007-04-05T22:04 45.2
2007-04-05T22:05 45.3
2007-04-05T22:06 45.1
humidity hum-2
2007-04-05T22:04 44.4
2007-04-05T22:05 43.9
2007-04-05T22:06 44.9
2007-04-05T22:07 43.8
2007-04-05T22:08 42.1
monoxide mon-1
2007-04-05T22:04 5
2007-04-05T22:05 7
2007-04-05T22:06 9
monoxide mon-2
2007-04-05T22:04 2
2007-04-05T22:05 4
2007-04-05T22:06 10
2007-04-05T22:07 8
2007-04-05T22:08 6
";
        }
    }
}