using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BubbleEffect
{
    public partial class ClickEffect : Component
    {
        [Browsable(true)]
        public Control ClickControl 
        { 
            get{return _ClickControl;}
            set{_ClickControl = value; RegisterControl();}
        }
        private Control _ClickControl;
        private int step = 0;
        private Point startPoint;
        public ClickEffect()
        {
            InitializeComponent();
           
        }

        public ClickEffect(IContainer container)
        {
            container.Add(this);
            InitializeComponent();


        }
        [Browsable(true)]
        private void  RegisterControl()
        {
            _ClickControl.Paint += new PaintEventHandler(control_Paint);
            _ClickControl.Click += new EventHandler(control_Click);
            SetDoubleBuffered(_ClickControl);
        }

        void effectTimer_Tick(object sender, EventArgs e)
        {
            step++;
            if (_ClickControl != null)
            {
                _ClickControl.Invalidate();
            }
            if (  startPoint.X < step * 5  && startPoint.Y < step * 5  &&  _ClickControl.Width  < step * 5 && _ClickControl.Height  < step * 5) 
            {
                this.effectTimer.Enabled = false;
                step = 0;
            }
        }
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }
     

        void control_Click(object sender, EventArgs e)
        {
            this.effectTimer.Enabled = true;
            startPoint = _ClickControl.PointToClient(Cursor.Position);
            step = 0;
        }

        void control_Paint(object sender, PaintEventArgs e)
        {
            using (Brush brush = new SolidBrush(Color.FromArgb(128, 255, 255, 255)))
            {
                e.Graphics.FillEllipse(brush, startPoint.X - (step * 5) , startPoint.Y -( step*5) , step*10, step*10);
            }
        }
    }
}
