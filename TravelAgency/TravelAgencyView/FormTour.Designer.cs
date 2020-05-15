namespace TravelAgencyView
{
    partial class FormTour
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelCountry = new System.Windows.Forms.Label();
            this.labelDuration = new System.Windows.Forms.Label();
            this.labelTypeOfAllocation = new System.Windows.Forms.Label();
            this.labelCost = new System.Windows.Forms.Label();
            this.textBoxCountry = new System.Windows.Forms.TextBox();
            this.textBoxDuration = new System.Windows.Forms.TextBox();
            this.textBoxTypeOfAllocation = new System.Windows.Forms.TextBox();
            this.textBoxCost = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(152, 16);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(241, 20);
            this.textBoxName.TabIndex = 0;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(16, 16);
            this.labelName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(57, 13);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Название";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(182, 161);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(73, 24);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(301, 161);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(73, 24);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelCountry
            // 
            this.labelCountry.AutoSize = true;
            this.labelCountry.Location = new System.Drawing.Point(16, 40);
            this.labelCountry.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCountry.Name = "labelCountry";
            this.labelCountry.Size = new System.Drawing.Size(43, 13);
            this.labelCountry.TabIndex = 4;
            this.labelCountry.Text = "Страна";
            // 
            // labelDuration
            // 
            this.labelDuration.AutoSize = true;
            this.labelDuration.Location = new System.Drawing.Point(16, 62);
            this.labelDuration.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDuration.Name = "labelDuration";
            this.labelDuration.Size = new System.Drawing.Size(80, 13);
            this.labelDuration.TabIndex = 5;
            this.labelDuration.Text = "Длительность";
            // 
            // labelTypeOfAllocation
            // 
            this.labelTypeOfAllocation.AutoSize = true;
            this.labelTypeOfAllocation.Location = new System.Drawing.Point(16, 89);
            this.labelTypeOfAllocation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTypeOfAllocation.Name = "labelTypeOfAllocation";
            this.labelTypeOfAllocation.Size = new System.Drawing.Size(94, 13);
            this.labelTypeOfAllocation.TabIndex = 6;
            this.labelTypeOfAllocation.Text = "Тип размещения";
            // 
            // labelCost
            // 
            this.labelCost.AutoSize = true;
            this.labelCost.Location = new System.Drawing.Point(16, 117);
            this.labelCost.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCost.Name = "labelCost";
            this.labelCost.Size = new System.Drawing.Size(33, 13);
            this.labelCost.TabIndex = 7;
            this.labelCost.Text = "Цена";
            // 
            // textBoxCountry
            // 
            this.textBoxCountry.Location = new System.Drawing.Point(152, 40);
            this.textBoxCountry.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCountry.Name = "textBoxCountry";
            this.textBoxCountry.Size = new System.Drawing.Size(241, 20);
            this.textBoxCountry.TabIndex = 8;
            // 
            // textBoxDuration
            // 
            this.textBoxDuration.Location = new System.Drawing.Point(152, 65);
            this.textBoxDuration.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxDuration.Name = "textBoxDuration";
            this.textBoxDuration.Size = new System.Drawing.Size(241, 20);
            this.textBoxDuration.TabIndex = 9;
            // 
            // textBoxTypeOfAllocation
            // 
            this.textBoxTypeOfAllocation.Location = new System.Drawing.Point(152, 93);
            this.textBoxTypeOfAllocation.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTypeOfAllocation.Name = "textBoxTypeOfAllocation";
            this.textBoxTypeOfAllocation.Size = new System.Drawing.Size(241, 20);
            this.textBoxTypeOfAllocation.TabIndex = 10;
            // 
            // textBoxCost
            // 
            this.textBoxCost.Location = new System.Drawing.Point(152, 117);
            this.textBoxCost.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCost.Name = "textBoxCost";
            this.textBoxCost.Size = new System.Drawing.Size(241, 20);
            this.textBoxCost.TabIndex = 11;
            // 
            // FormTour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 190);
            this.Controls.Add(this.textBoxCost);
            this.Controls.Add(this.textBoxTypeOfAllocation);
            this.Controls.Add(this.textBoxDuration);
            this.Controls.Add(this.textBoxCountry);
            this.Controls.Add(this.labelCost);
            this.Controls.Add(this.labelTypeOfAllocation);
            this.Controls.Add(this.labelDuration);
            this.Controls.Add(this.labelCountry);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormTour";
            this.Text = "Тур";
            this.Load += new System.EventHandler(this.FormTour_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelCountry;
        private System.Windows.Forms.Label labelDuration;
        private System.Windows.Forms.Label labelTypeOfAllocation;
        private System.Windows.Forms.Label labelCost;
        private System.Windows.Forms.TextBox textBoxCountry;
        private System.Windows.Forms.TextBox textBoxDuration;
        private System.Windows.Forms.TextBox textBoxTypeOfAllocation;
        private System.Windows.Forms.TextBox textBoxCost;
    }
}