using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Kyle Watson
// 8/11/2021
namespace MyLists
{
    public partial class FormLists : Form
    {
        public FormLists()
        {
            InitializeComponent();
        }
        List<string> ColourList = new List<string>() { "1FBK-235", "1CKR-085", "1GEU-069", "1YOB-758", "1KAP-084", "1APR-016", "3DYX-773" };
        private void DisplayList()
        {
            listBoxDisplay.Items.Clear();
            ColourList.Sort();
            foreach (var color in ColourList)
            {
                listBoxDisplay.Items.Add(color);
            }
        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            ColourList.Sort();
            if (ColourList.BinarySearch(textBoxInput.Text) >= 0)
                MessageBox.Show("found");
            else
                MessageBox.Show("Not Found");
            textBoxInput.Clear();
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxInput.Text != string.Empty)
            {
                ColourList.Add(textBoxInput.Text);
                DisplayList();
                textBoxInput.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a valid rego plate.");
            }
        }        
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            listBoxDisplay.SetSelected(listBoxDisplay.SelectedIndex, true);
            ColourList.RemoveAt(listBoxDisplay.SelectedIndex);
            DisplayList();
        }
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            string fileName = "Rainbow.bin";
            OpenFileDialog OpenBinary = new OpenFileDialog();
            DialogResult sr = OpenBinary.ShowDialog();
            if (sr == DialogResult.OK)
            {
                fileName = OpenBinary.FileName;
            }
            try
            {
                ColourList.Clear();
                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    while (stream.Position < stream.Length)
                    {
                        ColourList.Add((string)binaryFormatter.Deserialize(stream));
                    }
                }
                DisplayList();
            }
            catch (IOException)
            {
                MessageBox.Show("cannot open file");
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            string fileName = "Rainbow.bin";
            SaveFileDialog saveBinary = new SaveFileDialog();
            DialogResult sr = saveBinary.ShowDialog();
            if(sr == DialogResult.Cancel)
            {
                saveBinary.FileName = fileName;
            }
            if(sr == DialogResult.OK)
            {
                fileName = saveBinary.FileName;
            }
            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Create))
                {
                    BinaryFormatter binFormatter = new BinaryFormatter();
                    foreach (var item in ColourList)
                    {
                        binFormatter.Serialize(stream, item);
                    }
                }
            }
            catch (IOException)
            {
                MessageBox.Show("cannot save file");
            }
        }
        private void FormLists_Load(object sender, EventArgs e)
        {
            DisplayList();
        }
    }
}
