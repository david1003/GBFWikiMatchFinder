namespace GBFFinderLibrary
{
    public class MultiBattleDefine
    {
        public MultiBattleDefine(string name, string value, int level)
        {
            Name = name;
            Value = value;
            Level = level;
        }
        public string Name { get; set; }
        public string Value { get; set; }
        public int Level { get; set; }
    }

    
}