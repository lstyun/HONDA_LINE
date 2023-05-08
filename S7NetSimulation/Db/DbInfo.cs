namespace S7NetSimulation.Db
{
    public static class DbInfo
    {
        private static readonly Dictionary<string, object> _dbValueDict = new();

        public static void put(string key, object value) 
        {
            if (_dbValueDict.ContainsKey(key)) 
            {
                _dbValueDict[key] = value;
            } 
            else
            {
                _dbValueDict.Add(key, value);
            }
        }

        public static object? get(string key) 
        {
            if (!_dbValueDict.ContainsKey(key)) 
            {
                return null;
            }
            return _dbValueDict[key];
        }



    }
}
