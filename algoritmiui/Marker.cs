using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Diagnostics;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Windows.UI;

namespace algoritmiui {
    class Marker {
        public int IRCount { get; set; }
        public int Size { get; set; }
        public long Time { get; set; }
        public Color Colon { get; set; }
        public double LocX { get; set; }
        public double LocY { get; set; }

        // constructor for largest size, count and time
        public Marker(int size, int count, long time) {
            Size = size;
            IRCount = count;
            Time = time;
        }

        // constructor for just count
        public Marker(int count) {
            IRCount = count;
        }

        public Marker(double x, double y) {
            LocX = x;
            LocY = y;
        }

        public Marker(int size, int count, long time, Color rectum) {
            Size = size;
            IRCount = count;
            Time = time;
            Colon = rectum;
        }

        private void this_PointerMoved(object sender, PointerRoutedEventArgs e) {

        }

    }
}
