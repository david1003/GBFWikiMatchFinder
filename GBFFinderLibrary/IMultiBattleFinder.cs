using System;
using System.Collections.Generic;

namespace GBFFinderLibrary
{
    public interface IMultiBattleFinder
    {
        event Action<string, string, string> OnBattleFound; //matchid, name, fulltext
        event Action<string> OnWriteLog;

        void Execute(List<MultiBattleDefine> selectedBattles);
        //void Pause();
        void Stop();
    }
}
