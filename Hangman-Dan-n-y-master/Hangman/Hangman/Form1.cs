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

        string word = "";
        List<Label> myLabels = new List<Label>();
        int wrongGuesses = 0;


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
            drawPost();
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
            word = RandomWord();
            char[] chars = word.ToCharArray();
            int Between = 330 / chars.Length;
            for (int i = 0; i < chars.Length; i++)
            {
                myLabels.Add(new Label());
                myLabels[i].Location = new Point((i * Between) + 10, 80);
                myLabels[i].Text = "_";
                myLabels[i].Parent = groupBox2;
                myLabels[i].BringToFront();
                myLabels[i].CreateControl();

            }
            label1.Text = "Woord Lengte: " + word.Length.ToString();
        }

        string RandomWord()
        {
            string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "words.txt");
            Random rand = new Random();
            return lines[rand.Next(lines.Length)];
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            MakeStripes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("You are required to enter a letter.");
                return;
            }

            char letterInput = textBox1.Text.ToLower().ToCharArray()[0];
            if(!char.IsLetter(letterInput))
            {
                MessageBox.Show("You can only submit letters.");
                return;
            }
            if(word.Contains(letterInput))
            {
                char[] letters = word.ToCharArray();
                for(int i = 0; i < letters.Length; i++)
                {
                    if (letters[i] == letterInput)
                    {
                        myLabels[i].Text = letterInput.ToString();
                    }

                }
                foreach (Label l in myLabels)
                {
                    if (l.Text == "_")
                    {
                        return;
                    }
                }
                winGame();
            }
            else
            {
                MessageBox.Show("letter is not in the word");
                label2.Text += " " + letterInput + ",";
                wrongGuess();
            }
        }


        void resetGame()
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(panel1.BackColor);
            MakeStripes();
            drawPost();
            wrongGuesses = 0;

        }

        void winGame()
        {
            MessageBox.Show("you won");
            resetGame();
        }

        void wrongGuess()
        {
            drawnBodyParts((bodyParts)wrongGuesses);
            wrongGuesses++;
            if (wrongGuesses == Enum.GetNames(typeof(bodyParts)).Length)
            {
                MessageBox.Show("you lost, resetting.");
                resetGame();
            }
        }

        void drawPost()
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Black, 10);

            g.DrawLine(p, new Point(130, 218), new Point(130, 5));
            g.DrawLine(p, new Point(135, 5), new Point(65, 5));
            g.DrawLine(p, new Point(60, 0), new Point(60, 50));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text == "")
            {
                MessageBox.Show("You are required to enter a letter.");
                return;
            }
            if(textBox2.Text == word)
            {
                winGame();
            }
            else
            {
                wrongGuess();
                MessageBox.Show("wrong");
            }
        }
    }
}
