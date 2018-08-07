using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }
        DispatcherTimer dt = null;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick+=new EventHandler(dt_Tick);
            dt.Start();
        }
        public int s = 0;
        private void dt_Tick(object sender, EventArgs e) 
        {
            ++s;
            label1.Content = s.ToString();

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            dt.Stop();
        }
    }
}
