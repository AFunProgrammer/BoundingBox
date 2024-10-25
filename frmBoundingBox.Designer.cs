namespace BoundingBox
{
    partial class frmBoundingBox
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
            picBoundingBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picBoundingBox).BeginInit();
            SuspendLayout();
            // 
            // picBoundingBox
            // 
            picBoundingBox.Location = new Point(12, 12);
            picBoundingBox.Name = "picBoundingBox";
            picBoundingBox.Size = new Size(635, 607);
            picBoundingBox.TabIndex = 0;
            picBoundingBox.TabStop = false;
            // 
            // frmBoundingBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.AppWorkspace;
            ClientSize = new Size(659, 631);
            Controls.Add(picBoundingBox);
            Name = "frmBoundingBox";
            Text = "Bounding Box";
            Load += frmBoundingBox_Load;
            ((System.ComponentModel.ISupportInitialize)picBoundingBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picBoundingBox;
    }
}
