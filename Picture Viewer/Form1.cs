using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace images_viewer
{
    
    public partial class Picture_viewer : Form
    {
        public Picture_viewer()
        {
            InitializeComponent();
           
        }

        List<String> list = new List<string>();
        int i = 0;
       
        // add images to program
        private void btn_choosepicture_Click(object sender, EventArgs e)
        {

            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "Images|*.jpg;*.png;*.bmp;*.gif;";
            SaveFileDialog savaFileDialoge = new SaveFileDialog();
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                foreach(String fileName in openFileDialog1.FileNames) 
                {
                    list.Add(fileName);
                    String fn = Path.GetFileNameWithoutExtension(fileName);
                    
                    if (!listBox1.Items.Contains(fn))
                    {
                        listBox1.Items.Add(fn);
                    }

                }
                
            }
            else
            {
                MessageBox.Show("You shouid add images");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (listBox1.SelectionMode==SelectionMode.One)
            {
                pictureBox1.ImageLocation = list[listBox1.SelectedIndex];
            }
            else if(listBox1.SelectionMode == SelectionMode.MultiExtended)
            {
                // multi images

                panel1.Controls.Clear();
                panel1.Show();
                pictureBox1.Hide();
                pictureBox2.Hide();
                int x = 7;
                int y = 7;
                int h = 0;
                foreach (int indeximageSelected in listBox1.SelectedIndices)
                {

                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(new Point(120,100));
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    pic.ImageLocation = list[indeximageSelected];
                    pic.Left = x;
                    pic.Top = y;
                    h++;
                    if (h >= 10)
                    {
                        x = 7;
                        y = pic.Top + pic.Height + 7;
                        h = 0;
                    }
                    else
                    {
                        x = pic.Left + pic.Width + 7;
                    }
                    this.panel1.Controls.Add(pic);

                }
                
            }
        }

        // single image 

        private void singleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pictureBox1.Show();
            pictureBox2.Hide();
            panel1.Hide();
            listBox1.SelectionMode = SelectionMode.One;
            statusBar1.Hide();
            

        }

        // slide show

        private void slideShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Show();
            pictureBox1.Hide();
            panel1.Hide();
            statusBar1.Show();
            listBox1.SelectionMode = SelectionMode.None;
            timer1.Enabled=true;
            timer1_Tick(sender, e);

        }
        
        // timer for slide show

        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            if (i >= list.Count)
            {
                i = 0;
            }
           
                pictureBox2.ImageLocation = list[i];  
                statusBar1.Panels[0].Text= Path.GetFileNameWithoutExtension(list[i]);

        }


        // choose from Strip Menu

        private void multiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            pictureBox1.Hide();
            pictureBox2.Hide();
            statusBar1.Hide();
        }

        // exit programe
        private void existToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
