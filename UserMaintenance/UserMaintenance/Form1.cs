using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();

        public Form1()
        {
            InitializeComponent();

            label1.Text = Resource1.FullName;
            button1.Text = Resource1.Add;
            button2.Text = Resource1.Button2;

            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = textBox1.Text,
            };
            users.Add(u);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = @"C:\Users\Vivi\source\repos\VersionControl\UserMaintenance\UserMaintenance\bin\Debug";

            // A kiválasztható fájlformátumokat adjuk meg ezzel a sorral. Jelen esetben csak a csv-t fogadjuk el
            saveFileDialog.Filter = "Comma Seperated Values (*.csv)|*.csv";

            // A csv lesz az alapértelmezetten kiválasztott kiterjesztés
            saveFileDialog.DefaultExt = "csv";

            // Ha ez igaz, akkor hozzáírja a megadott fájlnévhez a kiválasztott kiterjesztést, de érzékeli, ha a felhasználó azt is beírta és nem fogja duplán hozzáírni
            saveFileDialog.AddExtension = true;

            //a fájl nevét adom meg
            saveFileDialog.FileName = "userList";

            saveFileDialog.ShowDialog();

            /*A StreamWriter paraméterei:
             * 1) Fájlnév: mi itt azt a fájlnevet adjuk át, amit a felhasználó az sfd dialógusban megadott
             * 2) Append: ha igaz és már létezik ilyen fájl, akkor a végéhez fűzi a sorokat, ha hamis, akkor felülírja a létező fájlt
             * 3) Karakterkódolás: a magyar nyelvnek is megfelelő legáltalánosabb karakterkódolás az UTF8*/
            using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
            {
                //végig megyek a lista elemein, mert a StreamWriter részenként építi fel a sorokat.
                foreach (var u1 in users)
                {
                    sw.Write(u1.ID);
                    sw.Write(";");
                    sw.Write(u1.FullName);

                    sw.WriteLine();
                }
            }

        }
    }
}
