namespace thecicadassong
{
    partial class FormPrologue
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
            components = new System.ComponentModel.Container();
            prologue_text = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // prologue_text
            // 
            prologue_text.Anchor = AnchorStyles.None;
            prologue_text.AutoSize = true;
            prologue_text.BackColor = SystemColors.ActiveCaptionText;
            prologue_text.Font = new Font("Georgia", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            prologue_text.ForeColor = SystemColors.ButtonFace;
            prologue_text.Location = new Point(421, 252);
            prologue_text.Name = "prologue_text";
            prologue_text.Size = new Size(254, 72);
            prologue_text.TabIndex = 0;
            prologue_text.Text = "Пролог";
            // 
            // timer1
            // 
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            // 
            // FormPrologue
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1008, 729);
            Controls.Add(prologue_text);
            Name = "FormPrologue";
            Text = "FormPrologue";
            Load += FormPrologue_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label prologue_text;
        private System.Windows.Forms.Timer timer1;
    }
}