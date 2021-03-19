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
                    int index = path.IndexOf("day"); ;
                    if(index!= -1)
                    {
                        int i = path.IndexOf("$day");
                        string day = path.Substring(0, i);
                        int m = path.LastIndexOf("$time");
                        string name = path.Substring(i+4,m-11);
                        i = path.LastIndexOf("time")+4;
                        string time = path.Substring(i);
                        string[] rows = { day, name, time };
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
