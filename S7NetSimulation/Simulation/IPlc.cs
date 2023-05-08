namespace S7.Net.Interfaces
{
    public interface IPlc : IDisposable
    {
        string IP { get; set; }

        int Port { get; set; }

        bool IsConnected { get; }

        CpuType CPU { get; set; }

        short Rack { get; set; }

        short Slot { get; set; }

        string Name { get; set; }

        object? Tag { get; set; }

        bool IsAvailable { get; }

        string? LastErrorString { get; }

        ErrorCode LastErrorCode { get; }

        ErrorCode Open();

        void Close();

        //byte[] ReadBytes(DataType dataType, int DB, int startByteAdr, int count);

        //ErrorCode WriteBytes(DataType dataType, int db, int startByteAdr, byte[] value);

        //ushort[] ReadWords(DataType dataType, int DB, int startByteAdr, int count);

        //ErrorCode WriteWords(DataType dataType, int db, int startByteAdr, ushort[] value);
    }
}
