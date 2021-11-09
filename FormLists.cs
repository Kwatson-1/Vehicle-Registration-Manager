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
        List<string> RegoList = new List<string>();
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
            textBoxInput.Focus();
        }
        #endregion
        #region Delete
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            bool isEmpty = !RegoList.Any();
            if (listBoxDisplay.SelectedIndex != -1)
            {
                listBoxDisplay.SetSelected(listBoxDisplay.SelectedIndex, true);
                RegoList.RemoveAt(listBoxDisplay.SelectedIndex);
                statusStrip.Text = "Item deleted successfully.";
                DisplayList();
                textBoxInput.Clear();
                textBoxInput.Focus();

            }
            else if (isEmpty)
            {
                statusStrip.Text = "There are currently no items in the list to delete.";
            }
            else
            {
                statusStrip.Text = "Please select a valid item from the list box.";
            }
        }
        #endregion
        #region Open
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            string fileName = "demo_00.txt";
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
        #endregion
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

        private void listBoxDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBoxDisplay.SelectedIndex != -1)
            {
                textBoxInput.Text = listBoxDisplay.SelectedItem.ToString();
                textBoxInput.Select();
            }
        }
        #region Double click delete
        private void listBoxDisplay_DoubleClick(object sender, EventArgs e)
        {
            bool isEmpty = !RegoList.Any();
            DialogResult dialogResult = MessageBox.Show("Do you want to delete this item?", "Delete Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (listBoxDisplay.SelectedIndex != -1)
                {
                    listBoxDisplay.SetSelected(listBoxDisplay.SelectedIndex, true);
                    RegoList.RemoveAt(listBoxDisplay.SelectedIndex);
                    statusStrip.Text = "Item deleted successfully.";
                    DisplayList();
                    textBoxInput.Clear();
                    textBoxInput.Focus();

                }
                else if (isEmpty)
                {
                    statusStrip.Text = "There are currently no items in the list to delete.";
                }
                else
                {
                    statusStrip.Text = "Please select a valid item from the list box.";
                }
            }
            else if (dialogResult == DialogResult.No)
            {

            }

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            String NewValue = textBoxInput.Text;
            int RegoIndex = listBoxDisplay.SelectedIndex;
            RegoList[RegoIndex] = NewValue;
            DisplayList();
        }
        #endregion
        //Path.GetFileNameWithoutExtensions(currentFileName);
        //string strnumy = currentFileName.Remove(0,5)
        //int num = int.Parse(strnumy)
        //num++
        //String newValue
        //if(num < 9)
        //newValue = "0" + num.ToString();
        //else
        //newValue = num.ToString();
        //String newfilename = "demo_" + newValue + ".txt"
        //SaveTextFile(newFileName)

        //path.getdirectoryname(application.executablePath);
    }

}
