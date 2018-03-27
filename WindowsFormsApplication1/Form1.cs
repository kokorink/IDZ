using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;



namespace WindowsFormsApplication1
{
  
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          

        }
        [Serializable]

        public struct RingSquare
        {
            public double r;
            public double a;
            public double s;
        }

        RingSquare ring;

        private void button1_Click(object sender, EventArgs e)
        {
            
            ring.r = Convert.ToDouble(textBoxRadius.Text);
            ring.a = Convert.ToDouble(textBoxAngle.Text);
            ring.s = Math.PI * Math.Pow(ring.r, 2) * (1 - ring.a / 360);
            textBoxResult.Text = Convert.ToString(ring.s);
            richTextBoxFile.Clear();
        }
        
        private void toolStripMenuItemSaveJSON_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog.OpenFile()) != null)
                {

                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(RingSquare));
                    jsonSerializer.WriteObject(myStream, ring);
                    myStream.Close();
                }
            }
        }
        
        private void toolStripMenuItemSaveXML_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog.OpenFile()) != null)
                {
                    XmlSerializer xmlFormatter = new XmlSerializer(typeof(RingSquare));
                    xmlFormatter.Serialize(myStream, ring);
                    myStream.Close();
                }
            }
        }
    
        private void loadJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myFile = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myFile = openFileDialog.OpenFile()) != null)
                    {
                        using (myFile)
                        {
                            richTextBoxFile.Text = File.ReadAllText(openFileDialog.FileName);
                            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RingSquare));
                            ring = (RingSquare)ser.ReadObject(myFile);
                        }
                        textBoxRadius.Text = Convert.ToString(ring.r);
                        textBoxAngle.Text = Convert.ToString(ring.a);
                        textBoxResult.Text = Convert.ToString(ring.s);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void loadXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myFile = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            openFileDialog.RestoreDirectory = true;
            //string myFilePath = openFileDialog.FileName;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myFile = openFileDialog.OpenFile()) != null)
                    {
                        using (myFile)
                        {
                            richTextBoxFile.Text = File.ReadAllText(openFileDialog.FileName);
                            XmlSerializer f = new XmlSerializer(typeof(RingSquare));
                            ring = (RingSquare)f.Deserialize(myFile);
                        }
                        textBoxRadius.Text = Convert.ToString(ring.r);
                        textBoxAngle.Text = Convert.ToString(ring.a);
                        textBoxResult.Text = Convert.ToString(ring.s);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
