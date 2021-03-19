using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
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

            
            frame.TabPages.Add("Новая вкладка");
            frame.SelectTab(kolpages);
            frame.SelectedTab.Controls.Add(browser);
            ((WebBrowser)frame.SelectedTab.Controls[0]).Navigate(@"http://startedpages.tilda.ws/");
            AddTextInHistory(@"http://startedpages.tilda.ws/");
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
                AddTextInHistory(AddresLine.Text);
                
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
            AddTextInHistory(@"http://startedpages.tilda.ws/");
            kolpages++;

        }

        private void GoBackInBrowser(object sender, EventArgs e)
        {
            ((WebBrowser) frame.SelectedTab.Controls[0]).GoBack(); //переход назад
            AddTextInHistory(((WebBrowser)frame.SelectedTab.Controls[0]).Url.ToString());
        }
        private void GoForwardInBrowser(object sender, EventArgs e)
        {
            ((WebBrowser)frame.SelectedTab.Controls[0]).GoForward(); //переход вперед 
            AddTextInHistory(((WebBrowser)frame.SelectedTab.Controls[0]).Url.ToString());
        }

        private void RefreshInBrowser(object sender, EventArgs e)
        {
            ((WebBrowser)frame.SelectedTab.Controls[0]).Refresh(); //обновление
            AddTextInHistory(((WebBrowser)frame.SelectedTab.Controls[0]).Url.ToString());
        }
        private void StopInBrowser(object sender, EventArgs e) => ((WebBrowser)frame.SelectedTab.Controls[0]).Stop();

        private void AddFavoriteSite(object sender, EventArgs e) // сохраняет в избранные 
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
                AddTextInHistory(AddresLine.Text);
            }
        }
        private void ReadFavorite() //метод, который считывает избранные сайты
        {
            using (StreamReader sr = new StreamReader(@"C:\Users\dewaf\source\repos\Lababrowser\Lababrowser\SavedSites"))
            {
                string p;
                while((p = sr.ReadLine())!= null)
                {
                    Favoritesites.Items.Add(p);
                    toolStripComboBox1.Items.Add(p);
                }
            }
        } 
        private void OpenTheFavoriteSite(object sender, KeyEventArgs e) // открывает выбранный избранный сайт 
        {
            ((WebBrowser)frame.SelectedTab.Controls[0]).Navigate(Favoritesites.SelectedItem.ToString());
            AddTextInHistory(Favoritesites.SelectedItem.ToString());
        }
        public void AddTextInHistory(string path) //добавляет в файл история
        {
            using (StreamWriter sr = new StreamWriter(@"C:\Users\dewaf\source\repos\Lababrowser\Lababrowser\History", true))
            {
                sr.WriteLine("\n" + DateTime.Now.DayOfWeek +" $day "+ path + " $time " + DateTime.Now + "\n");
            }

        }
        private void OpenHistory(object sender, EventArgs e) //открывает окно с историей
        {
            HistoryBrowser br = new HistoryBrowser();
            AddTextInHistory("History");
            br.Show();
        }

        private void DeleteFavoriteSite(object sender, EventArgs e) //удаляет избранный сайт 
        {
            int numofsite = Favoritesites.SelectedIndex;
            if(numofsite!=-1)
            {
                string deletingsite = Favoritesites.SelectedItem.ToString();
                string path = @"C:\Users\dewaf\source\repos\Lababrowser\Lababrowser\SavedSites";
                File.WriteAllLines(path, File.ReadAllLines(path).Where(v => v.Trim().IndexOf(deletingsite) == -1).ToArray());
                Favoritesites.Items.RemoveAt(numofsite);
            } else
            {

            }
            
        }

        private void SavePageOnDisk(object sender, EventArgs e)//сохраняет на диск
        {
            ((WebBrowser)frame.SelectedTab.Controls[0]).ShowSaveAsDialog();
        }

        private void CloseApp(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PrintPage(object sender, EventArgs e)
        {
            ((WebBrowser)frame.SelectedTab.Controls[0]).ShowPrintDialog();
        }

        private void OpenNewWindow(object sender, EventArgs e)
        {
            Browser br = new Browser();
            br.Show();
        }
    }
}
