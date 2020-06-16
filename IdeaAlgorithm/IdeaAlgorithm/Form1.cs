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
            IdeaAlgorithmImpl ideaAlgorithmImpl = new IdeaAlgorithmImpl(key, true);
            var encryptData = GetASCIIBox(inputBox);

            if (encryptData == null) return;
            var result = ideaAlgorithmImpl.crypt(encryptData);
            outputBox.Text = string.Join(" ", result);
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            var key = IdeaAlgorithmImpl.makeKey(keyTextBox.Text);
            IdeaAlgorithmImpl ideaAlgorithmImpl = new IdeaAlgorithmImpl(key, false);
            var encryptData = GetASCIIBox(outputBox);

            if (encryptData == null) return;

            var result = ideaAlgorithmImpl.crypt(encryptData);
            inputBox.Text = string.Join(" ", result);
        }

   
        private byte[] GetASCIIBox(RichTextBox box)
        {
            try
            {
                var res = box.Text.Split(' ').Select(_ => Convert.ToByte(_)).ToArray();
                return res;
            }
            catch (Exception)
            {
                MessageBox.Show("Procesowane dane mają nieodpowiedni format", "O nie!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        
        private void toBytesButton_Click(object sender, EventArgs e)
        {
            var bytes = inputBox.Text.Select(_ => (byte)_);
            inputBox.Text = string.Join(" ", bytes);
        }

        private void toASCIIButton_Click(object sender, EventArgs e)
        {
            try
            {
                var encryptData = GetASCIIBox(inputBox);
                if (encryptData == null) return;
                inputBox.Text = Encoding.ASCII.GetString(encryptData);
            }
            catch (Exception)
            {
                MessageBox.Show("Podanego wyrażenia nie da się zamienić na ASCII","O nie!" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
