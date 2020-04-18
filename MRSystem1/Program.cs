using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MRSystem1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MRDB mrdb = new MRDB();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new AddMedicineForm(mrdb));
            //Application.Run(new CreateReport(mrdb));
            //Application.Run(new UpdateMedicine(mrdb));
            //Application.Run(new viewReport(mrdb));
            Application.Run(new Login());
       //     Application.Run(new ConfirmScheduleForm(mrdb));



        }
    }
}
