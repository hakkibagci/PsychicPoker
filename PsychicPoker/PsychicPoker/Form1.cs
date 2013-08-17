using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PsychicPoker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
        }

        //to store input lines
        List<string> inputLines = new List<string>();

        //to store result lines
        List<string> resultLines = new List<string>();

        /// <summary>
        /// Opens an input file that contains the input lines for hand and deck cards.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();

            inputLines.Clear();
            textBox.Clear();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                string line;

                try
                {
                    //read the input lines from the input file and show them in the text box
                    using (StreamReader reader = new StreamReader(openFileDialog1.FileName))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            inputLines.Add(line);
                            textBox.Text += line + Environment.NewLine;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show(this, "An error occured while opening file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        /// <summary>
        /// Calculates the best hand values for the given input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            resultLines.Clear();
           

            if (inputLines.Count > 0)
            {
                string result;

                try
                {
                    //find the possible best hand value for each input line
                    foreach (string line in inputLines)
                    {
                        result = HandEvaluator.FindBestHandValue(line);
                        resultLines.Add(result);
                    }

                }
                catch (InvalidInputException ex)
                {
                    //This exception type is a known exception, so we can show its message to user.
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    resultLines.Clear();
                    return;
                }
                catch
                {
                    //Normally we should log this kind of errors.
                    MessageBox.Show(this, "An unknown error occured during hand evaluation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                textBox.Clear();
                
                //show the result in the text box
                foreach (string line in resultLines)
                {
                    textBox.Text += line + Environment.NewLine;
                }
                
            }
            else
            {
                MessageBox.Show(this,"There is no input to process!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Saves the current results to a text file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (resultLines.Count == 0)
            {
                MessageBox.Show(this, "There is nothing to save!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult dr = saveFileDialog1.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    //write the result lines to a test file
                    using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                    {
                        foreach (string resultLine in resultLines)
                        {
                            writer.WriteLine(resultLine);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show(this, "An error occured while saving file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

       
    }
}
