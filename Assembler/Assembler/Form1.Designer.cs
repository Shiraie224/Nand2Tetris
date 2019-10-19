namespace Assembler
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.Start = new System.Windows.Forms.Button();
            this.TestBox = new System.Windows.Forms.TextBox();
            this.Debug = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(36, 127);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(140, 85);
            this.Start.TabIndex = 0;
            this.Start.Text = "レッツアセンブル!";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // TestBox
            // 
            this.TestBox.Location = new System.Drawing.Point(243, 73);
            this.TestBox.MaxLength = 0;
            this.TestBox.Multiline = true;
            this.TestBox.Name = "TestBox";
            this.TestBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TestBox.Size = new System.Drawing.Size(530, 203);
            this.TestBox.TabIndex = 1;
            // 
            // Debug
            // 
            this.Debug.AutoSize = true;
            this.Debug.Location = new System.Drawing.Point(268, 58);
            this.Debug.Name = "Debug";
            this.Debug.Size = new System.Drawing.Size(60, 12);
            this.Debug.TabIndex = 2;
            this.Debug.Text = "DebugText";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Git用テスト";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Debug);
            this.Controls.Add(this.TestBox);
            this.Controls.Add(this.Start);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.TextBox TestBox;
        private System.Windows.Forms.Label Debug;
        private System.Windows.Forms.Label label1;
    }
}

