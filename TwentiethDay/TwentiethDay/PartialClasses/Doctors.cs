using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentiethDay.DB
{
    partial class Doctors
    {
        public string FullName
        {
            get
            {
                return LastName + " " + Firstname + " " + Patronymic;
            }
        }
    }
}
