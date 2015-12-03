using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLamp
{
    class MainViewModel
    {
        private NetworkHandler _networkHandler;
        private ObservableCollection<Lamp> _lamps;

        public NetworkHandler NetworkHandler
        {
            get { return _networkHandler; }
            set { _networkHandler = value; }
        }

        public ObservableCollection<Lamp> Lamps
        {
            get { return _lamps; }
            set { _lamps = value; }
        }


        public MainViewModel()
        {
            _lamps = new ObservableCollection<Lamp>();
            _lamps.Add(new Lamp("true", "233", "222", "123"));
            _lamps.Add(new Lamp("true", "233", "222", "123"));
            _lamps.Add(new Lamp("true", "233", "222", "123"));
            _lamps.Add(new Lamp("true", "233", "222", "123"));
        }
    }
}
