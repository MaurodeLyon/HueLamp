﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLamp
{
    public class Lamp
    {


        private string _on;
        public string on
        {
            get { return _on; }
            set { _on = value; }
        }

        private string _bri;

        public string bri
        {
            get { return _bri; }
            set { _bri = value; }
        }

        private string _hue;

        public string hue
        {
            get { return _hue; }
            set { _hue = value; }
        }

        private string _sat;

        public string sat
        {
            get { return _sat; }
            set { _sat = value; }
        }




        public Lamp(string on, string bri, string hue, string sat)
        {
            _on = on;
            _bri = bri;
            _hue = hue;
            _sat = sat;

        }

    }
}
