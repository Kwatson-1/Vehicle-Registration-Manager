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
        // Initialisation of List object and initial file name.
        List<string> RegoList = new List<string>();
        string currentFileName = "demo_00";

        #region Load Form
        // Displays the list on form load.
        private void FormLists_Load(object sender, EventArgs e)
        {
            DisplayList();
        }
        #endregion
        #region Display List
        // Clears all items in the listBox, sorts the List and then iterate through all objects and displays them in the listBox
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
        #region Button Binary Search
        // Sorts the List then utilizes the in-built binary search method to find an element.
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
                MessageBox.Show("Rego plate not found.");
            }
            PostFunctionUtility();

        }
        #endregion
        #region Button Enter
        // Method for adding an element to the List and displaying it.
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            bool alreadyExists = RegoList.Contains(textBoxInput.Text);
            if (!alreadyExists)
            {
                if (textBoxInput.Text != string.Empty)
                {
                    RegoList.Add(textBoxInput.Text);
                    statusStrip.Text = "Rego plate " + "'" + textBoxInput.Text + "'" + " added successfully.";
                }
                else
                {
                    statusStrip.Text = "Error: please enter a valid rego plate.";
                }
            }
            else
            {
                statusStrip.Text = "Error: cannot enter a duplicate item into the list.";
            }
            PostFunctionUtility();
        }
        #endregion
        #region Button Delete
        // Method for deleting an element off the list.
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            DeleteMethod();
        }
        #endregion
        #region Button Open
        // Method for opening a text file from file explorer, reading its contents and displaying them.
        // Limited error trapping.
        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            string fileName = "";
            OpenFileDialog OpenText = new OpenFileDialog();
            OpenText.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            OpenText.Filter = "Txt files (*.txt)|*.txt| All files (*.*)|*.*";
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
                {
                    while (!reader.EndOfStream)
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

            }
        }
        #endregion
        #region Button Save
        // Functionality for saving the List contents to a text file.
        private void ButtonSave_Click(object sender, EventArgs e)
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
        #region Double Click Delete
        // Opens a dialogue box asking if the user wants to delete an item from a List when double clicked.
        private void ListBoxDisplay_DoubleClick(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to delete this item?", "Delete Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DeleteMethod();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
        #endregion
        #region Button Edit
        // Button for editing the selected rego plate.
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            String NewValue = textBoxInput.Text;
            int RegoIndex = listBoxDisplay.SelectedIndex;
            bool alreadyExists = RegoList.Contains(textBoxInput.Text);
            try
            {
                if (!alreadyExists)
                {
                    RegoList[RegoIndex] = NewValue;
                    statusStrip.Text = "Rego plate edited to " + "'" + textBoxInput.Text + "'" + " successfully.";
                }
                else
                {
                    statusStrip.Text = "Error: rego plate already exists on the list.";
                }
                PostFunctionUtility();
            }
            catch(System.ArgumentOutOfRangeException)
            {
                statusStrip.Text = "Error: no rego plate to edit has been selected.";
            }
        }
        #endregion
        #region Button Reset
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
            textBoxInput.Clear();
            listBoxDisplay.ClearSelected();
            textBoxInput.Focus();
        }
        #endregion
        #region Close Form Save
        // Saves the form automatically when closed and increments the save name by 1.
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
        #region Save Method
        // Method for iterating through the rego list and writing it to an external text file.
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
            catch (System.ArgumentException)
            {

            }
        }
        #endregion
        #region Button Tag
        // Tags a rego plate with the prefex 'z'. If the plate is already tagged it will remove it instead.
        private void ButtonTag_Click(object sender, EventArgs e)
        {
            try
            {
                string tagIndexString = listBoxDisplay.SelectedIndex.ToString();
                int tagIndex = Int32.Parse(tagIndexString);
                string tagPlate = RegoList[tagIndex];
                if (tagPlate.StartsWith("z"))
                {
                    tagPlate = tagPlate.Remove(0, 1);
                    statusStrip.Text = "Rego plate untagged successfully.";
                }
                else
                {
                    tagPlate = "z" + tagPlate;
                    statusStrip.Text = "Rego plate tagged successfully.";
                }
                RegoList[tagIndex] = tagPlate;
                DisplayList();
                textBoxInput.Text = tagPlate;
                listBoxDisplay.SelectedIndex = tagIndex;
            }
            catch (System.ArgumentOutOfRangeException)
            {
                statusStrip.Text = "Error: please select a valid rego plate for tagging.";
            }
        }
        #endregion
        #region Post Function Utility
        // Utility method for dispalying the list, clearing the textbox input, placing focus in the text box and clearing selection simultaneously.
        private void PostFunctionUtility()
        {
            DisplayList();
            textBoxInput.Clear();
            textBoxInput.Focus();
            listBoxDisplay.ClearSelected();
        }
        #endregion
        #region Highlight Selection
        // Items clicked in the list box will be converted to a String and placed in the input text box and highlighted.
        private void ListBoxDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBoxDisplay.SelectedIndex != -1)
            {
                textBoxInput.Text = listBoxDisplay.SelectedItem.ToString();
                textBoxInput.Select();
            }
        }
        #endregion
        #region Clear Selection
        // Clicking on the form will clear the focus and deselect items.
        private void VehicleRegistrationManager_Click(object sender, EventArgs e)
        {
            listBoxDisplay.SelectedItems.Clear();
            listBoxDisplay.Focus();
        }
        // Clicking white space in the listBox will clear the selection.
        private void ListBoxDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            int index = listBoxDisplay.IndexFromPoint(pt);

            if (index <= -1)
            {
                listBoxDisplay.SelectedItems.Clear();
            }
        }
        #endregion
        #region Delete Method
        // Method for checking if a rego has been selected and deleting it.
        private void DeleteMethod()
        {
            bool isEmpty = !RegoList.Any();
            if (RegoList.Contains(textBoxInput.Text))
            {
                int delete = RegoList.IndexOf(textBoxInput.Text);
                RegoList.RemoveAt(delete);
                statusStrip.Text = "Rego plate " + "'" + textBoxInput.Text + "'" + " deleted successfully."; DisplayList();
                PostFunctionUtility();
            }
            else if (listBoxDisplay.SelectedIndex != -1)
            {
                listBoxDisplay.SetSelected(listBoxDisplay.SelectedIndex, true);
                RegoList.RemoveAt(listBoxDisplay.SelectedIndex);
                statusStrip.Text = "Rego plate " + "'" + textBoxInput.Text + "'" + " deleted successfully."; DisplayList();
                PostFunctionUtility();
            }
            else if (isEmpty)
            {
                statusStrip.Text = "Error: there are currently no items in the list to delete.";
            }
            else
            {
                statusStrip.Text = "Error: please select a valid item from the list to delete.";
            }
        }
        #endregion

        private void TextBoxInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBoxInput.CharacterCasing = CharacterCasing.Upper;
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch!= 46 && !char.IsNumber(ch))
            {

                e.Handled = true;
                statusStrip.Text = "Error: accepted characters include: Numbers 0-9, Letters A-Z and \"-\"";
            }

        }
    }
}
