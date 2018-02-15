using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentiethDay.HelpClasses
{
    class StaticClass
    {
        static public DB.Entity InicializateDataBase = new DB.Entity();
        static public DB.Patients SelectPatient;
        static public DB.Doctors SelectDoctors;
    }
}
