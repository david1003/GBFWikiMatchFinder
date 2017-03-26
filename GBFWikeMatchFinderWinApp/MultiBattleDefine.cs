using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBFWikeMatchFinderWinApp
{
    public class MultiBattleDefine
    {
        public MultiBattleDefine(string name, string value, int battleLevel)
        {
            Name = name;
            Value = value;
            BattleLevel = battleLevel;
        }
        public string Name { get; set; }
        public string Value { get; set; }
        public int BattleLevel { get; set; }
    }
}
