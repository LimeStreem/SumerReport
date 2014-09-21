namespace SummerReport
{
    partial class Form2
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
            this.minimum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.maximum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.itr = new System.Windows.Forms.TextBox();
            this.ysc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.xsc = new System.Windows.Forms.TextBox();
            this.savePosMap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // minimum
            // 
            this.minimum.Location = new System.Drawing.Point(160, 12);
            this.minimum.Name = "minimum";
            this.minimum.Size = new System.Drawing.Size(100, 19);
            this.minimum.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Minimum";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Maximum";
            // 
            // maximum
            // 
            this.maximum.Location = new System.Drawing.Point(160, 46);
            this.maximum.Name = "maximum";
            this.maximum.Size = new System.Drawing.Size(100, 19);
            this.maximum.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Iteration";
            // 
            // itr
            // 
            this.itr.Location = new System.Drawing.Point(160, 103);
            this.itr.Name = "itr";
            this.itr.Size = new System.Drawing.Size(100, 19);
            this.itr.TabIndex = 6;
            // 
            // ysc
            // 
            this.ysc.Location = new System.Drawing.Point(160, 186);
            this.ysc.Name = "ysc";
            this.ysc.Size = new System.Drawing.Size(100, 19);
            this.ysc.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "Y-Scaling";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "X-Scaling";
            // 
            // xsc
            // 
            this.xsc.Location = new System.Drawing.Point(160, 152);
            this.xsc.Name = "xsc";
            this.xsc.Size = new System.Drawing.Size(100, 19);
            this.xsc.TabIndex = 7;
            // 
            // savePosMap
            // 
            this.savePosMap.Location = new System.Drawing.Point(99, 226);
            this.savePosMap.Name = "savePosMap";
            this.savePosMap.Size = new System.Drawing.Size(161, 23);
            this.savePosMap.TabIndex = 11;
            this.savePosMap.Text = "save position map";
            this.savePosMap.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.savePosMap);
            this.Controls.Add(this.ysc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.xsc);
            this.Controls.Add(this.itr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.maximum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.minimum);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox minimum;
        public System.Windows.Forms.TextBox maximum;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox itr;
        public System.Windows.Forms.TextBox ysc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox xsc;
        public System.Windows.Forms.Button savePosMap;
    }
}