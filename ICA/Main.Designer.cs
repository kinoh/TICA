namespace ICA
{
    partial class Main
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
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
            this.textboxSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textboxPatches = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textboxSamples = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxDirectory = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textboxRate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonInit = new System.Windows.Forms.Button();
            this.textboxOutput = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonOutput = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonAnalyze = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textboxCutoff = new System.Windows.Forms.TextBox();
            this.picturePatch = new System.Windows.Forms.PictureBox();
            this.textboxImage = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonSample = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picturePatch)).BeginInit();
            this.SuspendLayout();
            // 
            // textboxSize
            // 
            this.textboxSize.Location = new System.Drawing.Point(126, 12);
            this.textboxSize.Name = "textboxSize";
            this.textboxSize.Size = new System.Drawing.Size(137, 22);
            this.textboxSize.TabIndex = 1;
            this.textboxSize.Text = "12";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "size";
            // 
            // textboxPatches
            // 
            this.textboxPatches.Location = new System.Drawing.Point(126, 40);
            this.textboxPatches.Name = "textboxPatches";
            this.textboxPatches.Size = new System.Drawing.Size(137, 22);
            this.textboxPatches.TabIndex = 3;
            this.textboxPatches.Text = "20000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "patches";
            // 
            // textboxSamples
            // 
            this.textboxSamples.Location = new System.Drawing.Point(126, 68);
            this.textboxSamples.Name = "textboxSamples";
            this.textboxSamples.Size = new System.Drawing.Size(137, 22);
            this.textboxSamples.TabIndex = 5;
            this.textboxSamples.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "samples";
            // 
            // textboxDirectory
            // 
            this.textboxDirectory.Location = new System.Drawing.Point(126, 96);
            this.textboxDirectory.Name = "textboxDirectory";
            this.textboxDirectory.Size = new System.Drawing.Size(137, 22);
            this.textboxDirectory.TabIndex = 7;
            this.textboxDirectory.Text = "D:/img/MIRFLICKR/images";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "directory";
            // 
            // textboxRate
            // 
            this.textboxRate.Location = new System.Drawing.Point(126, 152);
            this.textboxRate.Name = "textboxRate";
            this.textboxRate.Size = new System.Drawing.Size(137, 22);
            this.textboxRate.TabIndex = 11;
            this.textboxRate.Text = "0.001";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "rate";
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(188, 180);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(75, 23);
            this.buttonRun.TabIndex = 13;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonInit
            // 
            this.buttonInit.Location = new System.Drawing.Point(107, 180);
            this.buttonInit.Name = "buttonInit";
            this.buttonInit.Size = new System.Drawing.Size(75, 23);
            this.buttonInit.TabIndex = 12;
            this.buttonInit.Text = "Initialize";
            this.buttonInit.UseVisualStyleBackColor = true;
            this.buttonInit.Click += new System.EventHandler(this.buttonInit_Click);
            // 
            // textboxOutput
            // 
            this.textboxOutput.Location = new System.Drawing.Point(126, 124);
            this.textboxOutput.Name = "textboxOutput";
            this.textboxOutput.Size = new System.Drawing.Size(137, 22);
            this.textboxOutput.TabIndex = 9;
            this.textboxOutput.Text = "D:/ICA/filters.txt";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "outut";
            // 
            // buttonOutput
            // 
            this.buttonOutput.Location = new System.Drawing.Point(188, 209);
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.Size = new System.Drawing.Size(75, 23);
            this.buttonOutput.TabIndex = 15;
            this.buttonOutput.Text = "Output";
            this.buttonOutput.UseVisualStyleBackColor = true;
            this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(107, 302);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 24;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonAnalyze
            // 
            this.buttonAnalyze.Location = new System.Drawing.Point(188, 302);
            this.buttonAnalyze.Name = "buttonAnalyze";
            this.buttonAnalyze.Size = new System.Drawing.Size(75, 23);
            this.buttonAnalyze.TabIndex = 25;
            this.buttonAnalyze.Text = "Analyze";
            this.buttonAnalyze.UseVisualStyleBackColor = true;
            this.buttonAnalyze.Click += new System.EventHandler(this.buttonAnalyze_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 277);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 15);
            this.label7.TabIndex = 22;
            this.label7.Text = "cutoff";
            // 
            // textboxCutoff
            // 
            this.textboxCutoff.Location = new System.Drawing.Point(126, 274);
            this.textboxCutoff.Name = "textboxCutoff";
            this.textboxCutoff.Size = new System.Drawing.Size(137, 22);
            this.textboxCutoff.TabIndex = 23;
            this.textboxCutoff.Text = "5";
            // 
            // picturePatch
            // 
            this.picturePatch.Location = new System.Drawing.Point(231, 331);
            this.picturePatch.Name = "picturePatch";
            this.picturePatch.Size = new System.Drawing.Size(32, 32);
            this.picturePatch.TabIndex = 20;
            this.picturePatch.TabStop = false;
            // 
            // textboxImage
            // 
            this.textboxImage.Location = new System.Drawing.Point(126, 246);
            this.textboxImage.Name = "textboxImage";
            this.textboxImage.Size = new System.Drawing.Size(137, 22);
            this.textboxImage.TabIndex = 21;
            this.textboxImage.Text = "D:/img/MIRFLICKR/images/im2.jpg";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 249);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 15);
            this.label8.TabIndex = 20;
            this.label8.Text = "image";
            // 
            // buttonSample
            // 
            this.buttonSample.Location = new System.Drawing.Point(107, 209);
            this.buttonSample.Name = "buttonSample";
            this.buttonSample.Size = new System.Drawing.Size(75, 23);
            this.buttonSample.TabIndex = 16;
            this.buttonSample.Text = "Sample";
            this.buttonSample.UseVisualStyleBackColor = true;
            this.buttonSample.Click += new System.EventHandler(this.buttonSample_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 437);
            this.Controls.Add(this.buttonSample);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textboxImage);
            this.Controls.Add(this.picturePatch);
            this.Controls.Add(this.textboxCutoff);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonAnalyze);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonOutput);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textboxOutput);
            this.Controls.Add(this.buttonInit);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textboxRate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textboxDirectory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxSamples);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textboxPatches);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textboxSize);
            this.Name = "Main";
            this.Text = "ICA";
            ((System.ComponentModel.ISupportInitialize)(this.picturePatch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textboxSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textboxPatches;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textboxSamples;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textboxDirectory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textboxRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonInit;
        private System.Windows.Forms.TextBox textboxOutput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonOutput;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonAnalyze;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textboxCutoff;
        private System.Windows.Forms.PictureBox picturePatch;
        private System.Windows.Forms.TextBox textboxImage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonSample;
    }
}

