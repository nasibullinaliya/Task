using MathNet.Numerics.LinearAlgebra;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Task
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = ReadFileContent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WriteToFile(textBox2.Text);
        }

        private string ReadFileContent()
        {
            var fileContent = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            return fileContent;
        }

        private void WriteToFile(string results)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    var writer = new StreamWriter(myStream);
                    writer.Write(results);
                    writer.Close();
                    myStream.Close();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();

            var transmitter = new Transmitter();
            var receiver1 = new Receiver();
            var receiver2 = new Receiver();
            var receiver3 = new Receiver();

            try
            {
                ParseInput(receiver1, receiver2, receiver3);
            }
            catch (Exception)
            {
                label1.Text = "Неверный формат данных";
                return;
            }

            transmitter.GetTransmitterTrack(receiver1, receiver2, receiver3);
            WriteOutput(transmitter);

            DrawChart(transmitter);
        }

        private void ParseInput(Receiver receiver1, Receiver receiver2, Receiver receiver3)
        {
            try
            {
                var input = textBox1.Text;

                StringReader strReader = new StringReader(input);
                var line = strReader.ReadLine();

                var xy = line.Split(',').Select(x => float.Parse(x)).ToArray();

                receiver1.X = xy[0];
                receiver1.Y = xy[1];
                receiver2.X = xy[2];
                receiver2.Y = xy[3];
                receiver3.X = xy[4];
                receiver3.Y = xy[5];

                line = strReader.ReadLine();
                while (line != null)
                {
                    xy = line.Split(',').Select(x => float.Parse(x)).ToArray();

                    receiver1.S.Add(xy[0] * 1000000);//dt*1000000m/s
                    receiver2.S.Add(xy[1] * 1000000);
                    receiver3.S.Add(xy[2] * 1000000);

                    line = strReader.ReadLine();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void DrawChart(Transmitter transmitter)
        {
            var n = transmitter.Track.Count;
            for (var i = 0; i < n; i++)
            {
                chart1.Series[0].Points.AddXY(transmitter.Track[i].X, transmitter.Track[i].Y);
            }
        }

        private void WriteOutput(Transmitter transmitter)
        {
            var resultLines = new StringBuilder(); 
            var count = transmitter.Track.Count;
            for(int i = 0; i < count; i++)
            {
                resultLines.AppendLine(string.Concat(transmitter.Track[i].X, ",", transmitter.Track[i].Y));
            }
            textBox2.Text = resultLines.ToString();
        }
    }
}
