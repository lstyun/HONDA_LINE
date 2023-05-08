namespace S7.Net
{
    public enum ErrorCode
    {
        NoError = 0,
        WrongCPU_Type = 1,
        ConnectionError = 2,
        IPAddressNotAvailable = 3,
        WrongVarFormat = 10,
        WrongNumberReceivedBytes = 11,
        SendData = 20,
        ReadData = 30,
        WriteData = 50
    }
}