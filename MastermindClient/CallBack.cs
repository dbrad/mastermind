// Bradley Elliott and David Brad
// CallBack.cs
// Impliments ICallBack and inherits from MarshalByRefObject
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MastermindLibrary;

namespace MastermindClient
{
    public class CallBack : MarshalByRefObject, ICallBack
    {
        private MainWindow wnd;

        public CallBack(MainWindow w)
        {
            wnd = w;
        }

        public void GameState(StateInfo info)
        {
            wnd.UpdateClientWindow(info);
        }
    }
}
