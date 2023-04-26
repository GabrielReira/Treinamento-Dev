namespace Enumeradores
{
    partial class frmEnums
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
            this.cmbDias = new System.Windows.Forms.ComboBox();
            this.btnExibirDia = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbDias
            // 
            this.cmbDias.FormattingEnabled = true;
            this.cmbDias.Location = new System.Drawing.Point(28, 12);
            this.cmbDias.Name = "cmbDias";
            this.cmbDias.Size = new System.Drawing.Size(121, 21);
            this.cmbDias.TabIndex = 0;
            // 
            // btnExibirDia
            // 
            this.btnExibirDia.Location = new System.Drawing.Point(172, 10);
            this.btnExibirDia.Name = "btnExibirDia";
            this.btnExibirDia.Size = new System.Drawing.Size(75, 23);
            this.btnExibirDia.TabIndex = 1;
            this.btnExibirDia.Text = "OK";
            this.btnExibirDia.UseVisualStyleBackColor = true;
            this.btnExibirDia.Click += new System.EventHandler(this.btnExibirDia_Click);
            // 
            // frmEnums
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnExibirDia);
            this.Controls.Add(this.cmbDias);
            this.Name = "frmEnums";
            this.Text = "Enumeradores";
            this.Shown += new System.EventHandler(this.frmEnums_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDias;
        private System.Windows.Forms.Button btnExibirDia;
    }
}

