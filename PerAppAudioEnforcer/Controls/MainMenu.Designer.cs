namespace PerAppAudioEnforcer;

partial class MainMenu
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
        LogBox = new TextBox();
        ButtonsPanel = new ButtonsPanel();
        SuspendLayout();
        // 
        // LogBox
        // 
        LogBox.Dock = DockStyle.Fill;
        LogBox.Location = new Point(0, 0);
        LogBox.Margin = new Padding(30);
        LogBox.Multiline = true;
        LogBox.Name = "LogBox";
        LogBox.ReadOnly = true;
        LogBox.Size = new Size(800, 409);
        LogBox.TabIndex = 0;
        // 
        // ButtonsPanel
        // 
        ButtonsPanel.AutoSize = true;
        ButtonsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        ButtonsPanel.Dock = DockStyle.Bottom;
        ButtonsPanel.Location = new Point(0, 409);
        ButtonsPanel.Name = "ButtonsPanel";
        ButtonsPanel.Padding = new Padding(5);
        ButtonsPanel.Size = new Size(800, 41);
        ButtonsPanel.TabIndex = 1;
        // 
        // MainMenu
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(LogBox);
        Controls.Add(ButtonsPanel);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "MainMenu";
        Text = "PerAppAudioEnforcer";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox LogBox;
    private ButtonsPanel ButtonsPanel;
}
