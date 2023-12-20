namespace Lambda
{
    partial class frmLambda
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txbTexto1 = new System.Windows.Forms.TextBox();
            this.txbTexto2 = new System.Windows.Forms.TextBox();
            this.btnConcatenar = new System.Windows.Forms.Button();
            this.txbResultado = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Texto 1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Texto 2:";
            // 
            // txbTexto1
            // 
            this.txbTexto1.Location = new System.Drawing.Point(76, 6);
            this.txbTexto1.Name = "txbTexto1";
            this.txbTexto1.Size = new System.Drawing.Size(196, 20);
            this.txbTexto1.TabIndex = 2;
            // 
            // txbTexto2
            // 
            this.txbTexto2.Location = new System.Drawing.Point(76, 37);
            this.txbTexto2.Name = "txbTexto2";
            this.txbTexto2.Size = new System.Drawing.Size(196, 20);
            this.txbTexto2.TabIndex = 3;
            // 
            // btnConcatenar
            // 
            this.btnConcatenar.Location = new System.Drawing.Point(76, 63);
            this.btnConcatenar.Name = "btnConcatenar";
            this.btnConcatenar.Size = new System.Drawing.Size(75, 23);
            this.btnConcatenar.TabIndex = 4;
            this.btnConcatenar.Text = "Concatenar";
            this.btnConcatenar.UseVisualStyleBackColor = true;
            this.btnConcatenar.Click += new System.EventHandler(this.btnConcatenar_Click);
            // 
            // txbResultado
            // 
            this.txbResultado.Location = new System.Drawing.Point(76, 92);
            this.txbResultado.Name = "txbResultado";
            this.txbResultado.ReadOnly = true;
            this.txbResultado.Size = new System.Drawing.Size(196, 20);
            this.txbResultado.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Resultado:";
            // 
            // frmLambda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 130);
            this.Controls.Add(this.txbResultado);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnConcatenar);
            this.Controls.Add(this.txbTexto2);
            this.Controls.Add(this.txbTexto1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmLambda";
            this.Text = "Concatenar Strings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbTexto1;
        private System.Windows.Forms.TextBox txbTexto2;
        private System.Windows.Forms.Button btnConcatenar;
        private System.Windows.Forms.TextBox txbResultado;
        private System.Windows.Forms.Label label3;
    }
}

