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
// An application for logging and recording vehicle registrations in a large city car park
namespace MyLists   
{
    public partial class VehicleRegistrationManager : Form
    {
        #region Initialization Components
        public VehicleRegistrationManager()
        {
            InitializeComponent();
        }

        // Initialisation of List object and initial file name.
        List<string> RegoList = new List<string>();
        string currentFileName = "demo_00";
        #endregion
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
            textBoxInput.Focus();

        }
        #endregion
        #region Button Open
        // Method for opening a text file from file explorer, reading its contents and displaying them.
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
                statusStrip.Text = "File opened successfully";
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
        #region Button Enter
        // Method for adding an element to the List and displaying it.
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            bool alreadyExists = RegoList.Contains(textBoxInput.Text);
            if (textBoxInput.Text.Length >= 3)
            {
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
            }
            else
            {
                statusStrip.Text = "Error: rego plate must be a minimum of 3 characters long.";
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
                if (RegoList.Any())
                {
                    if (!alreadyExists)
                    {
                        if (textBoxInput.Text.Length >= 3)
                        {
                            RegoList[RegoIndex] = NewValue;
                            statusStrip.Text = "Rego plate edited to " + "'" + textBoxInput.Text + "'" + " successfully.";
                        }
                        else if (listBoxDisplay.SelectedIndex == -1)
                        {
                            statusStrip.Text = "Error: please select a valid rego plate to edit.";
                        }
                        else if (textBoxInput.Text == "")
                        {
                            statusStrip.Text = "Error: rego plate may not be changed to a blank entry.";
                        }
                        else
                        {
                            statusStrip.Text = "Error: rego plate must be a minimum of 3 characters long.";
                        }
                    }
                    else
                    {
                        statusStrip.Text = "Error: rego plate already exists on the list.";
                    }
                    PostFunctionUtility();
                }
                else
                {
                    statusStrip.Text = "Error: there are currently no items on the list to edit.";
                }
            }
            catch(System.ArgumentOutOfRangeException)
            {
                statusStrip.Text = "Error: please select a valid rego plate to edit.";
            }
        }
        #endregion
        #region Button Tag
        // Tags a rego plate with the prefex 'z'. If the plate is already tagged it will remove it instead.
        private void ButtonTag_Click(object sender, EventArgs e)
        {
            if (RegoList.Any())
            {
                try
                {
                    string tagIndexString = listBoxDisplay.SelectedIndex.ToString();
                    int tagIndex = Int32.Parse(tagIndexString);
                    string tagPlate = RegoList[tagIndex];
                    if (tagPlate.StartsWith("Z"))
                    {
                        tagPlate = tagPlate.Remove(0, 1);
                        statusStrip.Text = "Rego plate untagged successfully.";
                    }
                    else
                    {
                        tagPlate = "Z" + tagPlate;
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
            else
            {
                statusStrip.Text = "Error: there are currently no rego plates on the list for tagging.";
            }
        }
            #endregion
        #region Button Linear Search
        // Utilises a foreach loop to iterate through each element in the list and check if it is the same and the text box input.
            private void ButtonLinearSearch_Click(object sender, EventArgs e)
        {
            int counter = -1;
            if (listBoxDisplay.Items.Count > 0)
            {
                foreach (String element in RegoList)
                {
                    counter++;
                    if (textBoxInput.Text == element)
                    {
                        listBoxDisplay.SelectedIndex = counter;
                        statusStrip.Text = "Rego plate found at index: " + counter + ".";
                        MessageBox.Show("Plate found.");


                        return;
                    }
                }
                statusStrip.Text = "";
                MessageBox.Show("Rego plate not found.");
            }
            else
            {
                statusStrip.Text = "Error: list box is empty.";
            }
            PostFunctionUtility();
        }
        #endregion
        #region Button Binary Search
        // Sorts the List then utilizes the in-built binary search method to find an element.
        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            RegoList.Sort();
            if (listBoxDisplay.Items.Count > 0)
            {
                if (RegoList.BinarySearch(textBoxInput.Text) >= 0)
                {
                    listBoxDisplay.SelectedIndex = RegoList.BinarySearch(textBoxInput.Text);
                    statusStrip.Text = "Rego plate found at index: " + RegoList.BinarySearch(textBoxInput.Text) + ".";
                    MessageBox.Show("Plate found.");

                }
                else
                {
                    statusStrip.Text = "";
                    MessageBox.Show("Rego plate not found.");
                    PostFunctionUtility();
                }
            }
            else
            {
                statusStrip.Text = "Error: list box is empty.";
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
        #region Method Save
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
                MessageBox.Show("Error: file cannot be saved.");
            }
            catch (System.ArgumentException)
            {

            }
        }
        #endregion
        #region Method Delete
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
                statusStrip.Text = "Error: there are currently no items on the list to delete.";
            }
            else
            {
                statusStrip.Text = "Error: please select a rego plate from the list to delete.";
            }
        }
        #endregion
        #region Save on Form Close
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
        #region Method Post Function Utility
        // Utility method for displaying the list, clearing the textbox input, placing focus in the text box and clearing selection simultaneously.
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
        #region TextBox Input Handling
        // Text box input converted to uppercase
        // Handling of user keyboard input to only allow control functions, letters, digits and hyphens
        // Text box input length cannot be longer than 9 characters
        private void TextBoxInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxInput.Text.Length < 9)
            {
                textBoxInput.CharacterCasing = CharacterCasing.Upper;
                char ch = e.KeyChar;
                if (!(char.IsControl(ch) || char.IsLetterOrDigit(ch) || ch == '-'))
                {
                    e.Handled = true;
                    statusStrip.Text = "Error: accepted characters include: numbers 0-9, letters A-Z & hyphen.";
                }
            }
            else
            {
                statusStrip.Text = "Error: rego plates must be less than 9 characters in length.";
            }

        }
        #endregion
    }
}
