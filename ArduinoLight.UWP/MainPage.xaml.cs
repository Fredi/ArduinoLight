using Microsoft.Maker.RemoteWiring;
using Microsoft.Maker.Serial;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ArduinoLight.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IStream connection;
        private RemoteDevice arduino;
        public static PinState? State = null;

        private const byte LED_PIN = 12;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            connection = new UsbSerial("VID_1A86", "PID_7523");
            arduino = new RemoteDevice(connection);

            arduino.DeviceReady += Setup;

            connection.begin(57600, SerialConfig.SERIAL_8N1);
        }

        private void Setup()
        {
            arduino.pinMode(LED_PIN, PinMode.OUTPUT);

            if (State != null)
            {
                arduino.digitalWrite(LED_PIN, State.Value);
            }
        }

        private void btnLigar_Click(object sender, RoutedEventArgs e)
        {
            arduino.digitalWrite(LED_PIN, PinState.HIGH);
        }

        private void btnDesligar_Click(object sender, RoutedEventArgs e)
        {
            arduino.digitalWrite(LED_PIN, PinState.LOW);
        }
    }
}
