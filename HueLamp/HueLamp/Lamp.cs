using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLamp
{
    public class Lamp
    {
        public string test1;
        public string test2;

        

        public Lamp()
        {
            test1 = "test";
            test2 = "test2";
        }

        public string getTest
        {
            get
            {
                return test1;
            }
        }
    }

    public class LampViewModel
    {
        private Lamp defaultLamp = new Lamp();
        public Lamp DEFAULTLAMP { get { return this.defaultLamp; } }

    }
   

}
