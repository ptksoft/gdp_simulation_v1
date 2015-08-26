using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GDP_Simulation_Demo_V1
{
    public partial class frmMain : Form
    {
        int sx = 50;    // Start X
        int sy = 50;    // Start Y
        int ph = 20;    // Panel Height
        int pw = 5;     // Panel Width
        Panel[] pn = new Panel[220];    // Panel

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            PrepareGDP();
            this.Refresh();
        }

        private void PrepareGDP()
        {
            pw = (this.ClientSize.Width) / 210;

            //MessageBox.Show("Width=" + w.ToString());
                        
            for (int i = 0; i < 220; i++)
            {
                pn[i] = new Panel();
                pn[i].BorderStyle = BorderStyle.FixedSingle;
                //pn[i].BackColor = Color.FromArgb(i, Math.Abs(220-i), 0);
                pn[i].BackColor = Color.FromArgb(25+i, 255-i, 0);
                pn[i].Size = new Size(pw, ph);
                pn[i].Location = new Point(sx + (i * pw), sy);
                this.Controls.Add(pn[i]);
            }

            // Scale Label
            for (int i = 0; i < 22; i++)
            {
                Label lb = new Label();
                lb.Text = i.ToString();
                lb.Location = new Point(sx + ((i * 10) * pw), sy - 20);
                lb.AutoSize = true;
                this.Controls.Add(lb);
            }

            // Sensor            
            ShowSensor("S1-M", 0);      // S1 - Microwave
            ShowSensor("S2-V", 20);      // S2 - Video
            ShowSensor("S3-V", 40);      // S3 - Video
            ShowSensor("S4-V", 90);      // S4 - Video
            ShowSensor("S5-V", 160);      // S5 - Video
            ShowSensor("S6-V", 180);      // S6 - Video
            ShowSensor("S7-V", 214);      // S7 - Microwave
        }

        private void ShowSensor(string sname, int i)
        {
            Label lb = new Label();
            lb.Text = sname;
            lb.Location = new Point(sx + (i * pw), sy + 25);
            lb.AutoSize = true;
            this.Controls.Add(lb);
        }

        private void tBar_Scroll(object sender, EventArgs e)
        {
            DateTime DT = new DateTime(2015, 1, 1, 16, 30, 0);
            DT = DT.AddMinutes(tBar.Value);
            lblTime.Text = DT.Hour.ToString() + "." + string.Format("{0:00}", DT.Minute);
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            StopPlaying();
            tBar.Value = tBar.Minimum;
            tBar_Scroll(this, new EventArgs());
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            StopPlaying();
            tBar.Value = tBar.Maximum;
            tBar_Scroll(this, new EventArgs());
        }

        private void btnPrev5_Click(object sender, EventArgs e)
        {
            StopPlaying();
            int d = tBar.Value - 5;
            if (tBar.Minimum <= d) tBar.Value = d;
            else tBar.Value = tBar.Minimum;
            tBar_Scroll(this, new EventArgs());
        }

        private void btnNext5_Click(object sender, EventArgs e)
        {
            StopPlaying();
            int d = tBar.Value + 5;
            if (tBar.Maximum >= d) tBar.Value = d;
            else tBar.Value = tBar.Maximum;
            tBar_Scroll(this, new EventArgs());
        }

        private void tmPlaying_Tick(object sender, EventArgs e)
        {
            if (tBar.Value >= tBar.Maximum)
            {
                tmPlaying.Enabled = false;
            }
            else
            {
                tBar.Value += 1;
                tBar_Scroll(this, new EventArgs());
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (tmPlaying.Enabled)
            {
                tmPlaying.Stop();
                btnPlay.Text = "Play";
            }
            else
            {
                tmPlaying.Start();
                btnPlay.Text = "Stop";
            }
        }

        private void StopPlaying()
        {
            tmPlaying.Stop();
            btnPlay.Text = "Play";
        }
    }
}
