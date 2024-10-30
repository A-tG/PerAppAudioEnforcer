namespace PerAppAudioEnforcer;

partial class ButtonsPanel
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        ReloadConfigBtn = new Button();
        SrcCodeLink = new LinkLabel();
        SuspendLayout();
        // 
        // ReloadConfigBtn
        // 
        ReloadConfigBtn.Anchor = AnchorStyles.Left;
        ReloadConfigBtn.AutoSize = true;
        ReloadConfigBtn.Location = new Point(3, 3);
        ReloadConfigBtn.Name = "ReloadConfigBtn";
        ReloadConfigBtn.Size = new Size(92, 25);
        ReloadConfigBtn.TabIndex = 0;
        ReloadConfigBtn.Text = "Reload Config";
        ReloadConfigBtn.UseVisualStyleBackColor = true;
        // 
        // SrcCodeLink
        // 
        SrcCodeLink.Anchor = AnchorStyles.Right;
        SrcCodeLink.AutoSize = true;
        SrcCodeLink.Location = new Point(326, 8);
        SrcCodeLink.Name = "SrcCodeLink";
        SrcCodeLink.Size = new Size(72, 15);
        SrcCodeLink.TabIndex = 1;
        SrcCodeLink.TabStop = true;
        SrcCodeLink.Text = "Source code";
        SrcCodeLink.LinkClicked += SrcCodeLink_LinkClicked;
        // 
        // ButtonsPanel
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Controls.Add(SrcCodeLink);
        Controls.Add(ReloadConfigBtn);
        Name = "ButtonsPanel";
        Size = new Size(403, 31);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button ReloadConfigBtn;
    private LinkLabel SrcCodeLink;
}
