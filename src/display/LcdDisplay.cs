using System;
using System.Device.Gpio;
using System.Device.I2c;
using Iot.Device.CharacterLcd;
using Iot.Device.Mcp23xxx;
using Iot.Device.Pcx857x;
using System.Threading.Tasks;

namespace display
{
    public class LcdDisplay : IDisposable
    {
        I2cDevice _i2c;
        Pcf8574 _driver;
        Lcd2004 _lcd;

        public LcdDisplay(I2cDevice i2c)
        {
            _i2c = i2c;
            _driver = new Pcf8574(_i2c);
            _lcd = new Lcd2004(registerSelectPin: 0, 
                    enablePin: 2, 
                    dataPins: new int[] { 4, 5, 6, 7 }, 
                    backlightPin: 3, 
                    backlightBrightness: 0.1f, 
                    readWritePin: 1, 
                    controller: new GpioController(PinNumberingScheme.Logical, _driver));
        }
        

        public async Task DisplayText(string text)
        {
            int screenWidth = 20;

            string padding = new string(' ', screenWidth);
            string paddedText = $"{padding}{text}{padding}";

            _lcd.Clear();
            for (int i = 0; i <= (text.Length + screenWidth); i++)
            {
                string frame = paddedText.Substring(i, screenWidth);
                _lcd.SetCursorPosition(0, 0);
                _lcd.Write(frame);
                await Task.Delay(TimeSpan.FromMilliseconds(250));
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _lcd.Dispose();
                    _driver.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Display()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }


        #endregion
    }
}
