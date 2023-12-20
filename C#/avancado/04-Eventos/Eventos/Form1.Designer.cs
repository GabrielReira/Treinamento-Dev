namespace Eventos
{
    partial class frmGerenciador
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
            this.pgbIntensidadeLatido = new System.Windows.Forms.ProgressBar();
            this.btnLatir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pgbIntensidadeLatido
            // 
            this.pgbIntensidadeLatido.Location = new System.Drawing.Point(12, 41);
            this.pgbIntensidadeLatido.Name = "pgbIntensidadeLatido";
            this.pgbIntensidadeLatido.Size = new System.Drawing.Size(260, 23);
            this.pgbIntensidadeLatido.TabIndex = 0;
            // 
            // btnLatir
            // 
            this.btnLatir.Location = new System.Drawing.Point(12, 12);
            this.btnLatir.Name = "btnLatir";
            this.btnLatir.Size = new System.Drawing.Size(75, 23);
            this.btnLatir.TabIndex = 1;
            this.btnLatir.Text = "Latir!";
            this.btnLatir.UseVisualStyleBackColor = true;
            this.btnLatir.Click += new System.EventHandler(this.btnLatir_Click);
            // 
            // frmGerenciador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 78);
            this.Controls.Add(this.btnLatir);
            this.Controls.Add(this.pgbIntensidadeLatido);
            this.Name = "frmGerenciador";
            this.Text = "Gerenciador de Latidos";
            this.Load += new System.EventHandler(this.frmGerenciador_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgbIntensidadeLatido;
        private System.Windows.Forms.Button btnLatir;
    }
}

