namespace Lab4_Client;

partial class MainForm
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
            this.ChatWrapper = new System.Windows.Forms.GroupBox();
            this.ChatBox = new System.Windows.Forms.RichTextBox();
            this.BottomContainer = new System.Windows.Forms.SplitContainer();
            this.MessageTextBox = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.RootContainer = new System.Windows.Forms.SplitContainer();
            this.ChatWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BottomContainer)).BeginInit();
            this.BottomContainer.Panel1.SuspendLayout();
            this.BottomContainer.Panel2.SuspendLayout();
            this.BottomContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RootContainer)).BeginInit();
            this.RootContainer.Panel1.SuspendLayout();
            this.RootContainer.Panel2.SuspendLayout();
            this.RootContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChatWrapper
            // 
            this.ChatWrapper.Controls.Add(this.ChatBox);
            this.ChatWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatWrapper.Location = new System.Drawing.Point(0, 0);
            this.ChatWrapper.Name = "ChatWrapper";
            this.ChatWrapper.Size = new System.Drawing.Size(324, 483);
            this.ChatWrapper.TabIndex = 0;
            this.ChatWrapper.TabStop = false;
            this.ChatWrapper.Text = "Чат";
            // 
            // ChatBox
            // 
            this.ChatBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatBox.Location = new System.Drawing.Point(3, 19);
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.Size = new System.Drawing.Size(318, 461);
            this.ChatBox.TabIndex = 0;
            this.ChatBox.Text = "";
            // 
            // BottomContainer
            // 
            this.BottomContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.BottomContainer.IsSplitterFixed = true;
            this.BottomContainer.Location = new System.Drawing.Point(0, 0);
            this.BottomContainer.Name = "BottomContainer";
            // 
            // BottomContainer.Panel1
            // 
            this.BottomContainer.Panel1.Controls.Add(this.MessageTextBox);
            this.BottomContainer.Panel1.Padding = new System.Windows.Forms.Padding(4, 4, 0, 4);
            // 
            // BottomContainer.Panel2
            // 
            this.BottomContainer.Panel2.Controls.Add(this.SendButton);
            this.BottomContainer.Panel2.Padding = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.BottomContainer.Size = new System.Drawing.Size(324, 46);
            this.BottomContainer.SplitterDistance = 239;
            this.BottomContainer.TabIndex = 1;
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageTextBox.Location = new System.Drawing.Point(4, 4);
            this.MessageTextBox.Multiline = true;
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.PlaceholderText = "Сообщение";
            this.MessageTextBox.Size = new System.Drawing.Size(235, 38);
            this.MessageTextBox.TabIndex = 0;
            // 
            // SendButton
            // 
            this.SendButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SendButton.Location = new System.Drawing.Point(0, 4);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(77, 38);
            this.SendButton.TabIndex = 0;
            this.SendButton.Text = "Отправить";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // RootContainer
            // 
            this.RootContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RootContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.RootContainer.IsSplitterFixed = true;
            this.RootContainer.Location = new System.Drawing.Point(0, 0);
            this.RootContainer.Name = "RootContainer";
            this.RootContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // RootContainer.Panel1
            // 
            this.RootContainer.Panel1.Controls.Add(this.ChatWrapper);
            // 
            // RootContainer.Panel2
            // 
            this.RootContainer.Panel2.Controls.Add(this.BottomContainer);
            this.RootContainer.Size = new System.Drawing.Size(324, 533);
            this.RootContainer.SplitterDistance = 483;
            this.RootContainer.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 533);
            this.Controls.Add(this.RootContainer);
            this.MinimumSize = new System.Drawing.Size(340, 0);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ChatWrapper.ResumeLayout(false);
            this.BottomContainer.Panel1.ResumeLayout(false);
            this.BottomContainer.Panel1.PerformLayout();
            this.BottomContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BottomContainer)).EndInit();
            this.BottomContainer.ResumeLayout(false);
            this.RootContainer.Panel1.ResumeLayout(false);
            this.RootContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RootContainer)).EndInit();
            this.RootContainer.ResumeLayout(false);
            this.ResumeLayout(false);

	}

    #endregion

    private GroupBox ChatWrapper;
    private SplitContainer BottomContainer;
    private TextBox MessageTextBox;
    private Button SendButton;
    private RichTextBox ChatBox;
    private SplitContainer RootContainer;
}
