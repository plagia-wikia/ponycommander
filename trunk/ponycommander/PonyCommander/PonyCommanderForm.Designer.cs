using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PonyCommander
{
    partial class PonyCommanderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvOkno1 = new System.Windows.Forms.ListView();
            this.Nazwa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Typ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Rozmiar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvOkno2 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Dysk1 = new System.Windows.Forms.ComboBox();
            this.Dysk2 = new System.Windows.Forms.ComboBox();
            this.cmdOpen = new System.Windows.Forms.Button();
            this.cmdCopy = new System.Windows.Forms.Button();
            this.cmdMove = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdRename = new System.Windows.Forms.Button();
            this.cmdNew = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvOkno1
            // 
            this.lvOkno1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvOkno1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Nazwa,
            this.Typ,
            this.Rozmiar});
            this.lvOkno1.FullRowSelect = true;
            this.lvOkno1.HideSelection = false;
            this.lvOkno1.Location = new System.Drawing.Point(12, 39);
            this.lvOkno1.Name = "lvOkno1";
            this.lvOkno1.Size = new System.Drawing.Size(376, 482);
            this.lvOkno1.TabIndex = 0;
            this.lvOkno1.UseCompatibleStateImageBehavior = false;
            this.lvOkno1.View = System.Windows.Forms.View.Details;
            this.lvOkno1.Enter += new System.EventHandler(this.lvOkno1_Enter);
            this.lvOkno1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvOkno_KeyDown);
            this.lvOkno1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvOkno_MouseDoubleClick);
            // 
            // Nazwa
            // 
            this.Nazwa.Text = "Nazwa";
            // 
            // Typ
            // 
            this.Typ.Text = "Typ";
            // 
            // Rozmiar
            // 
            this.Rozmiar.Text = "Rozmiar";
            this.Rozmiar.Width = 65;
            // 
            // lvOkno2
            // 
            this.lvOkno2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvOkno2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvOkno2.FullRowSelect = true;
            this.lvOkno2.HideSelection = false;
            this.lvOkno2.Location = new System.Drawing.Point(394, 39);
            this.lvOkno2.Name = "lvOkno2";
            this.lvOkno2.Size = new System.Drawing.Size(376, 482);
            this.lvOkno2.TabIndex = 1;
            this.lvOkno2.UseCompatibleStateImageBehavior = false;
            this.lvOkno2.View = System.Windows.Forms.View.Details;
            this.lvOkno2.Enter += new System.EventHandler(this.lvOkno2_Enter);
            this.lvOkno2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvOkno_KeyDown);
            this.lvOkno2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvOkno_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nazwa";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Typ";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Rozmiar";
            // 
            // Dysk1
            // 
            this.Dysk1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Dysk1.FormattingEnabled = true;
            this.Dysk1.Location = new System.Drawing.Point(12, 12);
            this.Dysk1.Name = "Dysk1";
            this.Dysk1.Size = new System.Drawing.Size(121, 21);
            this.Dysk1.TabIndex = 2;
            this.Dysk1.SelectedIndexChanged += new System.EventHandler(this.Dysk1_SelectedIndexChanged);
            // 
            // Dysk2
            // 
            this.Dysk2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Dysk2.FormattingEnabled = true;
            this.Dysk2.Location = new System.Drawing.Point(394, 12);
            this.Dysk2.Name = "Dysk2";
            this.Dysk2.Size = new System.Drawing.Size(121, 21);
            this.Dysk2.TabIndex = 3;
            this.Dysk2.SelectedIndexChanged += new System.EventHandler(this.Dysk2_SelectedIndexChanged);
            // 
            // cmdOpen
            // 
            this.cmdOpen.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdOpen.Location = new System.Drawing.Point(12, 527);
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.Size = new System.Drawing.Size(120, 23);
            this.cmdOpen.TabIndex = 4;
            this.cmdOpen.Text = "Otworz\r\nF3";
            this.cmdOpen.UseVisualStyleBackColor = true;
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // cmdCopy
            // 
            this.cmdCopy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdCopy.Location = new System.Drawing.Point(138, 527);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(120, 23);
            this.cmdCopy.TabIndex = 5;
            this.cmdCopy.Text = "Kopiuj\r\nF5";
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // cmdMove
            // 
            this.cmdMove.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdMove.Location = new System.Drawing.Point(264, 527);
            this.cmdMove.Name = "cmdMove";
            this.cmdMove.Size = new System.Drawing.Size(120, 23);
            this.cmdMove.TabIndex = 6;
            this.cmdMove.Text = "Przenies\r\nF6";
            this.cmdMove.UseVisualStyleBackColor = true;
            this.cmdMove.Click += new System.EventHandler(this.cmdMove_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdDelete.Location = new System.Drawing.Point(394, 527);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(120, 23);
            this.cmdDelete.TabIndex = 7;
            this.cmdDelete.Text = "Usun\r\nF8";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdRename
            // 
            this.cmdRename.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdRename.Location = new System.Drawing.Point(520, 527);
            this.cmdRename.Name = "cmdRename";
            this.cmdRename.Size = new System.Drawing.Size(120, 23);
            this.cmdRename.TabIndex = 8;
            this.cmdRename.Text = "Zmień nazwę\r\nF9";
            this.cmdRename.UseVisualStyleBackColor = true;
            this.cmdRename.Click += new System.EventHandler(this.cmdRename_Click);
            //
            // cmdNew
            // 
            this.cmdNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdNew.Location = new System.Drawing.Point(646, 527);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(120, 23);
            this.cmdNew.TabIndex = 9;
            this.cmdNew.Text = "Utwórz katalog\r\n10";
            this.cmdNew.UseVisualStyleBackColor = true;
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // PonyCommanderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.cmdNew);
            this.Controls.Add(this.cmdRename);
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.cmdMove);
            this.Controls.Add(this.cmdCopy);
            this.Controls.Add(this.cmdOpen);
            this.Controls.Add(this.Dysk2);
            this.Controls.Add(this.Dysk1);
            this.Controls.Add(this.lvOkno2);
            this.Controls.Add(this.lvOkno1);
            this.KeyPreview = true;
            this.Name = "PonyCommanderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PonyCommander";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PonyCommanderForm_Load);
            this.SizeChanged += new System.EventHandler(this.PonyCommanderForm_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PonyCommanderForm_KeyDown);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvOkno_MouseDoubleClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvOkno1;
        private System.Windows.Forms.ListView lvOkno2;
        private System.Windows.Forms.ColumnHeader Nazwa;
        private System.Windows.Forms.ColumnHeader Typ;
        private System.Windows.Forms.ColumnHeader Rozmiar;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ComboBox Dysk1;
        private System.Windows.Forms.ComboBox Dysk2;
        private System.Windows.Forms.Button cmdOpen;
        private System.Windows.Forms.Button cmdCopy;
        private System.Windows.Forms.Button cmdMove;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Button cmdRename;
        private System.Windows.Forms.Button cmdNew;
    }
}

