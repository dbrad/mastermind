// Bradley Elliott and David Brad
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MastermindLibrary
{
    public interface ICallBack
    {
        void GameState(StateInfo info);
    }
}
