namespace ProxiesGrabber
{
    partial class ProxiesGrabberForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.ResultrichTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lblProxiesCounter = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(-1, -1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 30);
            this.label2.TabIndex = 16;
            this.label2.Text = "Proxies Grabber";
            // 
            // ResultrichTextBox1
            // 
            this.ResultrichTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(26)))));
            this.ResultrichTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ResultrichTextBox1.Location = new System.Drawing.Point(19, 35);
            this.ResultrichTextBox1.Name = "ResultrichTextBox1";
            this.ResultrichTextBox1.ReadOnly = true;
            this.ResultrichTextBox1.Size = new System.Drawing.Size(575, 159);
            this.ResultrichTextBox1.TabIndex = 17;
            this.ResultrichTextBox1.Text = "";
            // 
            // lblProxiesCounter
            // 
            this.lblProxiesCounter.AutoSize = true;
            this.lblProxiesCounter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(34)))), ((int)(((byte)(36)))));
            this.lblProxiesCounter.Location = new System.Drawing.Point(243, 195);
            this.lblProxiesCounter.Name = "lblProxiesCounter";
            this.lblProxiesCounter.Size = new System.Drawing.Size(102, 13);
            this.lblProxiesCounter.TabIndex = 19;
            this.lblProxiesCounter.Text = "Grabbed : 0 Proxies";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(5, 244);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(90, 13);
            this.lblTime.TabIndex = 20;
            this.lblTime.Text = "Timer :  00:00:00";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(26)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(194, 213);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 35);
            this.button2.TabIndex = 18;
            this.button2.Text = "Run";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ProxiesGrabberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(34)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(619, 265);
            this.Controls.Add(this.ResultrichTextBox1);
            this.Controls.Add(this.lblProxiesCounter);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProxiesGrabberForm";
            this.Text = "ProxiesForm";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox ResultrichTextBox1;
        private System.Windows.Forms.Label lblProxiesCounter;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button button2;
    }
}