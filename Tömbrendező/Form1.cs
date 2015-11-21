using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tömbrendező
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Timer = new Timer();
            Timer.Tick += delegate
            {
                startbtn.Enabled = !(Timer.Enabled = Enum.MoveNext()); //Ha végzett, engedélyezi a Start gombot, az időzítőt pedig leállítja
            };
        }

        private List<NumericUpDown> NumUpdowns = new List<NumericUpDown>();
        private void countNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumUpdowns.ForEach(a => this.Controls.Remove(a));
            NumUpdowns.Clear();
            var rand = new Random();
            for (int i = 0; i < countNumUpDown.Value; i++)
            {
                var numupdown = new NumericUpDown();
                numupdown.Maximum = 1000;
                numupdown.Value = rand.Next((int)numupdown.Minimum, (int)numupdown.Maximum);
                numupdown.Location = new Point(10 + i * (numupdown.Width + 10), 50);
                numupdown.BringToFront();
                NumUpdowns.Add(numupdown);
                this.Controls.Add(numupdown);
            }
        }

        private IEnumerator Enum;
        private Timer Timer;
        private void startbtn_Click(object sender, EventArgs e)
        {
            /*foreach (bool b in Rendezés())
                ;*/
            /*if (Enum == null || !Enum.MoveNext())
                Enum = Rendezés().GetEnumerator();*/
            Enum = Rendezés().GetEnumerator();
            Timer.Interval = (int)numericUpDown1.Value / 10;
            Timer.Start();
        }

        /// <summary>
        /// Gyorsított buborékos rendezés
        /// </summary>
        /// <returns></returns>
        private IEnumerable Rendezés()
        {
            Point p;
            NumUpdowns.ForEach(numupdown => numupdown.ReadOnly = true);
            countNumUpDown.ReadOnly = true;

            HighlightCode("int i = T.Length - 1;");
            int i = NumUpdowns.Count - 1;
            labeli.Text = i.ToString();
            labeli.ForeColor = Color.Red;
            for (int x = 0; x < 10; x++)
                yield return null;
            labeli.ForeColor = Color.Black;
            HighlightCode("while (i >= 1)");
            for (int x = 0; x < 10; x++)
                yield return null;
            while (i >= 1)
            {
                HighlightCode("int cs = 0;");
                int cs = 0;
                labelcs.Text = cs.ToString();
                labelcs.ForeColor = Color.Red;
                for (int x = 0; x < 10; x++)
                    yield return null;
                labelcs.ForeColor = Color.Black;
                HighlightCode("for (int j = 0; j < i; j++)");
                for (int j = 0; j < i; j++)
                {
                    labelj.Text = j.ToString();
                    labelj.ForeColor = Color.Red;
                    for (int x = 0; x < 10; x++)
                        yield return null;
                    labelj.ForeColor = Color.Black;
                    labelcs.ForeColor = Color.Black;
                    HighlightCode("if (T[j] > T[j + 1])");
                    if (NumUpdowns[j].Value > NumUpdowns[j + 1].Value)
                    {
                        int xtáv = 100 - NumUpdowns[j].Location.X;
                        int ytáv = 280 - NumUpdowns[j].Location.Y;
                        Point eredetipoz = NumUpdowns[j].Location;
                        HighlightCode("int csere = T[j];");
                        labelcsere.Text = NumUpdowns[j].Value.ToString(); //==csere
                        labelcsere.ForeColor = Color.Red;
                        for (int k = 0; k < 10; k++)
                        {
                            p = NumUpdowns[j].Location;
                            p.Offset(xtáv / 10, ytáv / 10);
                            NumUpdowns[j].Location = p;
                            yield return null;
                        }
                        NumUpdowns[j].Location = new Point(100, 280);
                        labelcsere.ForeColor = Color.Black;
                        HighlightCode("T[j] = T[j + 1];");
                        for (int k = 0; k < 10; k++)
                        {
                            p = NumUpdowns[j + 1].Location;
                            p.Offset(0, 5);
                            NumUpdowns[j + 1].Location = p;
                            yield return null;
                        }
                        xtáv = eredetipoz.X - NumUpdowns[j + 1].Location.X;
                        ytáv = eredetipoz.Y - NumUpdowns[j + 1].Location.Y;
                        for (int k = 0; k < 10; k++)
                        {
                            p = NumUpdowns[j + 1].Location;
                            p.Offset(xtáv / 10, ytáv / 10);
                            NumUpdowns[j + 1].Location = p;
                            yield return null;
                        }
                        HighlightCode("T[j + 1] = csere;");
                        xtáv = eredetipoz.X + NumUpdowns[j + 1].Width + 10 - NumUpdowns[j].Location.X;
                        ytáv = eredetipoz.Y - NumUpdowns[j].Location.Y;
                        for (int k = 0; k < 10; k++)
                        {
                            p = NumUpdowns[j].Location;
                            p.Offset(xtáv / 10, ytáv / 10);
                            NumUpdowns[j].Location = p;
                            yield return null;
                        }
                        NumUpdowns[j].Location = new Point(eredetipoz.X + NumUpdowns[j + 1].Width + 10, eredetipoz.Y);

                        var csere = NumUpdowns[j];
                        NumUpdowns[j] = NumUpdowns[j + 1];
                        NumUpdowns[j + 1] = csere;
                        yield return null;

                        HighlightCode("cs = j;");
                        cs = j;
                        labelcs.Text = cs.ToString();
                        labelcs.ForeColor = Color.Red;
                    }
                }
                for (int x = 0; x < 10; x++)
                    yield return null;
                labelcs.ForeColor = Color.Black;
                HighlightCode("i = cs;");
                i = cs;
                labeli.Text = i.ToString();
                labeli.ForeColor = Color.Red;
                for (int x = 0; x < 10; x++)
                    yield return null;
                labeli.ForeColor = Color.Black;
            }
            NumUpdowns.ForEach(numupdown => numupdown.ReadOnly = false);
            countNumUpDown.ReadOnly = false;
            richTextBox1.SelectAll();
            richTextBox1.SelectionColor = richTextBox1.ForeColor;
            richTextBox1.DeselectAll();
            labeli.Text = "";
            labelcs.Text = "";
            labelj.Text = "";
            labelcsere.Text = "";
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (Timer != null)
            {
                bool started = Timer.Enabled;
                Timer.Stop();
                Timer.Interval = (int)numericUpDown1.Value / 10;
                if (started)
                    Timer.Start();
            }
        }

        private int LastHighlightStart;
        private int LastHighlightLength;
        private void HighlightCode(string code)
        {
            richTextBox1.Select(LastHighlightStart, LastHighlightLength);
            richTextBox1.SelectionColor = richTextBox1.ForeColor;
            int start = richTextBox1.Find(code);
            LastHighlightStart = start;
            LastHighlightLength = code.Length;
            richTextBox1.Select(start, code.Length);
            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.DeselectAll();
        }

        private void pausebtn_Click(object sender, EventArgs e)
        {
            Timer.Stop();
        }

        private void resumebtn_Click(object sender, EventArgs e)
        {
            if (Enum != null)
                Timer.Start();
        }

        private Timer StepTimer;
        private int StepTimerCount;
        private void stepbtn_Click(object sender, EventArgs e)
        {
            if (StepTimer == null)
            {
                StepTimer = new Timer();
                StepTimer.Tick += delegate
                  {
                      if (!Enum.MoveNext())
                      {
                          startbtn.Enabled = true;
                          Enum = null;
                          StepTimer.Stop();
                      }
                      StepTimerCount++;
                      if (StepTimerCount >= 10)
                          StepTimer.Stop();
                  };
            }
            if (StepTimer.Enabled)
                return;
            StepTimerCount = 0;
            StepTimer.Interval = Timer.Interval;
            if (Enum == null)
                Enum = Rendezés().GetEnumerator();
            startbtn.Enabled = false;
            StepTimer.Start();
        }
    }
}
