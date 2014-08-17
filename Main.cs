using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using HOP.GUI.Presenter;
using HOP.GUI.View;
using HOP.Storage.DropBox;
using HOP.Config;
using HOP.GUI.Model;

namespace HOP
{
    class HOP
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Configuration config = new Configuration();
            DropBoxStorage storage = new DropBoxStorage( config );
            GuiModel model = new GuiModel(storage);
            MainWindow main_window = new MainWindow();
            GuiPresenter presenter = new GuiPresenter( main_window, model );
            main_window.SetPresenter(presenter);
            Application.Run( main_window );
        }
    }
}
