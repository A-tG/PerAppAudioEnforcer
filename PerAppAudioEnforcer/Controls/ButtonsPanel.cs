using PerAppAudioEnforcer.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PerAppAudioEnforcer;
public partial class ButtonsPanel : UserControl
{
    public ButtonsPanel()
    {
        InitializeComponent();
    }

    private void SrcCodeLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        OpenInOs.TryOpen("https://github.com/A-tG/PerAppAudioEnforcer");
    }
}
