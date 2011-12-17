using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PonyCommander
{
    public partial class PonyCommanderForm : Form
    {
        private ListView AktywneOkno = null;
        private string AktywnaSciezka;
        private ListView NieAktywneOkno = null;
        private string NieAktywnaSciezka;
        private string sciezka1 = "C:\\";
        private string sciezka2 = "C:\\";

        public PonyCommanderForm()
        {
            Thread t = new Thread(new ThreadStart(SplashScreen));
            t.Start();
            Thread.Sleep(1500);
            InitializeComponent();
            t.Abort();
            
        }

        public void SplashScreen()
        {
            Application.Run(new PonySplash());
        }

        private void wyswietl(ListView gdzie, string katalog)
        {
            gdzie.Items.Clear();
            string[] nazwy;
            ListViewItem buf;
            FileInfo plik;
            DirectoryInfo dir;
            if (!Dysk1.Items.Contains(katalog))
                gdzie.Items.Add(new ListViewItem("..."));

            int i;
            nazwy = Directory.GetDirectories(katalog);
            for (i = 0; i < nazwy.Length; i++)
            {
                dir = new DirectoryInfo(nazwy[i]);
                buf = new ListViewItem(new string[] { dir.Name, "<DIR>" });
                gdzie.Items.Add(buf);
            }
            string nazwapliku;
            int gdziekropka;
            nazwy = Directory.GetFiles(katalog);
            for (i = 0; i < nazwy.Length; i++)
            {
                plik = new FileInfo(nazwy[i]);
                nazwapliku = plik.Name;
                gdziekropka = plik.Name.LastIndexOf('.');
                if (gdziekropka > 0)
                    nazwapliku = nazwapliku.Substring(0, plik.Name.LastIndexOf('.'));
                buf = new ListViewItem(new string[] { nazwapliku, plik.Extension.Replace(".", ""),
                    plik.Length.ToString() });
                gdzie.Items.Add(buf);
            }
        }

        private void KopiujKatalog(string zrodlo, string cel)
        {
            string[] nazwy;
            if (!Directory.Exists(cel)) Directory.CreateDirectory(cel);
            nazwy = Directory.GetFileSystemEntries(zrodlo);
            foreach (string nazwa in nazwy)
            {
                if (Directory.Exists(nazwa))
                {
                    KopiujKatalog(nazwa, cel + Path.GetFileName(nazwa) + "\\");
                }
                else
                {
                    File.Copy(nazwa, cel + Path.GetFileName(nazwa), true);
                }
            }
        }

        private void PonyCommanderForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Maximized)
            {
                int szAll = this.Size.Width - 33;
                int sz = szAll / 2;
                lvOkno1.Width = sz;
                lvOkno2.Width = sz;
                lvOkno2.Location = new Point(lvOkno1.Location.X + sz + 3, lvOkno1.Location.Y);
            }
        }

        private void PonyCommanderForm_Load(object sender, EventArgs e)
        {
            string[] nazwy;
            nazwy = Directory.GetLogicalDrives();
            foreach (string dysk in nazwy)
            {
                Dysk1.Items.Add(dysk);
                Dysk2.Items.Add(dysk);
            }
            Dysk1.SelectedIndex = Dysk1.Items.IndexOf("C:\\");
            Dysk2.SelectedIndex = Dysk2.Items.IndexOf("C:\\");
        }

        private void Dysk1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sciezka1 = Dysk1.Text;
            AktywnaSciezka = Dysk1.Text;
            try
            {
                wyswietl(lvOkno1, sciezka1);
            }
            catch
            {
                MessageBox.Show("Błąd przy próbie dostępu do napędu", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Dysk1.SelectedIndex = Dysk1.Items.IndexOf("C:\\");
            }
        }

        private void Dysk2_SelectedIndexChanged(object sender, EventArgs e)
        {
            sciezka2 = Dysk2.Text;
            AktywnaSciezka = Dysk2.Text;
            try
            {
                wyswietl(lvOkno2, sciezka2);
            }
            catch
            {
                MessageBox.Show("Błąd przy próbie dostępu do napędu", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Dysk2.SelectedIndex = Dysk2.Items.IndexOf("C:\\");
            }
        }

        private void lvOkno1_Enter(object sender, EventArgs e)
        {
            AktywneOkno = lvOkno1;
            AktywnaSciezka = sciezka1;
            NieAktywneOkno = lvOkno2;
            NieAktywnaSciezka = sciezka2;
        }

        private void lvOkno2_Enter(object sender, EventArgs e)
        {
            AktywneOkno = lvOkno2;
            AktywnaSciezka = sciezka2;
            NieAktywneOkno = lvOkno1;
            NieAktywnaSciezka = sciezka1;
        }

        private void lvOkno_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e == null || e.Button == MouseButtons.Left)
            {
                if (AktywneOkno.SelectedItems[0].Text == "...")
                {
                    AktywnaSciezka = AktywnaSciezka.TrimEnd(new char[] { '\\' }); // usuniecie ostatniego znaku '\'
                    int last; // indeks ostatniego znaku '\'
                    last = AktywnaSciezka.LastIndexOf('\\');
                    AktywnaSciezka = AktywnaSciezka.Substring(0, last + 1);
                    wyswietl(AktywneOkno, AktywnaSciezka);
                }
                else if (AktywneOkno.SelectedItems[0].SubItems[1].Text == "<DIR>")
                {
                    AktywnaSciezka += (AktywneOkno.SelectedItems[0].Text + "\\");
                    wyswietl(AktywneOkno, AktywnaSciezka);
                }
                else
                {
                    string nazwa = AktywnaSciezka +
                    AktywneOkno.SelectedItems[0].Text +
                        "." + AktywneOkno.SelectedItems[0].SubItems[1].Text;
                    try
                    {
                        System.Diagnostics.Process.Start(nazwa);
                    }
                    catch
                    {
                        MessageBox.Show("Nie można otworzyć pliku", "Błąd",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
                if (AktywneOkno == lvOkno1)
                    sciezka1 = AktywnaSciezka;
                else sciezka2 = AktywnaSciezka;
            }
        }

        private void lvOkno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (AktywneOkno.SelectedItems[0].Text == "...")
                {
                    AktywnaSciezka = AktywnaSciezka.TrimEnd(new char[] { '\\' }); // usuniecie ostatniego znaku '\'
                    int last; // indeks ostatniego znaku '\'
                    last = AktywnaSciezka.LastIndexOf('\\');
                    AktywnaSciezka = AktywnaSciezka.Substring(0, last + 1);
                    wyswietl(AktywneOkno, AktywnaSciezka);
                }
                else if (AktywneOkno.SelectedItems[0].SubItems[1].Text == "<DIR>")
                {
                    AktywnaSciezka += (AktywneOkno.SelectedItems[0].Text + "\\");
                    wyswietl(AktywneOkno, AktywnaSciezka);
                }
                else
                {
                    string nazwa = AktywnaSciezka +
                        AktywneOkno.SelectedItems[0].Text +
                        "." + AktywneOkno.SelectedItems[0].SubItems[1].Text;
                    try
                    {
                        System.Diagnostics.Process.Start(nazwa);
                    }
                    catch
                    {
                        MessageBox.Show("Nie można otworzyć pliku", "Błąd",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
                if (AktywneOkno == lvOkno1)
                    sciezka1 = AktywnaSciezka;
                else sciezka2 = AktywnaSciezka;
            }
        }

        private void cmdOpen_Click(object sender, EventArgs e)
        {
            if (AktywneOkno == null) return;
            if (AktywneOkno.SelectedItems.Count != 1) return;
            lvOkno_MouseDoubleClick(null, null);
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            if (AktywneOkno == null) return;
            for (int i = 0; i < AktywneOkno.SelectedItems.Count; i++)
            {
                if (AktywneOkno.SelectedItems[i].Text == "...")
                    return;
                string zrodlo, cel;
                if (AktywneOkno.SelectedItems[i].SubItems[1].Text == "<DIR>")
                {
                    zrodlo = AktywnaSciezka + AktywneOkno.SelectedItems[i].Text + "\\";
                    cel = NieAktywnaSciezka + AktywneOkno.SelectedItems[i].Text + "\\";
                    KopiujKatalog(zrodlo, cel);
                }
                else
                {
                    zrodlo = AktywnaSciezka + AktywneOkno.SelectedItems[i].Text +
                        "." + AktywneOkno.SelectedItems[i].SubItems[1].Text;
                    cel = NieAktywnaSciezka + AktywneOkno.SelectedItems[i].Text +
                        "." + AktywneOkno.SelectedItems[i].SubItems[1].Text;
                    File.Copy(zrodlo, cel, true);
                }
            }
            wyswietl(NieAktywneOkno, NieAktywnaSciezka);
        }

        private void cmdMove_Click(object sender, EventArgs e)
        {
            if (AktywneOkno == null) return;
            for (int i = 0; i < AktywneOkno.SelectedItems.Count; i++)
            {
                if (AktywneOkno.SelectedItems[i].Text == "...")
                    return;
                string zrodlo, cel;
                if (AktywneOkno.SelectedItems[i].SubItems[1].Text == "<DIR>")
                {
                    zrodlo = AktywnaSciezka + AktywneOkno.SelectedItems[i].Text + "\\";
                    cel = NieAktywnaSciezka + AktywneOkno.SelectedItems[i].Text + "\\";
                    KopiujKatalog(zrodlo, cel);
                    Directory.Delete(zrodlo, true);
                }
                else
                {
                    zrodlo = AktywnaSciezka + AktywneOkno.SelectedItems[i].Text +
                        "." + AktywneOkno.SelectedItems[i].SubItems[1].Text;
                    cel = NieAktywnaSciezka + AktywneOkno.SelectedItems[i].Text +
                        "." + AktywneOkno.SelectedItems[i].SubItems[1].Text;
                    File.Copy(zrodlo, cel, true);
                    File.Delete(zrodlo);
                }
            }
            wyswietl(lvOkno1, sciezka1);
            wyswietl(lvOkno2, sciezka2);
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (AktywneOkno == null) return;
            if (AktywneOkno.SelectedItems[0].Text == "...")
                return;
            DialogResult odp = MessageBox.Show("Czy na pewno?", "Usuń",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (odp != DialogResult.Yes) return;
            string co;
            for (int i = 0; i < AktywneOkno.SelectedItems.Count; i++)
            {
                if (AktywneOkno.SelectedItems[i].SubItems[1].Text == "<DIR>")
                {
                    co = AktywnaSciezka + AktywneOkno.SelectedItems[i].Text;
                    Directory.Delete(co, true);
                }
                else
                {
                    co = AktywnaSciezka + AktywneOkno.SelectedItems[i].Text +
                        "." + AktywneOkno.SelectedItems[i].SubItems[1].Text;
                    File.Delete(co);
                }
            }
            wyswietl(AktywneOkno, AktywnaSciezka);
        }

        private void PonyCommanderForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F3:
                    cmdOpen_Click(null, null);
                    break;
                case Keys.F5:
                    cmdCopy_Click(null, null);
                    break;
                case Keys.F6:
                    cmdMove_Click(null, null);
                    break;
                case Keys.F8:
                    cmdDelete_Click(null, null);
                    break;
            }
        }
    }
}
