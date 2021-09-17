
using System;
using System.Device.I2c;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace display
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await DisplayText(args[0]);
            return;
        }

        static async Task DisplayText(string textToDisplay)
        {
            using var i2c = I2cDevice.Create(new I2cConnectionSettings(1, 0x27));
            using var lcd = new LcdDisplay(i2c);
            {
                await lcd.DisplayText(textToDisplay);
            }
        }

    }
}
