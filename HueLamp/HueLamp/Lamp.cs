using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLamp
{
    public class Lamp
    {

        private string _test1;
        public string test1
        {
            get { return _test1; }
            set { _test1 = value; }
        }
        
        private string _test2;
        public string test2
        {
            get { return _test2; }
            set { _test2 = value; }
        }

        public Lamp()
        {
            test1 = "test";
            test2 = "test2";
        }

    }

    public class LampViewModel
    {
        private Lamp defaultLamp = new Lamp();
        public Lamp DEFAULTLAMP { get { return this.defaultLamp; } }

    }


}
