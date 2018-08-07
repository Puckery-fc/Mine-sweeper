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
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            DrawMap();
            buLei();
         OnPaint();

        }

        void DrawMap()  //动态生成地图
        {
            sp1.Children.Clear();   //清空
            Grid g1 = new Grid();
            g1.Margin = new Thickness(10);
            g1.Width = 198;
            g1.Height = 198;
            sp1.Children.Add(g1);
            for (int i = 0; i < 9; i++)
            {
                RowDefinition r = new RowDefinition();
                g1.RowDefinitions.Add(r);
            }
            for (int i = 0; i < 9; i++)
            {
                ColumnDefinition c = new ColumnDefinition();
                g1.ColumnDefinitions.Add(c);
            }

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    Button b = new Button();
                    g1.Children.Add(b);
                    b.SetValue(Grid.RowProperty, i);
                    b.SetValue(Grid.ColumnProperty, j);
                    b.Name = "bt" + i.ToString() + j.ToString();    //注意
                }

            LCtb.Text = Convert.ToString(10);
            TT.Text = "00:00:00";

        }


        const int col = 9;
        const int row = 9;

        public struct map
        {
            public bool m_Type;   //true,false
            public int m_Around;
            public string name;
            public bool m_IsUsed;    //是否已被点开
        };

        public map[,] m_NodeList = new map[9, 9];


        public void buLei()//随机布雷
        {

            int k1, k2;

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    m_NodeList[i, j] = new map { m_Type = false, m_Around = 0, name = "bt" + i.ToString() + j.ToString(), m_IsUsed = false };
                }

            for (int i = 0; i < 10; i++)
            {
                Random r = new Random();

                k1 = r.Next(9);
                k2 = r.Next(9);
                if (m_NodeList[k1, k2].m_Type != true)
                {
                    m_NodeList[k1, k2].m_Type = true;
                }
                else
                {
                    i--;
                    continue;
                }
                if (((k1 - 1 >= 0) && (k2 - 1 >= 0)) && (m_NodeList[k1 - 1, k2 - 1].m_Type != true))//左上方
                {
                    //  m_NodeList[k1 - 1, k2 - 1].m_Type = false;
                    m_NodeList[k1 - 1, k2 - 1].m_Around += 1;
                }
                if ((k1 - 1 >= 0) && m_NodeList[k1 - 1, k2].m_Type != true)//上方
                {
                    //  m_NodeList[k1 - 1, k2].m_Type = false;
                    m_NodeList[k1 - 1, k2].m_Around += 1;
                }
                if (((k1 - 1 >= 0) && (k2 + 1 < col)) && m_NodeList[k1 - 1, k2 + 1].m_Type != true)//右上方
                {
                    //  m_NodeList[k1 - 1, k2 + 1].m_Type = false;
                    m_NodeList[k1 - 1, k2 + 1].m_Around += 1;
                }
                if ((k2 - 1 >= 0) && m_NodeList[k1, k2 - 1].m_Type != true)//左方
                {
                    // m_NodeList[k1, k2 - 1].m_Type = false;
                    m_NodeList[k1, k2 - 1].m_Around += 1;
                }
                if ((k2 + 1 < col) && m_NodeList[k1, k2 + 1].m_Type != true)//右方
                {
                    //  m_NodeList[k1, k2 + 1].m_Type = false;
                    m_NodeList[k1, k2 + 1].m_Around += 1;
                }
                if (((k1 + 1 < row) && (k2 - 1 >= 0)) && m_NodeList[k1 + 1, k2 - 1].m_Type != true)//左下方
                {
                    //  m_NodeList[k1 + 1, k2 - 1].m_Type = false;
                    m_NodeList[k1 + 1, k2 - 1].m_Around += 1;
                }
                if ((k1 + 1 < row) && m_NodeList[k1 + 1, k2].m_Type != true)//下方
                {
                    //  m_NodeList[k1 + 1, k2].m_Type = false;
                    m_NodeList[k1 + 1, k2].m_Around += 1;
                }
                if (((k1 + 1 < row) && (k2 + 1 < col)) && m_NodeList[k1 + 1, k2 + 1].m_Type != true)//右下方
                {
                    //   m_NodeList[k1 + 1, k2 + 1].m_Type = false;
                    m_NodeList[k1 + 1, k2 + 1].m_Around += 1;
                }
            }

        }


        void OnPaint()
        {
         
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    if (m_NodeList[i, j].m_Type == true)
                    {
                        Button a = LogicalTreeHelper.FindLogicalNode(this, "bt" + i.ToString() + j.ToString()) as Button;

                        //ImageBrush im = new ImageBrush();
                        //im.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/Lei.jpg", UriKind.Absolute));
                        //a.Background = im;

                        a.Click += new RoutedEventHandler(button_Click);////
                        //a.MouseRightButtonDown += new MouseButtonEventHandler(button1_MouseRightButtonDown);
                    }
                    else
                    {
                        if (m_NodeList[i, j].m_Around == 0)
                        {
                            Button a = LogicalTreeHelper.FindLogicalNode(this, "bt" + i.ToString() + j.ToString()) as Button;

                            a.Click += new RoutedEventHandler(button_Click);////
                            //a.MouseRightButtonDown += new MouseButtonEventHandler(button1_MouseRightButtonDown);
                        }
                        else if (m_NodeList[i, j].m_Around == 1)
                        {
                            Button a = LogicalTreeHelper.FindLogicalNode(this, "bt" + i.ToString() + j.ToString()) as Button;

                            //ImageBrush im = new ImageBrush();
                            //im.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/1.jpg", UriKind.Absolute));
                            //a.Background = im;

                            a.Click += new RoutedEventHandler(button_Click);////
                            //a.MouseRightButtonDown += new MouseButtonEventHandler(button1_MouseRightButtonDown);
                        }
                        else if (m_NodeList[i, j].m_Around == 2)
                        {
                            Button a = LogicalTreeHelper.FindLogicalNode(this, "bt" + i.ToString() + j.ToString()) as Button;

                            //ImageBrush im = new ImageBrush();
                            //im.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/2.jpg", UriKind.Absolute));
                            //a.Background = im;

                            a.Click += new RoutedEventHandler(button_Click);////
                            //a.MouseRightButtonDown += new MouseButtonEventHandler(button1_MouseRightButtonDown);
                        }
                        else if (m_NodeList[i, j].m_Around == 3)
                        {
                            Button a = LogicalTreeHelper.FindLogicalNode(this, "bt" + i.ToString() + j.ToString()) as Button;

                           // ImageBrush im = new ImageBrush();
                          //  im.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/3.jpg", UriKind.Absolute));
                           // a.Background = im;

                            a.Click += new RoutedEventHandler(button_Click);////
                            //a.MouseRightButtonDown += new MouseButtonEventHandler(button1_MouseRightButtonDown);
                        }
                        else if (m_NodeList[i, j].m_Around == 4)
                        {

                            Button a = LogicalTreeHelper.FindLogicalNode(this, "bt" + i.ToString() + j.ToString()) as Button;

                          //  ImageBrush im = new ImageBrush();
                          //  im.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/4.jpg", UriKind.Absolute));
                          //  a.Background = im;

                            a.Click += new RoutedEventHandler(button_Click);////
                            //a.MouseRightButtonDown += new MouseButtonEventHandler(button1_MouseRightButtonDown);
                        }
                        //                        else if (m_NodeList[i][j].m_Around == 5)
                        //                        {
                        //                            
                        //                        }
                        //                        else if (m_NodeList[i][j].m_Around == 6)
                        //                        {
                        //                           
                        //                        }
                        //                        else if (m_NodeList[i][j].m_Around == 7)
                        //                        {
                        //                           
                        //                        }
                        //                        else if (m_NodeList[i][j].m_Around == 8)
                        //                        {
                        //                          
                        //                        }
                    }
                }
        }

        private void leftButton(int i, int j) 
        {
            if (i < 0 || j < 0 || i >= row || j >= col || m_NodeList[i, j].m_Type == true)
                return;
            if (m_NodeList[i, j].m_Around != 0) 
            {
                if (m_NodeList[i, j].m_IsUsed == false)
                {
                    Button a = LogicalTreeHelper.FindLogicalNode(this, "bt" + i.ToString() + j.ToString()) as Button;
                    a.Background = null;
                    a.Content = m_NodeList[i, j].m_Around;
                    return;
                }
                else
                    return;
            }
            if (m_NodeList[i, j].m_Around == 0 && m_NodeList[i, j].m_IsUsed == false) 
            {
                Button a = LogicalTreeHelper.FindLogicalNode(this, "bt" + i.ToString() + j.ToString()) as Button;
                a.Background = null;
                m_NodeList[i, j].m_IsUsed = true;
                leftButton(i - 1, j - 1);
                leftButton(i - 1, j);
                leftButton(i - 1, j + 1);
                leftButton(i, j - 1);
                leftButton(i - 1, j + 1);
                leftButton(i + 1, j - 1);
                leftButton(i + 1, j);
                leftButton(i + 1, j + 1);
            }
        }

        DispatcherTimer dt = null;
        public int ss = 0;
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt. Tick += new EventHandler(dt_Tick);
            dt.Start();
        }
       
        private void dt_Tick(object sender, EventArgs e)
        {
         
            this.TT.Text = (++ss).ToString();
           
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e) 
        {

        }
        public void button_Click(object sender, RoutedEventArgs e) 
        {
           // OnPaint();
            var button = sender as Button;
            var buttonName = button.Name;
            int i = Convert.ToInt32(button.Name.Substring(2, 1));
            int j = Convert.ToInt32(button.Name.Substring(3, 1));
            if (m_NodeList[i, j].m_Type == true)
            {
                ImageBrush im = new ImageBrush();
                im.ImageSource = new BitmapImage(new Uri("Image/Lei.jpg", UriKind.Relative));
                button.Background = im;
                MessageBox.Show("不好意思，你输了，祝你下次走运!!!", "扫雷", MessageBoxButton.OK, MessageBoxImage.Stop);
                InitializeComponent();
                DrawMap();
                buLei();
                OnPaint();
            }
            else if (m_NodeList[i, j].m_Around != 0) 
            {
                button.Background = null;
                button.Content = m_NodeList[i, j].m_Around;
            }
            else if (m_NodeList[i, j].m_Around == 0) 
            {
                leftButton(i, j);
            }
        }
    }
}
