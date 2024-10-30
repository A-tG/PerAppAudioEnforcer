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
        // ButtonsPanel
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Controls.Add(ReloadConfigBtn);
        Name = "ButtonsPanel";
        Size = new Size(98, 31);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button ReloadConfigBtn;
}
