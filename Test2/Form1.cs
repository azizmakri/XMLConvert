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
using System.Xml.Serialization;

namespace Test2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Product> products = new List<Product>();
            XmlSerializer searial = new XmlSerializer(typeof(List<Product>));
            products.Add(new Product() {Id=1,Name="aziz"});
            products.Add(new Product() {Id=2,Name="amir"});
            using (FileStream fs = new FileStream(Path.Combine(Environment.CurrentDirectory, "Z:\\AZIZ\\4SAE\\stage4\\aziz.xml"), FileMode.Create, FileAccess.Write))
            {
                searial.Serialize(fs, products);
                MessageBox.Show("Created");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Product> products = new List<Product>();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Filter = "XML Files (*.xml)|*.xml";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));

                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        products = serializer.Deserialize(fs) as List<Product>;
                    }
                }
            }

            dataGridView1.DataSource = products;
        }

    }
}
