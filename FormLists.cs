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
// Vehicle Registration Manager
namespace MyLists
{
    public partial class VehicleRegistrationManager : Form
    {
        public VehicleRegistrationManager()
        {
            InitializeComponent();
        }

        List<string> RegoList = new List<string>();

        string currentFileName = "demo_00";
        #region Display List Method
        private void DisplayList()
        {
            listBoxDisplay.Items.Clear();
            RegoList.Sort();
            foreach (var rego in RegoList)
            {
                listBoxDisplay.Items.Add(rego);
            }
        }
        #endregion
        #region Binary Search
        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            RegoList.Sort();
            if (RegoList.BinarySearch(textBoxInput.Text) >= 0)
            {
                MessageBox.Show("Plate found.");
                statusStrip.Text = "Rego plate found at index: " + RegoList.BinarySearch(textBoxInput.Text);
            }
            else
            {
                statusStrip.Text = "Error: rego plate not found.";
            }
            PostFunctionUtility();

        }
        #endregion
        #region Add
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            bool alreadyExists = RegoList.Contains(textBoxInput.Text.ToUpper());
            if (!alreadyExists)
            {
                if (textBoxInput.Text != string.Empty)
                {
                    RegoList.Add(textBoxInput.Text.ToUpper());
                    statusStrip.Text = "Rego plate " + "'" + textBoxInput.Text.ToUpper() + "'" + " added successfully.";
                }
                else
                {
                    statusStrip.Text = "Error: please enter a valid rego plate.";
                }
            }
            else
            {
                statusStrip.Text = "Cannot enter a duplicate item into the list.";
            }
            PostFunctionUtility();
        }
        #endregion
        #region Delete
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            bool isEmpty = !RegoList.Any();
            if (listBoxDisplay.SelectedIndex != -1)
            {
                listBoxDisplay.SetSelected(listBoxDisplay.SelectedIndex, true);
                RegoList.RemoveAt(listBoxDisplay.SelectedIndex);
                statusStrip.Text = "Rego plate " + "'" + textBoxInput.Text.ToUpper() + "'" + " deleted successfully.";
                PostFunctionUtility();
            }
            else if (isEmpty)
            {
                statusStrip.Text = "Error: there are currently no items in the list to delete.";
            }
            else
            {
                statusStrip.Text = "Please select a valid plate from the list box.";
            }
        }
        #endregion
        #region Open
        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            string fileName = "";
            OpenFileDialog OpenText = new OpenFileDialog();
            OpenText.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            DialogResult sr = OpenText.ShowDialog();
            if (sr == DialogResult.OK)
            {
                fileName = OpenText.FileName;
            }
            currentFileName = fileName;
            try
            {
                RegoList.Clear();
                using (StreamReader reader = new StreamReader(File.OpenRead(fileName)))
                //(Stream stream = File.Open(fileName, FileMode.Open))
                {
                    //BinaryFormatter binaryFormatter = new BinaryFormatter();
                    while (!reader.EndOfStream)
                    //(stream.Position < stream.Length)
                    {
                        RegoList.Add(reader.ReadLine());
                    }
                }
                DisplayList();
            }
            catch (IOException)
            {
                MessageBox.Show("Cannot open file");
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("File path cannot be empty.");
            }
        }
        #endregion
        #region Save
        private void buttonSave_Click(object sender, EventArgs e)
        {
            string fileName = "";
            SaveFileDialog saveText = new SaveFileDialog();
            saveText.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            saveText.Filter = "Txt files (*.txt)|*.txt| All files (*.*)|*.*";
            DialogResult sr = saveText.ShowDialog();
            if (sr == DialogResult.OK)
            {
                fileName = saveText.FileName;
            }
            if (sr == DialogResult.Cancel)
            {
                fileName = saveText.FileName;
            }
            Save(fileName);
        }
        #endregion
        #region Load
        private void FormLists_Load(object sender, EventArgs e)
        {
            DisplayList();
        }
        #endregion
        #region Highlight Selection Method
        private void ListBoxDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBoxDisplay.SelectedIndex != -1)
            {
                textBoxInput.Text = listBoxDisplay.SelectedItem.ToString();
                textBoxInput.Select();
            }
        }
        #endregion
        #region Double click delete
        private void ListBoxDisplay_DoubleClick(object sender, EventArgs e)
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
        #endregion
        #region Edit
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            String NewValue = textBoxInput.Text;
            int RegoIndex = listBoxDisplay.SelectedIndex;
            bool alreadyExists = RegoList.Contains(textBoxInput.Text);
            if (!alreadyExists)
            {
                RegoList[RegoIndex] = NewValue;
                statusStrip.Text = "Rego plate edited to " + "'" + textBoxInput.Text.ToUpper() + "'" + " successfully.";
            }
            else
            {
                statusStrip.Text = "Error: rego plate already exists on the list.";
            }
            PostFunctionUtility();
        }
        #endregion
        #region Reset
        private void ButtonReset_Click(object sender, EventArgs e)
        {
            RegoList.Clear();
            PostFunctionUtility();
            statusStrip.Text = "Application reset successfully.";
        }
        #endregion
        #region Linear Search
        private void ButtonLinearSearch_Click(object sender, EventArgs e)
        {
            int counter = -1;
            foreach (String element in RegoList)
            {
                counter++;
                if (textBoxInput.Text == element)
                {
                    MessageBox.Show("Plate found.");
                    statusStrip.Text = "Plate found at index: " + counter;
                    return;
                }
            }
            MessageBox.Show("Rego plate not Found.");
            PostFunctionUtility();
        }
        #endregion
        #region Close form method
        private void VehicleRegistrationManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                int fileNumber = int.Parse(Path.GetFileNameWithoutExtension(currentFileName).Remove(0, 5));
                fileNumber++;
                String newValue;
                if (fileNumber <= 10)
                {
                    newValue = "0" + fileNumber.ToString();
                }
                else
                {
                    newValue = fileNumber.ToString();
                }
                String newFileName = "demo_" + newValue + ".txt";
                Save(newFileName);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Error!");
            }
            catch
            {
                return;
            }
        }
        #endregion
        public void Save(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, false))
                {
                    foreach (var item in RegoList)
                    {
                        writer.WriteLine(item);
                    }
                }
            }
            catch (IOException)
            {
                MessageBox.Show("cannot save file");
            }
        }
        #region Tag Method
        private void TagRego()
        {
            try
            {
                string tagIndexString = listBoxDisplay.SelectedIndex.ToString();
                int tagIndex = Int32.Parse(tagIndexString);
                string tagPlate = RegoList[tagIndex];
                if (tagPlate.StartsWith("z"))
                {
                    tagPlate = tagPlate.Remove(0, 1);
                    statusStrip.Text = "Rego untagged successfully.";
                }
                else
                {
                    tagPlate = "z" + tagPlate;
                    statusStrip.Text = "Rego tagged successfully.";
                }
                RegoList[tagIndex] = tagPlate;
                PostFunctionUtility();
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Please select a valid plate for tagging.");
            }

        }
        #endregion
        #region Tag Button
        private void ButtonTag_Click(object sender, EventArgs e)
        {
            TagRego();
        }
        #endregion
        #region Post function utility
        private void PostFunctionUtility()
        {
            DisplayList();
            textBoxInput.Clear();
            textBoxInput.Focus();
            listBoxDisplay.ClearSelected();
        }
        #endregion
    }
}
