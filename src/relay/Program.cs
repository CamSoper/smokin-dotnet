using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;

int pin = 19;

Console.WriteLine("Press enter to toggle the relay...");

using GpioController gpio = new GpioController(PinNumberingScheme.Logical, new RaspberryPi3Driver());
gpio.OpenPin(pin, PinMode.Output);
gpio.Write(pin, PinValue.High);

while (true)
{
    Console.ReadLine();
    if (gpio.Read(pin) == PinValue.High)
    {
        gpio.Write(pin, PinValue.Low);
        Console.WriteLine("On!");
    }
    else 
    {
        gpio.Write(pin, PinValue.High);
        Console.WriteLine("Off!");
    }
}


        