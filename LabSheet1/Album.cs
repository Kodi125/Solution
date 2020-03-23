using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabSheet1
{
    class Album
    {
        //Props
        public string Name { get; set; }
        public DateTime Released { get; set; }
        public int Sales { get; set; }

        //Ctors

        //Methods
        public override string ToString()
        {
            return (this.Name + " - " + (DateTime.Now.Year - Released.Year) + " Years since release");
        }

    }
}
