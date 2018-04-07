using System.Collections.Generic;

namespace FiiPrezent.Services
{
    public class ResultStatus
    {
        public ResultStatus()
        {
            Errors = new Dictionary<string, string>();
            Succeded = true;
        }

        public ResultStatus(dynamic @object)
        {
            Object = @object;
            Succeded = true;
        }
        
        public ResultStatus(ErrorType errorType)
        {
            ErrorType = errorType;
            Succeded = false;
        }

        public ResultStatus(string forProperty, string message)
            : this()
        {
            AddError(forProperty, message);
            Succeded = false;
        }

        public void AddError(string forProperty, string message)
        {
            Errors.Add(forProperty, message);
        }

        public Dictionary<string, string> GetErrors()
        {
            return Errors;
        }

        private Dictionary<string, string> Errors { get; }
        
        public ErrorType ErrorType { get; }

        public bool Succeded { get; }
        
        public dynamic Object { get; }
    }

    public enum ErrorType
    {
        NoError,
        NotFound
    }
}