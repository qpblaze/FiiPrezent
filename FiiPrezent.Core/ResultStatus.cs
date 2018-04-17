using System.Collections.Generic;

namespace FiiPrezent.Core
{
    public class ResultStatus
    {
        private Dictionary<string, string> Errors { get; }

        public ResultStatusType Type { get; }

        public dynamic Object { get; }

        #region Constructor

        public ResultStatus()
        {
            Type = ResultStatusType.Ok;
        }

        public ResultStatus(dynamic @object)
        {
            Object = @object;
        }

        public ResultStatus(ResultStatusType type, dynamic @object)
        {
            Object = @object;
            Type = type;
        }

        public ResultStatus(ResultStatusType type)
        {
            Type = type;
        }

        public ResultStatus(string forProperty, string message)
        {
            if(Errors == null)
                Errors = new Dictionary<string, string>();

            AddError(forProperty, message);
        }

        public ResultStatus(ResultStatusType type, string forProperty, string message)
            :this(forProperty, message)
        {
            Type = type;
        }

        #endregion

        public void AddError(string forProperty, string message)
        {
            Errors.Add(forProperty, message);
        }

        public Dictionary<string, string> GetErrors()
        {
            return Errors;
        }
    }

    public enum ResultStatusType
    {
        Error,
        Ok,
        NotFound,
        InvalidCode,
        CodeAlreadyInUse
    }
}