using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorseWinForm
{
    public partial class Form1 : Form
    {
        public bool winner = false;

        public int maxProgress = 100;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label5.Text = "Race has begun!";

            List<ProgressBar> progressBars = new List<ProgressBar>() { progressBar1, progressBar2, progressBar3, progressBar4 };

            foreach (ProgressBar bar in progressBars)
            {
                bar.Maximum = maxProgress;
            }

            List<Horse> horses = new List<Horse>() { new Horse(maxProgress), new Horse(maxProgress), new Horse(maxProgress), new Horse(maxProgress) };

            var HorseTasks = new List<Task> { new Task(horses[0].Advance), new Task(horses[1].Advance), new Task(horses[2].Advance), new Task(horses[3].Advance) };       

            foreach (Task task in HorseTasks)
            {
                task.Start();
            }

            while (true)
            {              
                for (int i = 0; i < horses.Count; i++)
                {
                    progressBars[i].Value = horses[i].Progress;                            
                }       

                if(horses[0].Progress == maxProgress || horses[1].Progress == maxProgress || horses[2].Progress == maxProgress || horses[3].Progress == maxProgress)
                {
                    foreach(Horse horse in horses)
                    {
                        horse.run = false;
                    }
                    break;

                }
            }

            //wait for tasks to finish
            foreach (Task task in HorseTasks)
            {
                task.Wait();
            }

            //look for winners
            string winners = string.Empty;

            for (int i = 0; i < horses.Count; i++)
            {
               progressBars[i].Value = horses[i].Progress;

                if (horses[i].Progress == maxProgress)
                {
                    winners += (i + 1).ToString() + ',';
                }

            }


            label5.Text = "The winner is horse:" + winners;

        }
        public class Horse
        {
            public int maxProgress;

            public int Progress;

            public bool run;

            Random random = new Random();
            public Horse(int Max) { Progress = 0; run = true; maxProgress = Max; }
            public void Advance()
            {

                while (run)
                {
                    if (Progress == maxProgress)
                    {
                        break;
                    }

                    if (random.Next(1, 1001) % 1000 == 0)
                    {
                        Progress += 1;
                    }

                }

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }

}


