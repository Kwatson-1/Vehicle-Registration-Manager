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
        List<string> RegoList = new List<string>() { "1FBK-235", "1CKR-085", "1GEU-069", "1YOB-758", "1KAP-084", "1APR-016", "3DYX-773" };
        private void DisplayList()
        {
            listBoxDisplay.Items.Clear();
            RegoList.Sort();
            foreach (var color in RegoList)
            {
                listBoxDisplay.Items.Add(color);
            }
        }
        #region Search
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            RegoList.Sort();
            if (RegoList.BinarySearch(textBoxInput.Text) >= 0)
                MessageBox.Show("found");
            else
                MessageBox.Show("Not Found");
            textBoxInput.Clear();
        }
        #endregion
        #region Add
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxInput.Text != string.Empty)
            {
                RegoList.Add(textBoxInput.Text);
                DisplayList();
                textBoxInput.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a valid rego plate.");
            }
        }
        #endregion
        #region Delete
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            listBoxDisplay.SetSelected(listBoxDisplay.SelectedIndex, true);
            RegoList.RemoveAt(listBoxDisplay.SelectedIndex);
            DisplayList();
        }
        #endregion
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            string fileName = "demo_nn.txt";
            OpenFileDialog OpenBinary = new OpenFileDialog();
            DialogResult sr = OpenBinary.ShowDialog();
            if (sr == DialogResult.OK)
            {
                fileName = OpenBinary.FileName;
            }
            try
            {
                RegoList.Clear();
                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    while (stream.Position < stream.Length)
                    {
                        RegoList.Add((string)binaryFormatter.Deserialize(stream));
                    }
                }
                DisplayList();
            }
            catch (IOException)
            {
                MessageBox.Show("cannot open file");
            }
        }
        #region Save
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
                    foreach (var item in RegoList)
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
        #endregion
        private void FormLists_Load(object sender, EventArgs e)
        {
            DisplayList();
        }
    }
}
