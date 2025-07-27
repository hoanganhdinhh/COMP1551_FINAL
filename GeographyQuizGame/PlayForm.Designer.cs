using System.Drawing;
using System.Windows.Forms;

namespace GeographyQuizGame
{
    partial class PlayForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Panel panelChoices;
        private System.Windows.Forms.RadioButton radioA;
        private System.Windows.Forms.RadioButton radioB;
        private System.Windows.Forms.RadioButton radioC;
        private System.Windows.Forms.RadioButton radioD;
        private System.Windows.Forms.TextBox txtAnswer;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Panel panelTrueFalse;
        private System.Windows.Forms.RadioButton radioTrue;
        private System.Windows.Forms.RadioButton radioFalse;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblQuestion = new System.Windows.Forms.Label();
            this.panelChoices = new System.Windows.Forms.Panel();
            this.radioA = new System.Windows.Forms.RadioButton();
            this.radioB = new System.Windows.Forms.RadioButton();
            this.radioC = new System.Windows.Forms.RadioButton();
            this.radioD = new System.Windows.Forms.RadioButton();
            this.txtAnswer = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.panelTrueFalse = new System.Windows.Forms.Panel();
            this.radioTrue = new System.Windows.Forms.RadioButton();
            this.radioFalse = new System.Windows.Forms.RadioButton();
            this.panelChoices.SuspendLayout();
            this.panelTrueFalse.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblQuestion.Location = new System.Drawing.Point(24, 30);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(77, 20);
            this.lblQuestion.TabIndex = 1;
            this.lblQuestion.Text = "Question:";
            this.lblQuestion.Click += new System.EventHandler(this.lblQuestion_Click);
            // 
            // panelChoices
            // 
            this.panelChoices.BackColor = System.Drawing.Color.White;
            this.panelChoices.Controls.Add(this.radioA);
            this.panelChoices.Controls.Add(this.radioB);
            this.panelChoices.Controls.Add(this.radioC);
            this.panelChoices.Controls.Add(this.radioD);
            this.panelChoices.Location = new System.Drawing.Point(26, 115);
            this.panelChoices.Name = "panelChoices";
            this.panelChoices.Size = new System.Drawing.Size(500, 135);
            this.panelChoices.TabIndex = 2;
            this.panelChoices.Visible = false;
            this.panelChoices.Paint += new System.Windows.Forms.PaintEventHandler(this.panelChoices_Paint);
            // 
            // radioA
            // 
            this.radioA.BackColor = System.Drawing.Color.White;
            this.radioA.Location = new System.Drawing.Point(10, 10);
            this.radioA.Name = "radioA";
            this.radioA.Size = new System.Drawing.Size(480, 24);
            this.radioA.TabIndex = 0;
            this.radioA.TabStop = true;
            this.radioA.Text = "A.";
            this.radioA.UseVisualStyleBackColor = false;
            // 
            // radioB
            // 
            this.radioB.BackColor = System.Drawing.Color.White;
            this.radioB.Location = new System.Drawing.Point(10, 40);
            this.radioB.Name = "radioB";
            this.radioB.Size = new System.Drawing.Size(480, 24);
            this.radioB.TabIndex = 1;
            this.radioB.TabStop = true;
            this.radioB.Text = "B.";
            this.radioB.UseVisualStyleBackColor = false;
            // 
            // radioC
            // 
            this.radioC.Location = new System.Drawing.Point(10, 70);
            this.radioC.Name = "radioC";
            this.radioC.Size = new System.Drawing.Size(480, 24);
            this.radioC.TabIndex = 2;
            this.radioC.TabStop = true;
            this.radioC.Text = "C.";
            this.radioC.UseVisualStyleBackColor = true;
            // 
            // radioD
            // 
            this.radioD.Location = new System.Drawing.Point(10, 100);
            this.radioD.Name = "radioD";
            this.radioD.Size = new System.Drawing.Size(480, 24);
            this.radioD.TabIndex = 3;
            this.radioD.TabStop = true;
            this.radioD.Text = "D.";
            this.radioD.UseVisualStyleBackColor = true;
            // 
            // txtAnswer
            // 
            this.txtAnswer.Location = new System.Drawing.Point(26, 83);
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.Size = new System.Drawing.Size(500, 22);
            this.txtAnswer.TabIndex = 3;
            this.txtAnswer.Visible = false;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(376, 278);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(150, 35);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit Answer";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // panelTrueFalse
            // 
            this.panelTrueFalse.BackColor = System.Drawing.Color.White;
            this.panelTrueFalse.Controls.Add(this.radioTrue);
            this.panelTrueFalse.Controls.Add(this.radioFalse);
            this.panelTrueFalse.Location = new System.Drawing.Point(26, 115);
            this.panelTrueFalse.Name = "panelTrueFalse";
            this.panelTrueFalse.Size = new System.Drawing.Size(180, 64);
            this.panelTrueFalse.TabIndex = 0;
            this.panelTrueFalse.Visible = false;
            // 
            // radioTrue
            // 
            this.radioTrue.AutoSize = true;
            this.radioTrue.Location = new System.Drawing.Point(10, 10);
            this.radioTrue.Name = "radioTrue";
            this.radioTrue.Size = new System.Drawing.Size(53, 20);
            this.radioTrue.TabIndex = 0;
            this.radioTrue.Text = "True";
            // 
            // radioFalse
            // 
            this.radioFalse.AutoSize = true;
            this.radioFalse.Location = new System.Drawing.Point(10, 35);
            this.radioFalse.Name = "radioFalse";
            this.radioFalse.Size = new System.Drawing.Size(59, 20);
            this.radioFalse.TabIndex = 1;
            this.radioFalse.Text = "False";
            // 
            // PlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(977, 414);
            this.Controls.Add(this.panelTrueFalse);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.txtAnswer);
            this.Controls.Add(this.panelChoices);
            this.Controls.Add(this.btnSubmit);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PlayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Play Quiz";
            this.Load += new System.EventHandler(this.PlayForm_Load);
            this.panelChoices.ResumeLayout(false);
            this.panelTrueFalse.ResumeLayout(false);
            this.panelTrueFalse.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}