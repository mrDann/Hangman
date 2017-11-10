using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Hangman
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //string word = "";
        List<Label> Labels = new List<Label>();
        


        enum bodyParts
        {
            Head,
            Left_eye,
            Right_eye,
            Mouth,
            Right_arm,
            Left_arm,
            body,
            Right_leg,
            Left_leg
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Black, 10);


            g.DrawLine(p, new Point(130, 218), new Point(130, 5));
            g.DrawLine(p, new Point(135, 5), new Point(65, 5));
            g.DrawLine(p, new Point(60, 0), new Point(60, 50));
            drawnBodyParts(bodyParts.Left_eye);
            drawnBodyParts(bodyParts.Right_eye);
            drawnBodyParts(bodyParts.Mouth);
            drawnBodyParts(bodyParts.Left_leg);
            drawnBodyParts(bodyParts.Right_leg);
            drawnBodyParts(bodyParts.Left_arm);
            drawnBodyParts(bodyParts.Right_arm);
            drawnBodyParts(bodyParts.Head);
            drawnBodyParts(bodyParts.body);
            MessageBox.Show(RandomWord());


        }

    

        void drawnBodyParts(bodyParts bp) {

            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.DarkSalmon, 2);

            if (bp == bodyParts.Head)
                g.DrawEllipse(p, 40, 50, 40, 40);
            else if (bp == bodyParts.Left_eye)
            {
                SolidBrush s = new SolidBrush(Color.Red);
                g.FillEllipse(s, 50, 60, 5, 5);
            } else if (bp == bodyParts.Right_eye)
            {
                SolidBrush s = new SolidBrush(Color.Red);
                g.FillEllipse(s, 63, 60, 5, 5);
            } else if (bp == bodyParts.Mouth)
            {
                g.DrawArc(p, 50, 60, 20, 20, 45, 100);
            } else if (bp == bodyParts.body)
            {
                g.DrawLine(p, new Point(60, 90), new Point(60, 170));
            } else if (bp == bodyParts.Left_arm)
            {
                g.DrawLine(p, new Point(60, 100), new Point(30, 85));
            }
            else if (bp == bodyParts.Right_arm)
            {
                g.DrawLine(p, new Point(60, 100), new Point(90, 85));

            } else if (bp == bodyParts.Left_leg) {
                g.DrawLine(p, new Point(60, 170), new Point(30, 190));
            }
            else if (bp == bodyParts.Right_leg)
            {
                g.DrawLine(p, new Point(60, 170), new Point(90, 190));
            }
        }

        void MakeStripes()
        {
            var word = RandomWord();
            char[] chars = word.ToCharArray();
            int Between = 330 / chars.Length - 1;
            for (int i = 0; i < chars.Length - 1; i ++)
            {
                Labels.Add(new Label());
                Labels[i].Location = new Point((i = Between) + 10, 80);
                Labels[i].Text = "_";
                Labels[i].Parent = groupBox2;
                Labels[i].BringToFront();
                Labels[i].CreateControl();

            }


        }

        string RandomWord()
        {
            string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "words.txt"); //i hope that the file is not too big
            Random rand = new Random();
            return lines[rand.Next(lines.Length)];
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

     
    }
}
