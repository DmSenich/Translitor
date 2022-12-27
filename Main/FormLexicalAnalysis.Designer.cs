namespace Main
{
    partial class FormLexicalAnalysis
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.StringsCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bLoad = new System.Windows.Forms.Button();
            this.bCrTb = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.bOperate = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StringsCol,
            this.NameCol});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(410, 426);
            this.dataGridView1.TabIndex = 0;
            // 
            // StringsCol
            // 
            this.StringsCol.HeaderText = "Лексема";
            this.StringsCol.MinimumWidth = 6;
            this.StringsCol.Name = "StringsCol";
            this.StringsCol.Width = 125;
            // 
            // NameCol
            // 
            this.NameCol.HeaderText = "Тип лексемы";
            this.NameCol.MinimumWidth = 6;
            this.NameCol.Name = "NameCol";
            this.NameCol.Width = 125;
            // 
            // bLoad
            // 
            this.bLoad.Location = new System.Drawing.Point(448, 12);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(137, 56);
            this.bLoad.TabIndex = 1;
            this.bLoad.Text = "Загрузить текст программы";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // bCrTb
            // 
            this.bCrTb.Location = new System.Drawing.Point(741, 12);
            this.bCrTb.Name = "bCrTb";
            this.bCrTb.Size = new System.Drawing.Size(140, 56);
            this.bCrTb.TabIndex = 2;
            this.bCrTb.Text = "Анализ синтаксический";
            this.bCrTb.UseVisualStyleBackColor = true;
            this.bCrTb.Click += new System.EventHandler(this.bCrTb_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.AcceptsTab = true;
            this.richTextBox1.Location = new System.Drawing.Point(448, 74);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(433, 364);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // bOperate
            // 
            this.bOperate.Location = new System.Drawing.Point(591, 12);
            this.bOperate.Name = "bOperate";
            this.bOperate.Size = new System.Drawing.Size(144, 56);
            this.bOperate.TabIndex = 4;
            this.bOperate.Text = "Анализ лексический";
            this.bOperate.UseVisualStyleBackColor = true;
            this.bOperate.Click += new System.EventHandler(this.bOperate_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormLexicalAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(941, 450);
            this.Controls.Add(this.bOperate);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.bCrTb);
            this.Controls.Add(this.bLoad);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormLexicalAnalysis";
            this.Text = "Лексический анализатор";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn StringsCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameCol;
        private System.Windows.Forms.Button bCrTb;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button bOperate;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

