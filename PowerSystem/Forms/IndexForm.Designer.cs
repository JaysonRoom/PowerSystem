namespace PowerSystem.Forms
{
    partial class IndexForm
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
            this.btnPower1 = new System.Windows.Forms.Button();
            this.btnPower2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPower1
            // 
            this.btnPower1.Font = new System.Drawing.Font("宋体", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPower1.Location = new System.Drawing.Point(429, 198);
            this.btnPower1.Name = "btnPower1";
            this.btnPower1.Size = new System.Drawing.Size(102, 40);
            this.btnPower1.TabIndex = 0;
            this.btnPower1.Text = "电源1";
            this.btnPower1.UseVisualStyleBackColor = true;
            this.btnPower1.Visible = false;
            this.btnPower1.Click += new System.EventHandler(this.btnPower1_Click);
            // 
            // btnPower2
            // 
            this.btnPower2.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
            this.btnPower2.Location = new System.Drawing.Point(591, 198);
            this.btnPower2.Name = "btnPower2";
            this.btnPower2.Size = new System.Drawing.Size(102, 40);
            this.btnPower2.TabIndex = 1;
            this.btnPower2.Text = "电源2";
            this.btnPower2.UseVisualStyleBackColor = true;
            this.btnPower2.Visible = false;
            this.btnPower2.Click += new System.EventHandler(this.btnPower2_Click);
            // 
            // IndexForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PowerSystem.Properties.Resources._200434391;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(854, 540);
            this.ControlBox = false;
            this.Controls.Add(this.btnPower2);
            this.Controls.Add(this.btnPower1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IndexForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPower1;
        private System.Windows.Forms.Button btnPower2;
    }
}