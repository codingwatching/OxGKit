﻿using OxGKit.LoggingSystem;
using UnityEngine;
using UnityEngine.InputSystem;

[LoggerName("LoggingDemo.Logger1")]
public class LoggingDemoLogger1 : Logging { }

[LoggerName("LoggingDemo.Logger2")]
public class LoggingDemoLogger2 : Logging { }

public class LoggingDemo : MonoBehaviour
{
    private void Start()
    {
        // Use logger1 to print
        Logging.Print<LoggingDemoLogger1>("Implement Logger by LoggingDemoLogger1!!!");
        // Use Logger2 to print
        Logging.Print<LoggingDemoLogger2>("Implement Logger by LoggingDemoLogger2!!!");
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            // Use logger1 to print
            Logging.Print<LoggingDemoLogger1>("Implement Logger by LoggingDemoLogger1!!!");
            // Use Logger2 to print
            Logging.Print<LoggingDemoLogger2>("Implement Logger by LoggingDemoLogger2!!!");
        }
    }
}
