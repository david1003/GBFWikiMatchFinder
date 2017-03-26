using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBFWikeMatchFinderWinApp.Finder
{
    public interface IMultiBattleFinder
    {
        event Action<string, string> OnBattleFound;
        event Action<string> OnWriteLog;

        void Execute(List<MultiBattleDefine> selectedBattles);
        //void Pause();
        void Stop();
    }
}
