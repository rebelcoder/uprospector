
//------------------------------------------------------------------------------

//  <auto-generated>
//      This code was generated by:
//        TerminalGuiDesigner v1.0.25.0
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// -----------------------------------------------------------------------------
namespace uprospector {
    using System;
    using Terminal.Gui;
    
    
    public partial class uprospector_window : Terminal.Gui.Window {
        
        private Terminal.Gui.ColorScheme tgDefault;
        
        private Terminal.Gui.CheckBox remove_unused_directories;
        
        private Terminal.Gui.CheckBox exclude_unity_only;
        
        private Terminal.Gui.TextField source_path;
        
        private Terminal.Gui.TextField destination_path;
        
        private Terminal.Gui.ProgressBar progress_bar;
        
        private Terminal.Gui.Button src_btn;
        
        private Terminal.Gui.Button dst_button;
        
        private Terminal.Gui.Button extract_btn;
        
        private Terminal.Gui.Button exit_btn;
        
        private void InitializeComponent() {
            this.exit_btn = new Terminal.Gui.Button();
            this.extract_btn = new Terminal.Gui.Button();
            this.dst_button = new Terminal.Gui.Button();
            this.src_btn = new Terminal.Gui.Button();
            this.progress_bar = new Terminal.Gui.ProgressBar();
            this.destination_path = new Terminal.Gui.TextField();
            this.source_path = new Terminal.Gui.TextField();
            this.exclude_unity_only = new Terminal.Gui.CheckBox();
            this.remove_unused_directories = new Terminal.Gui.CheckBox();
            this.tgDefault = new Terminal.Gui.ColorScheme();
            this.tgDefault.Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.White, Terminal.Gui.Color.Blue);
            this.tgDefault.HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightCyan, Terminal.Gui.Color.Blue);
            this.tgDefault.Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.Gray);
            this.tgDefault.HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightBlue, Terminal.Gui.Color.Gray);
            this.tgDefault.Disabled = new Terminal.Gui.Attribute(Terminal.Gui.Color.Brown, Terminal.Gui.Color.Blue);
            this.Width = Dim.Fill(0);
            this.Height = Dim.Fill(0);
            this.X = 0;
            this.Y = 0;
            this.Visible = true;
            this.Modal = false;
            this.IsMdiContainer = false;
            this.Border.BorderStyle = Terminal.Gui.BorderStyle.Single;
            this.Border.Effect3D = false;
            this.Border.Effect3DBrush = null;
            this.Border.DrawMarginFrame = true;
            this.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Title = "uprospector - set your assets free";
            this.remove_unused_directories.Width = 6;
            this.remove_unused_directories.Height = 1;
            this.remove_unused_directories.X = Pos.Center();
            this.remove_unused_directories.Y = 1;
            this.remove_unused_directories.Visible = true;
            this.remove_unused_directories.Data = "remove_unused_directories";
            this.remove_unused_directories.Text = "remove unused directories";
            this.remove_unused_directories.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.remove_unused_directories.Checked = false;
            this.Add(this.remove_unused_directories);
            this.exclude_unity_only.Width = 6;
            this.exclude_unity_only.Height = 1;
            this.exclude_unity_only.X = Pos.Center();
            this.exclude_unity_only.Y = Pos.Bottom(remove_unused_directories) + 1;
            this.exclude_unity_only.Visible = true;
            this.exclude_unity_only.Data = "exclude_unity_only";
            this.exclude_unity_only.Text = "exclude unity only files";
            this.exclude_unity_only.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.exclude_unity_only.Checked = false;
            this.Add(this.exclude_unity_only);
            this.source_path.Width = 55;
            this.source_path.Height = 1;
            this.source_path.X = Pos.Center();
            this.source_path.Y = Pos.Bottom(exclude_unity_only) + 3;
            this.source_path.Visible = true;
            this.source_path.Secret = false;
            this.source_path.Data = "source_path";
            this.source_path.Text = "Source:";
            this.source_path.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.source_path);
            this.destination_path.Width = 55;
            this.destination_path.Height = 1;
            this.destination_path.X = Pos.Center();
            this.destination_path.Y = Pos.Bottom(source_path) + 1;
            this.destination_path.Visible = true;
            this.destination_path.Secret = false;
            this.destination_path.Data = "destination_path";
            this.destination_path.Text = "Destination:";
            this.destination_path.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.destination_path);
            this.progress_bar.Width = 20;
            this.progress_bar.Height = 1;
            this.progress_bar.X = Pos.Center();
            this.progress_bar.Y = Pos.Center();
            this.progress_bar.Visible = true;
            this.progress_bar.Data = "progress_bar";
            this.progress_bar.Text = "0%";
            this.progress_bar.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.progress_bar.Fraction = 1F;
            this.progress_bar.BidirectionalMarquee = true;
            this.progress_bar.ProgressBarStyle = Terminal.Gui.ProgressBarStyle.MarqueeContinuous;
            this.progress_bar.ProgressBarFormat = Terminal.Gui.ProgressBarFormat.Simple;
            this.progress_bar.SegmentCharacter = '█';
            this.Add(this.progress_bar);
            this.src_btn.Width = 14;
            this.src_btn.Height = 1;
            this.src_btn.X = Pos.Center();
            this.src_btn.Y = Pos.Center();
            this.src_btn.Visible = true;
            this.src_btn.Data = "src_btn";
            this.src_btn.Text = "src folder";
            this.src_btn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.src_btn.IsDefault = false;
            this.Add(this.src_btn);
            this.dst_button.Width = 14;
            this.dst_button.Height = 1;
            this.dst_button.X = Pos.Center();
            this.dst_button.Y = Pos.Bottom(src_btn) + 1;
            this.dst_button.Visible = true;
            this.dst_button.Data = "dst_button";
            this.dst_button.Text = "dst folder";
            this.dst_button.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.dst_button.IsDefault = false;
            this.Add(this.dst_button);
            this.extract_btn.Width = 11;
            this.extract_btn.Height = 1;
            this.extract_btn.X = Pos.Center();
            this.extract_btn.Y = Pos.Bottom(dst_button) + 1;
            this.extract_btn.Visible = true;
            this.extract_btn.Data = "extract_btn";
            this.extract_btn.Text = "extract";
            this.extract_btn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.extract_btn.IsDefault = false;
            this.Add(this.extract_btn);
            this.exit_btn.Width = 8;
            this.exit_btn.Height = 1;
            this.exit_btn.X = Pos.Center();
            this.exit_btn.Y = Pos.Bottom(extract_btn) + 1;
            this.exit_btn.Visible = true;
            this.exit_btn.Data = "exit_btn";
            this.exit_btn.Text = "exit";
            this.exit_btn.TextAlignment = Terminal.Gui.TextAlignment.Centered;
            this.exit_btn.IsDefault = false;
            this.Add(this.exit_btn);
        }
    }
}
