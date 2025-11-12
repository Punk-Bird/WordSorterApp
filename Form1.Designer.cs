namespace WordSorterApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonSort;
        private System.Windows.Forms.DataGridView dataGridViewResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTimeQuick;
        private System.Windows.Forms.Label labelTimeAbc;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.ComboBox comboBoxWordCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewExperiments;
        private System.Windows.Forms.ComboBox comboBoxTimeUnit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonClearExperiments;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.Button buttonShowChart;
        private System.Windows.Forms.Label label4;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonSort = new System.Windows.Forms.Button();
            this.dataGridViewResults = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTimeQuick = new System.Windows.Forms.Label();
            this.labelTimeAbc = new System.Windows.Forms.Label();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.comboBoxWordCount = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewExperiments = new System.Windows.Forms.DataGridView();
            this.comboBoxTimeUnit = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonClearExperiments = new System.Windows.Forms.Button();
            this.panelChart = new System.Windows.Forms.Panel();
            this.buttonShowChart = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExperiments)).BeginInit();
            this.SuspendLayout();

            // textBoxInput
            this.textBoxInput.Location = new System.Drawing.Point(12, 41);
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInput.Size = new System.Drawing.Size(400, 150);
            this.textBoxInput.TabIndex = 0;

            // buttonSort
            this.buttonSort.Location = new System.Drawing.Point(12, 197);
            this.buttonSort.Name = "buttonSort";
            this.buttonSort.Size = new System.Drawing.Size(120, 30);
            this.buttonSort.TabIndex = 1;
            this.buttonSort.Text = "Сортировать";
            this.buttonSort.UseVisualStyleBackColor = true;
            this.buttonSort.Click += new System.EventHandler(this.buttonSort_Click);

            // dataGridViewResults
            this.dataGridViewResults.Location = new System.Drawing.Point(12, 233);
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.Size = new System.Drawing.Size(400, 200);
            this.dataGridViewResults.TabIndex = 2;

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Введите текст:";

            // labelTimeQuick
            this.labelTimeQuick.AutoSize = true;
            this.labelTimeQuick.Location = new System.Drawing.Point(150, 205);
            this.labelTimeQuick.Name = "labelTimeQuick";
            this.labelTimeQuick.Size = new System.Drawing.Size(118, 15);
            this.labelTimeQuick.TabIndex = 4;
            this.labelTimeQuick.Text = "Быстрая сортировка:";

            // labelTimeAbc
            this.labelTimeAbc.AutoSize = true;
            this.labelTimeAbc.Location = new System.Drawing.Point(300, 205);
            this.labelTimeAbc.Name = "labelTimeAbc";
            this.labelTimeAbc.Size = new System.Drawing.Size(112, 15);
            this.labelTimeAbc.TabIndex = 5;
            this.labelTimeAbc.Text = "ABC-сортировка:";

            // buttonGenerate
            this.buttonGenerate.Location = new System.Drawing.Point(500, 41);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(150, 30);
            this.buttonGenerate.TabIndex = 6;
            this.buttonGenerate.Text = "Сгенерировать текст";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);

            // comboBoxWordCount
            // comboBoxWordCount
            this.comboBoxWordCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWordCount.FormattingEnabled = true;
            this.comboBoxWordCount.Items.AddRange(new object[] {
    "100",
    "200",
    "300",
    "400",
    "500",
    "1000",
    "1500",
    "2000",
    "2500",
    "5000",
    "7500",
    "10000"});
            this.comboBoxWordCount.Location = new System.Drawing.Point(500, 12);
            this.comboBoxWordCount.Name = "comboBoxWordCount";
            this.comboBoxWordCount.Size = new System.Drawing.Size(150, 23);
            this.comboBoxWordCount.TabIndex = 7;

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(430, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Слов для:";

            // dataGridViewExperiments
            this.dataGridViewExperiments.Location = new System.Drawing.Point(430, 80);
            this.dataGridViewExperiments.Name = "dataGridViewExperiments";
            this.dataGridViewExperiments.Size = new System.Drawing.Size(350, 200);
            this.dataGridViewExperiments.TabIndex = 9;

            // comboBoxTimeUnit
            this.comboBoxTimeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeUnit.FormattingEnabled = true;
            this.comboBoxTimeUnit.Items.AddRange(new object[] {
            "Миллисекунды",
            "Микросекунды",
            "Секунды"});
            this.comboBoxTimeUnit.Location = new System.Drawing.Point(680, 12);
            this.comboBoxTimeUnit.Name = "comboBoxTimeUnit";
            this.comboBoxTimeUnit.Size = new System.Drawing.Size(130, 23);
            this.comboBoxTimeUnit.TabIndex = 10;
            this.comboBoxTimeUnit.SelectedIndexChanged += new System.EventHandler(this.comboBoxTimeUnit_SelectedIndexChanged);

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(660, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "в:";

            // buttonClearExperiments
            this.buttonClearExperiments.Location = new System.Drawing.Point(680, 41);
            this.buttonClearExperiments.Name = "buttonClearExperiments";
            this.buttonClearExperiments.Size = new System.Drawing.Size(100, 30);
            this.buttonClearExperiments.TabIndex = 12;
            this.buttonClearExperiments.Text = "Очистить";
            this.buttonClearExperiments.UseVisualStyleBackColor = true;
            this.buttonClearExperiments.Click += new System.EventHandler(this.buttonClearExperiments_Click);

            // panelChart
            this.panelChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChart.Location = new System.Drawing.Point(430, 320);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(350, 200);
            this.panelChart.TabIndex = 13;
            this.panelChart.Paint += new System.Windows.Forms.PaintEventHandler(this.panelChart_Paint);

            // buttonShowChart
            this.buttonShowChart.Location = new System.Drawing.Point(680, 285);
            this.buttonShowChart.Name = "buttonShowChart";
            this.buttonShowChart.Size = new System.Drawing.Size(100, 30);
            this.buttonShowChart.TabIndex = 14;
            this.buttonShowChart.Text = "График";
            this.buttonShowChart.UseVisualStyleBackColor = true;
            this.buttonShowChart.Click += new System.EventHandler(this.buttonShowChart_Click);

            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(430, 293);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(244, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "График зависимости времени от слов:";

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 550);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonShowChart);
            this.Controls.Add(this.panelChart);
            this.Controls.Add(this.buttonClearExperiments);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxTimeUnit);
            this.Controls.Add(this.dataGridViewExperiments);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxWordCount);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.labelTimeAbc);
            this.Controls.Add(this.labelTimeQuick);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewResults);
            this.Controls.Add(this.buttonSort);
            this.Controls.Add(this.textBoxInput);
            this.Name = "Form1";
            this.Text = "Word Sorter Application";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExperiments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}