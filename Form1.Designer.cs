namespace ClientSettingsLoader
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.Run_Btn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.Export_file_btn = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.Import_game_btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ResetTokens_Btn = new System.Windows.Forms.Button();
            this.GetSkins = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(77, 74);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 47);
            this.button1.TabIndex = 0;
            this.button1.Text = "Import Input Profile";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Run_Btn
            // 
            this.Run_Btn.BackColor = System.Drawing.Color.White;
            this.Run_Btn.Location = new System.Drawing.Point(77, 291);
            this.Run_Btn.Name = "Run_Btn";
            this.Run_Btn.Size = new System.Drawing.Size(209, 31);
            this.Run_Btn.TabIndex = 1;
            this.Run_Btn.Text = "Set client settings";
            this.Run_Btn.UseVisualStyleBackColor = false;
            this.Run_Btn.Click += new System.EventHandler(this.Run_Btn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 409);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // Export_file_btn
            // 
            this.Export_file_btn.BackColor = System.Drawing.Color.White;
            this.Export_file_btn.Location = new System.Drawing.Point(191, 74);
            this.Export_file_btn.Name = "Export_file_btn";
            this.Export_file_btn.Size = new System.Drawing.Size(95, 47);
            this.Export_file_btn.TabIndex = 3;
            this.Export_file_btn.Text = "Export Profiles";
            this.Export_file_btn.UseVisualStyleBackColor = false;
            this.Export_file_btn.Click += new System.EventHandler(this.Export_file_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(77, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "No File";
            // 
            // Import_game_btn
            // 
            this.Import_game_btn.BackColor = System.Drawing.Color.White;
            this.Import_game_btn.Location = new System.Drawing.Point(77, 142);
            this.Import_game_btn.Name = "Import_game_btn";
            this.Import_game_btn.Size = new System.Drawing.Size(95, 47);
            this.Import_game_btn.TabIndex = 5;
            this.Import_game_btn.Text = "Import General profile";
            this.Import_game_btn.UseVisualStyleBackColor = false;
            this.Import_game_btn.Click += new System.EventHandler(this.Import_game_btn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "No File";
            // 
            // ResetTokens_Btn
            // 
            this.ResetTokens_Btn.BackColor = System.Drawing.Color.White;
            this.ResetTokens_Btn.ForeColor = System.Drawing.Color.Black;
            this.ResetTokens_Btn.Location = new System.Drawing.Point(191, 142);
            this.ResetTokens_Btn.Name = "ResetTokens_Btn";
            this.ResetTokens_Btn.Size = new System.Drawing.Size(95, 47);
            this.ResetTokens_Btn.TabIndex = 7;
            this.ResetTokens_Btn.Text = "Reset Challenges";
            this.ResetTokens_Btn.UseVisualStyleBackColor = false;
            this.ResetTokens_Btn.Click += new System.EventHandler(this.ResetTokens_Btn_Click);
            // 
            // GetSkins
            // 
            this.GetSkins.BackColor = System.Drawing.Color.White;
            this.GetSkins.Location = new System.Drawing.Point(211, 218);
            this.GetSkins.Name = "GetSkins";
            this.GetSkins.Size = new System.Drawing.Size(75, 23);
            this.GetSkins.TabIndex = 8;
            this.GetSkins.Text = "Load Skins";
            this.GetSkins.UseVisualStyleBackColor = false;
            this.GetSkins.Click += new System.EventHandler(this.GetSkins_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(77, 218);
            this.textBox1.Name = "textBox1";
            this.textBox1.PlaceholderText = "Champion Name";
            this.textBox1.Size = new System.Drawing.Size(128, 23);
            this.textBox1.TabIndex = 9;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(77, 247);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(209, 23);
            this.comboBox1.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(366, 450);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.GetSkins);
            this.Controls.Add(this.ResetTokens_Btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Import_game_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Export_file_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Run_Btn);
            this.Controls.Add(this.button1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Client Settings Loader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private Button Run_Btn;
        private OpenFileDialog openFileDialog1;
        private Label label1;
        private Button Export_file_btn;
        private SaveFileDialog saveFileDialog1;
        private Label label2;
        private Button Import_game_btn;
        private Label label3;
        private Button ResetTokens_Btn;
        private Button GetSkins;
        private TextBox textBox1;
        private ComboBox comboBox1;
    }
}