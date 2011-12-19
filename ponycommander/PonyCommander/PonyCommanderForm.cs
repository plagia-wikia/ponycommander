using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

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
        private int sortColumn1 = -1;
        private int sortColumn2 = -1;

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
            try
            {
                nazwy = Directory.GetDirectories(katalog);
                for (i = 0; i < nazwy.Length; i++)
                {
                    dir = new DirectoryInfo(nazwy[i]);
                    buf = new ListViewItem(new string[] { dir.Name, "<DIR>", "", dir.LastWriteTime.ToString() });
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
                    plik.Length.ToString(), plik.LastWriteTime.ToString()});
                    gdzie.Items.Add(buf);
                }
            }
            catch
            {
                MessageBox.Show("Nie można uzyskać dostępu do katalogu", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Anuluj";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            form.TopMost = true;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
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

        private void lvOkno_MouseClick(object sender, MouseEventArgs e)
        {
            if (e == null || e.Button == MouseButtons.Right)
            {
                Point pt = e.Location;
                contextMenu1.Show(AktywneOkno, pt);
                if (AktywneOkno == lvOkno1)
                    sciezka1 = AktywnaSciezka;
                else sciezka2 = AktywnaSciezka;
            }
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
                    if (zrodlo != cel)
                    {
                        if (Directory.Exists(cel))
                        {
                            DialogResult odp = MessageBox.Show("Czy zastąpić istniejący katalog?", "Katalog istnieje",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (odp != DialogResult.Yes) return;
                        }
                        try
                        {
                            KopiujKatalog(zrodlo, cel);
                        }
                        catch
                        {
                            MessageBox.Show("Nie można uzyskać dostępu do katalogu", "Błąd",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Próbujesz skopiować do tego samego katalogu", "Błąd",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                    
                }
                else
                {
                    zrodlo = AktywnaSciezka + AktywneOkno.SelectedItems[i].Text +
                        "." + AktywneOkno.SelectedItems[i].SubItems[1].Text;
                    cel = NieAktywnaSciezka + AktywneOkno.SelectedItems[i].Text +
                        "." + AktywneOkno.SelectedItems[i].SubItems[1].Text;
                    if (zrodlo != cel)
                    {
                        if (File.Exists(cel))
                        {
                            DialogResult odp = MessageBox.Show("Czy zastąpić istniejący plik?", "Plik istnieje",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (odp != DialogResult.Yes) return;
                        }
                        try
                        {

                            
                            
                            File.Copy(zrodlo, cel, true);
                        }
                        catch
                        {
                            MessageBox.Show("Nie można uzyskać dostępu do pliku", "Błąd",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Próbujesz skopiować do tego samego katalogu", "Błąd",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
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

                    if (zrodlo != cel)
                    {
                        if (Directory.Exists(cel))
                        {
                            DialogResult odp = MessageBox.Show("Czy zastąpić istniejący katalog?", "Katalog istnieje",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (odp != DialogResult.Yes) return;
                        }
                        try
                        {
                            Directory.Move(zrodlo, cel);
                        }
                        catch
                        {
                            MessageBox.Show("Nie można uzyskać dostępu do katalogu", "Błąd",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Próbujesz przenieść do tego samego katalogu", "Błąd",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }




                }
                else
                {
                    zrodlo = AktywnaSciezka + AktywneOkno.SelectedItems[i].Text +
                        "." + AktywneOkno.SelectedItems[i].SubItems[1].Text;
                    cel = NieAktywnaSciezka + AktywneOkno.SelectedItems[i].Text +
                        "." + AktywneOkno.SelectedItems[i].SubItems[1].Text;
                    if (zrodlo != cel)
                    {
                        if (File.Exists(cel))
                        {
                            DialogResult odp = MessageBox.Show("Czy zastąpić istniejący plik?", "Plik istnieje",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (odp != DialogResult.Yes) return;

                        }
                        try
                        {
                            File.Move(zrodlo, cel);
                        }
                        catch
                        {
                            MessageBox.Show("Nie można uzyskać dostępu do pliku", "Błąd",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Próbujesz przenieść do tego samego katalogu", "Błąd",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                    try
                    {
                        Directory.Delete(co, true);
                    }
                    catch
                    {
                        MessageBox.Show("Nie można uzyskać dostępu do katalogu", "Błąd",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    co = AktywnaSciezka + AktywneOkno.SelectedItems[i].Text +
                        "." + AktywneOkno.SelectedItems[i].SubItems[1].Text;
                    try
                    {
                        File.Delete(co);
                    }
                    catch
                    {
                        MessageBox.Show("Nie można uzyskać dostępu do pliku", "Błąd",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }
            wyswietl(AktywneOkno, AktywnaSciezka);
        }
        
        private void cmdRename_Click(object sender, EventArgs e)
        {            
            string wartosc, zrodlo, cel;
            try
            {
                if (AktywneOkno.SelectedItems[0].SubItems[1].Text == "<DIR>") wartosc = AktywneOkno.SelectedItems[0].Text;
                else wartosc = AktywneOkno.SelectedItems[0].Text +
                        "." + AktywneOkno.SelectedItems[0].SubItems[1].Text;
                if (InputBox("Zmień nazwę", "Podaj nową nazwę:", ref wartosc) == DialogResult.OK)
                {

                    zrodlo = AktywnaSciezka + AktywneOkno.SelectedItems[0].Text +
                        "." + AktywneOkno.SelectedItems[0].SubItems[1].Text;
                    cel = AktywnaSciezka + wartosc;

                    if (AktywneOkno.SelectedItems[0].SubItems[1].Text == "<DIR>")
                    {                       
                        try
                        {
                            zrodlo = AktywnaSciezka + AktywneOkno.SelectedItems[0].Text + "\\";                
                            Directory.Move(zrodlo, cel);
                        }
                        catch
                        {
                            
                            MessageBox.Show("Nie można uzyskać dostępu do katalogu", "Błąd",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        try
                        {                           
                            File.Move(zrodlo, cel);
                        }
                        catch
                        {
                            MessageBox.Show("Nie można uzyskać dostępu do pliku", "Błąd",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Wybierz plik lub katalog", "Błąd",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            wyswietl(AktywneOkno, AktywnaSciezka);
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            string cel, wartosc;
            wartosc = "";
            try
            {
                if (InputBox("Utwórz katalog", "Podaj nazwę katalogu:", ref wartosc) == DialogResult.OK)
                {
                    cel = AktywnaSciezka + wartosc;
                    if (Directory.Exists(cel))
                    {
                        MessageBox.Show("Katalog już istnieje", "Błąd",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Directory.CreateDirectory(cel);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nie można utworzyć katalogu", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                case Keys.F9:
                    cmdRename_Click(null, null);
                    break;
                case Keys.F10:
                    cmdNew_Click(null, null);
                    break;
            }
        }

        private void lvOkno1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != sortColumn1)
            {
                // Set the sort column to the new column.
                sortColumn1 = e.Column;
                // Set the sort order to ascending by default.
                lvOkno1.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (lvOkno1.Sorting == SortOrder.Ascending)
                    lvOkno1.Sorting = SortOrder.Descending;
                else
                    lvOkno1.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            lvOkno1.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            this.lvOkno1.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              lvOkno1.Sorting);
        }
        
        private void lvOkno2_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != sortColumn2)
            {
                // Set the sort column to the new column.
                sortColumn2 = e.Column;
                // Set the sort order to ascending by default.
                lvOkno2.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (lvOkno2.Sorting == SortOrder.Ascending)
                    lvOkno2.Sorting = SortOrder.Descending;
                else
                    lvOkno2.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            lvOkno2.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            this.lvOkno2.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              lvOkno2.Sorting);
        }
                                      
    }

    class ListViewItemComparer : IComparer
    {
        private int col;
        private SortOrder order;
        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }
        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }
            public int Compare(object x, object y) 
            {
            int returnVal= -1;
            try
            {
                returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                        ((ListViewItem)y).SubItems[col].Text);
            }
            catch 
            {
                MessageBox.Show("Czemu ten blad tu jest?!", "Błąd",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            return returnVal;
        }
    }

}
