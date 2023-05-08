using HslCommunication;
using iWareCommon.Util;
using S7.Net;

namespace iWareSda.Plc.Entity;

public partial class PlcS7Net
{
    public static readonly object _Lock = new();

    public static int FailureTimes { get; set; } = 0;

    public static bool HeartFlag { get; set; } = false;

    private static int _heartbeatInd { get; set; } = 0;


    private readonly ILogger<PlcS7Net> _logger;

    private readonly PlcS7 _plcS7;

    private readonly DbHeartBeat _dbHeartBeat;


    public PlcS7Net(ILogger<PlcS7Net> logger, IConfiguration config, DbHeartBeat dbHeartBeat)
    {
        _logger = logger;

        _dbHeartBeat = dbHeartBeat;


        var host = config["plc:host"];

        var rack = config["plc:rack"];

        var slot = config["plc:slot"];

        _logger.LogInformation("PlcS7启动了... host={host}, rack={rack}, slot={slot}", host, rack, slot);

        _plcS7 = new PlcS7(EnumHelper.GetEnum<CpuType>(config["plc:cpuType"]), host ?? "127.0.0.1",
            short.Parse(rack ?? "0"), short.Parse(slot ?? "0"));

        ConnectServer();
    }

    public OperateResult ConnectServer()
    {
        OperateResult operateResult = new();

        try
        {
            var errorCode = _plcS7.Open();

            if (ErrorCode.NoError.Equals(errorCode))
            {
                operateResult.ErrorCode = 200;
                operateResult.IsSuccess = true;
                operateResult.Message = errorCode.ToString();
                _logger.LogInformation("PLC手动连接成功：{errorCode}", errorCode);
                return operateResult;
            }
            else
            {
                operateResult.ErrorCode = 500;
                operateResult.IsSuccess = false;
                operateResult.Message = errorCode.ToString();
                _logger.LogError("PLC手动连接失败：{errorCode}", errorCode);
                return operateResult;
            }
        }
        catch (Exception ex)
        {
            operateResult.ErrorCode = 500;
            operateResult.IsSuccess = false;
            operateResult.Message = ex.Message;
            _logger.LogError("PLC手动连接异常：{ex.Message}", ex.Message);
            return operateResult;
        }
    }

    public OperateResult CloseServer()
    {
        OperateResult operateResult = new();
        try
        {
            _plcS7.Close();
            operateResult.ErrorCode = 200;
            operateResult.IsSuccess = true;
            operateResult.Message = "PLC手动断开成功";
            _logger.LogInformation("PLC手动断开成功");
            return operateResult;
        }
        catch (Exception ex)
        {
            operateResult.ErrorCode = 500;
            operateResult.IsSuccess = false;
            operateResult.Message = ex.Message;
            _logger.LogError("PLC手动断开异常：{ex.Message}", ex.Message);
            return operateResult;
        }
    }

    public OperateResult Write(string db, object v)
    {
        OperateResult operateResult = new();

        var errorCode = ErrorCode.NoError;

        Console.WriteLine($"TYPE_NAME={v.GetType().Name}");

        switch (v.GetType().Name)
        {
            case "Byte":
            case "Int16":
            case "UInt16":
            case "Int32":
            case "UInt32":
            case "String":
                errorCode = (ErrorCode)_plcS7.Write(db, v);
                break;
            case "Boolean":
                errorCode = (ErrorCode)_plcS7.Write(db, (bool)v ? 1 : 0);
                break;
            case "Double":
                errorCode = (ErrorCode)WriteDouble(db, (double)v);
                break;
            case "Int64":
                errorCode = (ErrorCode)_plcS7.Write(db, int.Parse(v + ""));
                break;
            case "UInt64":
                errorCode = (ErrorCode)_plcS7.Write(db, uint.Parse(v + ""));
                break;
        }

        if (ErrorCode.NoError.Equals(errorCode))
        {
            operateResult.ErrorCode = 200;
            operateResult.IsSuccess = true;
            operateResult.Message = $"errorCode={errorCode},db={db}";
            return operateResult;
        }

        operateResult.ErrorCode = 500;
        operateResult.IsSuccess = false;
        operateResult.Message = $"errorCode={errorCode},db={db}";

        _logger.LogError("PLC写入{db}失败：{errorCode}", db, errorCode);

        return operateResult;
    }

    private object WriteDouble(string db, double v)
    {
        var array = db.Split(new char[1] { '.' });

        if (array.Length < 2) return ErrorCode.WrongVarFormat;

        var d = int.Parse(array[0][2..]);

        var offset = int.Parse(array[1][3..]);

        return _plcS7.Write(DataType.DataBlock, d, offset, v);
    }

    public OperateResult<short> ReadInt16(string db)
    {
        OperateResult<short> operateResult = new();

        var result = _plcS7.Read(db);

        if (result == null || result.GetType().Equals(typeof(ErrorCode)))
        {
            operateResult.ErrorCode = 500;
            operateResult.IsSuccess = false;
            operateResult.Message = $"result={result}, db={db}";
            operateResult.Content = 0;

            _logger.LogError("PLC读取{db}失败：{result}, {_plcS7.LastErrorString}",
                db, result == null ? "无数据" : result.ToString(), _plcS7.LastErrorString);
            return operateResult;
        }

        operateResult.ErrorCode = 200;
        operateResult.IsSuccess = true;
        operateResult.Message = $"读取成功,db={db}";
        operateResult.Content = short.Parse(result + "");
        return operateResult;
    }

    public OperateResult<int> ReadInt32(string db)
    {
        OperateResult<int> operateResult = new();

        var result = _plcS7.Read(db);

        if (result == null || result.GetType().Equals(typeof(ErrorCode)))
        {
            operateResult.ErrorCode = 500;
            operateResult.IsSuccess = false;
            operateResult.Message = $"result={result}, db={db}";
            operateResult.Content = 0;

            _logger.LogError("PLC读取{db}失败：{result}, {_plcS7.LastErrorString}",
                db, result == null ? "无数据" : result.ToString(), _plcS7.LastErrorString);
            return operateResult;
        }

        operateResult.ErrorCode = 200;
        operateResult.IsSuccess = true;
        operateResult.Message = $"读取成功,db={db}";
        operateResult.Content = int.Parse(result + "");
        return operateResult;
    }

    public OperateResult<long> ReadInt64(string db)
    {
        OperateResult<long> operateResult = new();

        var result = _plcS7.Read(db);

        if (result == null || result.GetType().Equals(typeof(ErrorCode)))
        {
            operateResult.ErrorCode = 500;
            operateResult.IsSuccess = false;
            operateResult.Message = $"result={result}, db={db}";
            operateResult.Content = 0;

            _logger.LogError("PLC读取{db}失败：{result}, {_plcS7.LastErrorString}",
                db, result == null ? "无数据" : result.ToString(), _plcS7.LastErrorString);
            return operateResult;
        }

        operateResult.ErrorCode = 200;
        operateResult.IsSuccess = true;
        operateResult.Message = $"读取成功,db={db}";
        operateResult.Content = long.Parse(result + "");
        return operateResult;
    }

    public OperateResult<bool> ReadBool(string db)
    {
        OperateResult<bool> operateResult = new();

        var result = _plcS7.Read(db);

        if (result.GetType().Equals(typeof(ErrorCode)))
        {
            operateResult.ErrorCode = 500;
            operateResult.IsSuccess = false;
            operateResult.Message = $"result={result}, db={db}";
            operateResult.Content = false;

            _logger.LogError("PLC读取{db}失败：{result}, {_plcS7.LastErrorString}",
                db, result == null ? "无数据" : result.ToString(), _plcS7.LastErrorString);
            return operateResult;
        }

        operateResult.ErrorCode = 200;
        operateResult.IsSuccess = true;
        operateResult.Message = $"读取成功,db={db}";
        operateResult.Content = (bool)result;
        return operateResult;
    }


    public OperateResult<double> ReadDouble(string db)
    {
        OperateResult<double> operateResult = new();

        var array = db.Split(new char[1] { '.' });
        if (array.Length < 2)
        {
            operateResult.ErrorCode = 500;
            operateResult.IsSuccess = false;
            operateResult.Message = $"errorcode={ErrorCode.WrongVarFormat}, db={db}";
            operateResult.Content = 0.0;
            return operateResult;
        }

        var d = int.Parse(array[0][2..]);
        var offset = int.Parse(array[1][3..]);
        var result = _plcS7.Read(DataType.DataBlock, d, offset, VarType.Real, 1);
        if (result == null || result.GetType().Equals(typeof(ErrorCode)))
        {
            operateResult.ErrorCode = 500;
            operateResult.IsSuccess = false;
            operateResult.Message = $"result={result}, db={db}";
            operateResult.Content = 0.0;

            _logger.LogError("PLC读取{db}失败：{result}, {_plcS7.LastErrorString}", db,
                result == null ? "无数据" : result.ToString(), _plcS7.LastErrorString);

            return operateResult;
        }

        operateResult.ErrorCode = 200;
        operateResult.IsSuccess = true;
        operateResult.Message = $"读取成功,db={db}";
        operateResult.Content = (double)result;

        return operateResult;
    }


    public OperateResult<string> ReadString(string db, int len)
    {
        OperateResult<string> operateResult = new();

        var array = db.Split(new char[1] { '.' });
        if (array.Length < 2)
        {
            operateResult.ErrorCode = 500;
            operateResult.IsSuccess = false;
            operateResult.Message = $"errorcode={ErrorCode.WrongVarFormat}, db={db}";
            operateResult.Content = "";
            return operateResult;
        }

        var d = int.Parse(array[0][2..]);
        var offset = int.Parse(array[1][3..]);
        var result = _plcS7.Read(DataType.DataBlock, d, offset, VarType.String, len);
        if (result == null || result.GetType().Equals(typeof(ErrorCode)))
        {
            operateResult.ErrorCode = 500;
            operateResult.IsSuccess = false;
            operateResult.Message = $"result={result}, db={db}";
            operateResult.Content = "";

            _logger.LogError("PLC读取{db}失败：{result}, {_plcS7.LastErrorString}",
                db, result == null ? "无数据" : result.ToString(), _plcS7.LastErrorString);
            return operateResult;
        }

        operateResult.ErrorCode = 200;
        operateResult.IsSuccess = true;
        operateResult.Message = $"读取成功,db={db}";
        operateResult.Content = (string)result;

        return operateResult;
    }

    public object Heartbeat()
    {
        _heartbeatInd = (_heartbeatInd + 1) % 100;
        return DateTime.Now.ToString() + " Heart:" + _heartbeatInd;
    }
}