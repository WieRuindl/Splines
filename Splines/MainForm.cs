using System;
using System.Drawing;
using System.Windows.Forms;

namespace Splines
{
    public partial class MainForm : Form
    {
        private CSpline _model;

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var random = new Random();

            var points = new CPoint[(int)numericUpDown1.Value];
            var interval = (pictureBox1.Width - 20) / (int)numericUpDown1.Value;

            for (var i = 0; i < (int)numericUpDown1.Value; i++)
            {
                points[i] = new CPoint(random.Next(10 + interval * i, 10 + interval * (i + 1)),
                random.Next(10, pictureBox1.Height - 10));
            }

            _model = new CSpline(points);

            vScrollBar1.Value = 0;
            vScrollBar2.Value = 0;

            SetD1ToModel();
            GetDerivatesFromModel();
            Draw();
        }

        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            SetD1ToModel();
            GetDerivatesFromModel();
            Draw();
        }

        private void SetD1ToModel()
        {
            _model.Df1 = (double)vScrollBar1.Value / 1000;
            _model.Dfn = (double)vScrollBar2.Value / 1000;
            _model.GenerateSplines();
        }

        private void GetDerivatesFromModel()
        {
            textBox_df1.Text = $"{-_model.Df1:0.0000}";
            textBox_dfn.Text = $"{-_model.Dfn:0.0000}";
            textBox_ddf1.Text = $"{-_model.Ddf1:0.0000}";
            textBox_ddfn.Text = $"{-_model.Ddfn:0.0000}"; 
        }

        private void Draw()
        {
            var bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            
            var canvas = Graphics.FromImage(bmp);

            _model.Draw(canvas);

            pictureBox1.Image = bmp;
        }
    }
}
