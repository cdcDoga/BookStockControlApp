namespace HW2
{
    partial class BookAddUpdateFrm
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
            this.lblBook = new System.Windows.Forms.Label();
            this.tbBookName = new System.Windows.Forms.TextBox();
            this.lblWriter = new System.Windows.Forms.Label();
            this.tbPage = new System.Windows.Forms.TextBox();
            this.lblPage = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.cbWriter = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dtpPublishDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // lblBook
            // 
            this.lblBook.AutoSize = true;
            this.lblBook.Location = new System.Drawing.Point(29, 55);
            this.lblBook.Name = "lblBook";
            this.lblBook.Size = new System.Drawing.Size(39, 16);
            this.lblBook.TabIndex = 0;
            this.lblBook.Text = "Book :";
            // 
            // tbBookName
            // 
            this.tbBookName.Location = new System.Drawing.Point(134, 52);
            this.tbBookName.Name = "tbBookName";
            this.tbBookName.Size = new System.Drawing.Size(199, 21);
            this.tbBookName.TabIndex = 1;
            // 
            // lblWriter
            // 
            this.lblWriter.AutoSize = true;
            this.lblWriter.Location = new System.Drawing.Point(29, 109);
            this.lblWriter.Name = "lblWriter";
            this.lblWriter.Size = new System.Drawing.Size(44, 16);
            this.lblWriter.TabIndex = 2;
            this.lblWriter.Text = "Writer :";
            // 
            // tbPage
            // 
            this.tbPage.Location = new System.Drawing.Point(134, 162);
            this.tbPage.Name = "tbPage";
            this.tbPage.Size = new System.Drawing.Size(199, 21);
            this.tbPage.TabIndex = 5;
            // 
            // lblPage
            // 
            this.lblPage.AutoSize = true;
            this.lblPage.Location = new System.Drawing.Point(29, 165);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(43, 16);
            this.lblPage.TabIndex = 4;
            this.lblPage.Text = "Page :";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(29, 220);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(81, 16);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "Publish Date :";
            // 
            // cbWriter
            // 
            this.cbWriter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWriter.FormattingEnabled = true;
            this.cbWriter.Location = new System.Drawing.Point(134, 106);
            this.cbWriter.Name = "cbWriter";
            this.cbWriter.Size = new System.Drawing.Size(199, 24);
            this.cbWriter.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSubmit.ForeColor = System.Drawing.Color.Firebrick;
            this.btnSubmit.Location = new System.Drawing.Point(255, 280);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(78, 33);
            this.btnSubmit.TabIndex = 10;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // dtpPublishDate
            // 
            this.dtpPublishDate.Location = new System.Drawing.Point(134, 216);
            this.dtpPublishDate.Name = "dtpPublishDate";
            this.dtpPublishDate.Size = new System.Drawing.Size(199, 21);
            this.dtpPublishDate.TabIndex = 7;
            // 
            // BookAddUpdateFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 357);
            this.Controls.Add(this.dtpPublishDate);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cbWriter);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.tbPage);
            this.Controls.Add(this.lblPage);
            this.Controls.Add(this.lblWriter);
            this.Controls.Add(this.tbBookName);
            this.Controls.Add(this.lblBook);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BookAddUpdateFrm";
            this.Text = "addUpdate";
            this.Load += new System.EventHandler(this.addUpdateFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBook;
        private System.Windows.Forms.TextBox tbBookName;
        private System.Windows.Forms.Label lblWriter;
        private System.Windows.Forms.TextBox tbPage;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.ComboBox cbWriter;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DateTimePicker dtpPublishDate;
    }
}