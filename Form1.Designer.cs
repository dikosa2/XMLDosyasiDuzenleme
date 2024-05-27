using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace XMLFileSelector
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnSelectXML = new System.Windows.Forms.Button();
            this.btnSaveXML = new System.Windows.Forms.Button();
            this.xmlFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.rtbDisplayXML = new System.Windows.Forms.RichTextBox();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.BtnConvertToUTF8 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSelectXML
            // 
            this.btnSelectXML.Location = new System.Drawing.Point(12, 12);
            this.btnSelectXML.Name = "btnSelectXML";
            this.btnSelectXML.Size = new System.Drawing.Size(200, 50);
            this.btnSelectXML.TabIndex = 0;
            this.btnSelectXML.Text = "Dosya Seç";
            this.btnSelectXML.UseVisualStyleBackColor = true;
            this.btnSelectXML.Click += new System.EventHandler(this.BtnSelectXML_Click);
            // 
            // btnSaveXML
            // 
            this.btnSaveXML.Location = new System.Drawing.Point(220, 12);
            this.btnSaveXML.Name = "btnSaveXML";
            this.btnSaveXML.Size = new System.Drawing.Size(200, 50);
            this.btnSaveXML.TabIndex = 1;
            this.btnSaveXML.Text = "Kaydet";
            this.btnSaveXML.UseVisualStyleBackColor = true;
            this.btnSaveXML.Click += new System.EventHandler(this.BtnSaveXML_Click);
            // 
            // rtbDisplayXML
            // 
            this.rtbDisplayXML.Location = new System.Drawing.Point(12, 68);
            this.rtbDisplayXML.Name = "rtbDisplayXML";
            this.rtbDisplayXML.Size = new System.Drawing.Size(776, 370);
            this.rtbDisplayXML.TabIndex = 2;
            this.rtbDisplayXML.Text = "";
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(426, 12);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(190, 50);
            this.BtnRefresh.TabIndex = 3;
            this.BtnRefresh.Text = "Yenile";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnConvertToUTF8
            // 
            this.BtnConvertToUTF8.Location = new System.Drawing.Point(622, 12);
            this.BtnConvertToUTF8.Name = "BtnConvertToUTF8";
            this.BtnConvertToUTF8.Size = new System.Drawing.Size(166, 50);
            this.BtnConvertToUTF8.TabIndex = 4;
            this.BtnConvertToUTF8.Text = "UTF-8 Dönüştürücü ";
            this.BtnConvertToUTF8.UseVisualStyleBackColor = true;
            this.BtnConvertToUTF8.Click += new System.EventHandler(this.BtnConvertToUTF8_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnConvertToUTF8);
            this.Controls.Add(this.BtnRefresh);
            this.Controls.Add(this.btnSaveXML);
            this.Controls.Add(this.btnSelectXML);
            this.Controls.Add(this.rtbDisplayXML);
            this.Name = "Form1";
            this.Text = "XML File Selector";
            this.ResumeLayout(false);
            //
            //
            //

        }

        private Button btnSelectXML;
        private Button btnSaveXML;
        private OpenFileDialog xmlFileDialog;
        private RichTextBox rtbDisplayXML;
        private Button BtnRefresh;
        private Button BtnConvertToUTF8;
    }
}
