using S7.Net.Interfaces;
using S7NetSimulation.Db;


namespace S7.Net
{
    public class PlcS7 : IPlc, IDisposable
    {     
        public string IP { get; set; }

        public int Port { get; set; }

        public CpuType CPU { get; set; }

        public short Rack { get; set; }

        public short Slot { get; set; }

        public string Name { get; set; }

        public object? Tag { get; set; }

        public bool IsAvailable
        {
            get
            {
               return true;
            }
        }

        public bool IsConnected { get; private set; }

        public string? LastErrorString { get; private set; }

        public ErrorCode LastErrorCode { get; private set; }

        public PlcS7()
            : this(CpuType.S7400, "localhost", 0, 2)
        {
        }

        public PlcS7(CpuType cpu, string ip, short rack, short slot, string name = "", object? tag = null)
        {
            IsConnected = false;
            IP = ip;
            CPU = cpu;
            Rack = rack;
            Slot = slot;
            Name = name;
            Tag = tag;
        }

        public ErrorCode Open()
        {
            return ErrorCode.NoError;
        }

        public void Close()
        {   
        }

        public object Write(string variable, object value)
        {
            DbInfo.put(variable, value);
            return ErrorCode.NoError;
        }

        public object Write(DataType dataType, int db, int startByteAdr, object value)
        {
            string variable = $"DB{db}.DBD{startByteAdr}";
            DbInfo.put(variable, value);
            return ErrorCode.NoError;
        }

        public object Read(string variable)
        {
            string text = variable.ToUpper();
            text = text.Replace(" ", "");
            
            string[] array = text.Split(new char[1] { '.' });

            try
            {
                if (array.Length < 2)
                {
                    throw new Exception("DB地址格式错误");
                }


                object? obj = DbInfo.get(variable);

                if (obj == null) 
                {
                    throw new Exception($"地址{variable}没有存储值");
                }

                if (array.Length >= 3)
                {
                    return ((int)obj) > 0;
                    
                }

                return obj;
            }
            catch (Exception e) 
            {
                LastErrorString = e.Message;
                return ErrorCode.WrongVarFormat;
            }
        }


        public object Read(DataType dataBlock, int db,  int startByteAdr, VarType varType, int len) {
            try 
            {
                object? obj = null;
                string variable = "";
                
                switch (varType) 
                {
                
                    case VarType.Real:
                        variable = $"DB{db}.DBD{startByteAdr}";
                        obj = DbInfo.get(variable);
                        break;
                    case VarType.String:
                        variable = $"DB{db}.DBS{startByteAdr}";
                        obj = DbInfo.get(variable);
                        if (obj != null) 
                        {
                            obj = ((string)obj)[..len];
                        }
                        break;
                }

                if (obj == null)
                {
                    throw new Exception($"地址{variable}没有存储值");
                }

                return obj;

            }
            catch (Exception e) 
            {
                LastErrorString = e.Message;
                return ErrorCode.WrongVarFormat;
            }
        }





        public void Dispose()
        {
            throw new NotImplementedException();
        }





    }
}
