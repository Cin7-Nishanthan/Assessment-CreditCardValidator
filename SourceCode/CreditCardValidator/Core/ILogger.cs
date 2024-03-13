
namespace Core
{
    public interface ILogger
    {
        void Log(int step, string data, string? requestData = null, string? exceptionData = null, string? responseData = null);
    }
}