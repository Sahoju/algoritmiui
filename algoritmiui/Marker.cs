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
        public int Count { get; set; }
        public int Size { get; set; }
        // 0 = Bubble sort, 1 = Quick sort
        public int Type { get; set; }
        public double LocX { get; set; }
        public double LocY { get; set; }

        public Marker(int size, int count, int type) {
            Size = size;
            Count = count;
            Type = type;
        }
    }
}
