using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using HOP.GUI.Presenter;
using HOP.GUI.View;

namespace HOP
{
    class HOP
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainWindow main_window = new MainWindow();
            GuiPresenter presenter = new GuiPresenter( main_window );
            main_window.SetPresenter(presenter);
            Application.Run( main_window );
        }
    }
}
