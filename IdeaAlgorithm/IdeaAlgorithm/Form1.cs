using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdeaAlgorithm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            var key =  IdeaAlgorithmImpl.makeKey(keyTextBox.Text);
            var inputText = inputBox.Text;
            IdeaAlgorithmImpl ideaAlgorithmImpl = new IdeaAlgorithmImpl(key, true);
            var encryptData = Encoding.ASCII.GetBytes(inputText);
            var result = ideaAlgorithmImpl.crypt(encryptData);
            //outputBox.Text = string.Join(" ", encryptData);

            outputBox.Text = Encoding.ASCII.GetString(result);
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            var key = IdeaAlgorithmImpl.makeKey(keyTextBox.Text);
            var inputText = outputBox.Text;
            IdeaAlgorithmImpl ideaAlgorithmImpl = new IdeaAlgorithmImpl(key, false);
            var encryptData = Encoding.ASCII.GetBytes(inputText);
            var result = ideaAlgorithmImpl.crypt(encryptData);

            //inputBox.Text = string.Join(" ", encryptData);


            inputBox.Text = Encoding.ASCII.GetString(result);
        }
    }
}
