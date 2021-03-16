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

namespace Zanyatie06._03
{
    public partial class HistoryBrowser : Form
    {
        public HistoryBrowser()
        {
            InitializeComponent();
            ReadHistory();
        }

        private void ReadHistory()
        {
            using (StreamReader sr = new StreamReader(@"C:\Users\dewaf\source\repos\Lababrowser\Lababrowser\History"))
            {
                string path;
                while ((path=sr.ReadLine())!= null)
                {
                    int index = path.IndexOf("time"); ;
                    if(index!= -1)
                    {
                        int i = path.IndexOf("time");
                        string name = path.Substring(0, i);
                        i = path.LastIndexOf("time")+4;
                        string time = path.Substring(i);
                        string[] rows = { name, time };
                        dataGridView1.Rows.Add(rows);
                    }
                    else
                    {

                    }
                    
                }
            }

        }

        private void DeleteHistory(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить всю историю", "Удаление истории", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (FileStream fs = new FileStream(@"C:\Users\dewaf\source\repos\Lababrowser\Lababrowser\History", FileMode.Create))
                {
                    dataGridView1.Rows.Clear();
                }
            }

        }
    }
}
