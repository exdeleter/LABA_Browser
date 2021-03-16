using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zanyatie06._03
{
    public partial class Browser : Form
    {
        public Browser()
        {
            InitializeComponent();
            ReadFavorite();
        }

        private int kolpages = 0;

        public void NewTab(object sender, EventArgs e)
        {
            // GoBack
            //GoForward 
            //Navigate
            WebBrowser browser = new WebBrowser();
            browser.Visible = true;
            browser.ScriptErrorsSuppressed = true;
            browser.Dock = DockStyle.Fill;
            browser.DocumentCompleted += browser_DocumentCompleted; // событие

            AddresLine.Text = "";
            frame.TabPages.Add("Новая вкладка");
            frame.SelectTab(kolpages);
            frame.SelectedTab.Controls.Add(browser);
            kolpages++;
        }
        public void CloseTab(object sender, EventArgs e)
        {
            if (kolpages > 1)
            {
                
                frame.TabPages.Remove(frame.SelectedTab);
                kolpages--;
            } else
            {
                Application.Exit();
            }

        }
        private void GoToSite(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(AddresLine.Text))
            {
                // явное преобразование типов
                ((WebBrowser)frame.SelectedTab.Controls[0]).Navigate(AddresLine.Text);
                AddTextInHistory();
            }
        }
        void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            frame.SelectedTab.Text = ((WebBrowser)frame.SelectedTab.Controls[0]).DocumentTitle;
        }

        private void LoadNewTab(object sender, EventArgs e)
        {
            WebBrowser browser = new WebBrowser();
            browser.Visible = true;
            browser.ScriptErrorsSuppressed = true;
            browser.Dock = DockStyle.Fill;
            browser.DocumentCompleted += browser_DocumentCompleted; // событие

            frame.TabPages.Add("Новая вкладка");
            frame.SelectTab(kolpages);
            frame.SelectedTab.Controls.Add(browser);
            ((WebBrowser)frame.SelectedTab.Controls[0]).Navigate(@"http://startedpages.tilda.ws/");
            kolpages++;
            
        }
        
        private void GoBackInBrowser(object sender, EventArgs e) => ((WebBrowser)frame.SelectedTab.Controls[0]).GoBack(); //переход назад
        private void GoForwardInBrowser(object sender, EventArgs e) => ((WebBrowser)frame.SelectedTab.Controls[0]).GoForward(); //переход вперед 
        private void RefreshBrowser(object sender, EventArgs e) => ((WebBrowser)frame.SelectedTab.Controls[0]).Refresh(); //обновление

        private void SaveSites(object sender, EventArgs e) // сохраняет в избранные 
        {
            using (StreamWriter sr = new StreamWriter(@"C:\Users\dewaf\source\repos\Lababrowser\Lababrowser\SavedSites", true))
            {
                string path = AddresLine.Text;
                sr.WriteLine(path + "\n");
            }
            Favoritesites.Items.Add(AddresLine.Text);
        }

        private void GoToEnter(object sender, KeyEventArgs e) // переходит по нажатию ентер 
        {
            if(e.KeyCode == Keys.Enter)
            {
                ((WebBrowser)frame.SelectedTab.Controls[0]).Navigate(AddresLine.Text);
                AddTextInHistory();
            }
        }
        public void ReadFavorite()
        {
            using (StreamReader sr = new StreamReader(@"C:\Users\dewaf\source\repos\Lababrowser\Lababrowser\SavedSites"))
            {
                string p;
                while((p = sr.ReadLine())!= null)
                {
                    Favoritesites.Items.Add(p);
                }
            }
        } //метод, который считывает избранные сайты
        private void OpenTheFavoriteSite(object sender, KeyEventArgs e) // открывает выбранный избранный сайт 
        {
            ((WebBrowser)frame.SelectedTab.Controls[0]).Navigate(Favoritesites.SelectedItem.ToString());
            AddTextInHistory();
        }
        public void AddTextInHistory() //добавляет в файл история
        {
            using (StreamWriter sr = new StreamWriter(@"C:\Users\dewaf\source\repos\Lababrowser\Lababrowser\History", true))
            {
                string path = AddresLine.Text;
                sr.WriteLine("\n"+path + " time " + DateTime.Now + "\n");
            }
        }
        private void OpenHistory(object sender, EventArgs e)
        {
            HistoryBrowser br = new HistoryBrowser();
            br.Show();
        }


        private void DeleteFavoriteSite(object sender, EventArgs e)
        {
            int q = Favoritesites.SelectedIndex;
            string a  = Favoritesites.SelectedItem.ToString();
            //string n = Favoritesites.SelectedIndex.ToString();
            //using (StreamWriter sr = new StreamWriter(@"C:\Users\dewaf\source\repos\Lababrowser\Lababrowser\SavedSites"))
            //{
                
            //}
            Favoritesites.Items.RemoveAt(q);

            
            //string n = Favoritesites.SelectedIndex.ToString();
            //string path = Favoritesites.SelectedItem.ToString();
            //DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить всю историю", "Удаление истории", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //if (result == DialogResult.Yes)
            //{
            //    using (FileStream fs = new FileStream(@"C:\Users\dewaf\source\repos\Lababrowser\Lababrowser\History", FileMode.Create))
            //    {
            //        dataGridView1.Rows.Clear();
            //    }
            //}
        }

        private void SavePageOnDisk(object sender, EventArgs e)
        {

            ((WebBrowser)frame.SelectedTab.Controls[0]).ShowSaveAsDialog();
        }

    }
}
