using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace analog_clock
{
    public partial class Form1 : Form
    {
        Graphics clock;
        Point hour_center;
        double radius = 12.00;
        int hour_length = 5;
        int min_length = 3;
        int sec_length = 2;

        public Form1()
        {
            InitializeComponent();
            this.Text = "머시계";
            clock = this.clock_panel.CreateGraphics();
            init_clock();
            timer_setting();
            
        }

        private void init_clock()
        {
            this.hour_center = new Point(this.clock_panel.Width / 2, this.clock_panel.Height / 2);
            radius = clock_panel.Height / 2;
            hour_length = (int)(radius * 0.6);
            min_length = (int)(radius * 0.3);
            sec_length = (int)(radius * 0.2);
        }

        private void timer_setting ()
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000;
            timer1.Tick += time_act;
            timer1.Start();
             
            
        }

        private void time_act(object s, EventArgs e)
        {
            DateTime now = DateTime.Now;
            double hour_r = (now.Hour % 12.00 + now.Minute / 60.00) * 30 * Math.PI / 180.00;
            double min_r = (now.Minute + now.Second / 60) * 6 * Math.PI / 180.00;
            double sec_r = (now.Second) * 6 * Math.PI / 180;
            clock_panel.Refresh();
            draw_clock(hour_r, min_r, sec_r);
        }

        private void draw_clock(double h, double m, double s)
        {
            Point next_point;
            Pen pen = new Pen(Brushes.Yellow, 10);
            Rectangle r = new Rectangle(hour_center.X - (int)(radius / 2) - 100, hour_center.Y - (int)(radius / 2) - 100, (int)radius + 200, (int)radius + 200);
            clock.DrawEllipse(pen, r);
            next_point = draw_line((int)(hour_length * Math.Sin(h)), (int)(-hour_length * Math.Cos(h)), 0, 0, Brushes.Red, 8, hour_center.X, hour_center.Y);

            pen = new Pen(Brushes.YellowGreen, 7);
            r = new Rectangle(next_point.X - (int)(radius / 2), next_point.Y - (int)(radius / 2), (int)radius, (int)radius);
            clock.DrawEllipse(pen, r);
            next_point = draw_line((int)(min_length * Math.Sin(m)), (int)(-min_length * Math.Cos(m)),
                0, 0,
                Brushes.Green, 3,
                next_point.X, next_point.Y);

            pen = new Pen(Brushes.DeepSkyBlue, 5);
            r = new Rectangle(next_point.X - 50, next_point.Y - 50, 100 , 100);
            clock.DrawEllipse(pen, r);
            draw_line((int)(sec_length * Math.Sin(s)), (int)(-sec_length * Math.Cos(s)),
                0, 0,
                Brushes.Blue, 2,
                next_point.X, next_point.Y);
        }

        private Point draw_line(int x1, int y1, int x2, int y2, Brush sex, int thick, int cx, int cy)
        {
            Pen pen = new Pen(sex, thick);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Point p1 = new Point(x1 + cx, y1 + cy);
            Point p2 = new Point(x2 + cx, y2 + cy);
            clock.DrawLine(pen, p1, p2);
            return p1;
        }

        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
