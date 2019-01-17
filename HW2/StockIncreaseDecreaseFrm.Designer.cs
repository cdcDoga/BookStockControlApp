namespace HW2
{
    partial class StockIncreaseDecreaseFrm
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
            this.lblPStock = new System.Windows.Forms.Label();
            this.lblPreviousStockDynamic = new System.Windows.Forms.Label();
            this.lblCStock = new System.Windows.Forms.Label();
            this.tbCurrentStock = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPStock
            // 
            this.lblPStock.AutoSize = true;
            this.lblPStock.Location = new System.Drawing.Point(35, 40);
            this.lblPStock.Name = "lblPStock";
            this.lblPStock.Size = new System.Drawing.Size(91, 16);
            this.lblPStock.TabIndex = 0;
            this.lblPStock.Text = "Previous Stock :";
            // 
            // lblPreviousStockDynamic
            // 
            this.lblPreviousStockDynamic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPreviousStockDynamic.Location = new System.Drawing.Point(156, 39);
            this.lblPreviousStockDynamic.Name = "lblPreviousStockDynamic";
            this.lblPreviousStockDynamic.Size = new System.Drawing.Size(142, 21);
            this.lblPreviousStockDynamic.TabIndex = 1;
            // 
            // lblCStock
            // 
            this.lblCStock.AutoSize = true;
            this.lblCStock.Location = new System.Drawing.Point(35, 102);
            this.lblCStock.Name = "lblCStock";
            this.lblCStock.Size = new System.Drawing.Size(86, 16);
            this.lblCStock.TabIndex = 2;
            this.lblCStock.Text = "Current Stock :";
            // 
            // tbCurrentStock
            // 
            this.tbCurrentStock.Location = new System.Drawing.Point(156, 99);
            this.tbCurrentStock.Name = "tbCurrentStock";
            this.tbCurrentStock.Size = new System.Drawing.Size(142, 21);
            this.tbCurrentStock.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.ForeColor = System.Drawing.Color.Firebrick;
            this.button1.Location = new System.Drawing.Point(192, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 37);
            this.button1.TabIndex = 4;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // StockIncreaseDecreaseFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 230);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbCurrentStock);
            this.Controls.Add(this.lblCStock);
            this.Controls.Add(this.lblPreviousStockDynamic);
            this.Controls.Add(this.lblPStock);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "StockIncreaseDecreaseFrm";
            this.Text = "StockIncreaseDecreaseFrm";
            this.Load += new System.EventHandler(this.StockIncreaseDecreaseFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPStock;
        private System.Windows.Forms.Label lblPreviousStockDynamic;
        private System.Windows.Forms.Label lblCStock;
        private System.Windows.Forms.TextBox tbCurrentStock;
        private System.Windows.Forms.Button button1;
    }
}