using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace iRacingSLI {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmMain form = new frmMain();
            Application.Run(form);

            AppDomain.CurrentDomain.ProcessExit += (s, e) => form.SendStopToArduino();
        }
    }
}
