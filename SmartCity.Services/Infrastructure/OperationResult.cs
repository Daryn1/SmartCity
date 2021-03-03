using System.Collections.Generic;

namespace SmartCity.Services.Infrastructure
{
    public class OperationResult
    {
        public List<string> Errors = new List<string>();

        public bool Succeeded { get; set; } = true;

        public static OperationResult Failed(string error)
        {
            var operationResult = new OperationResult { Succeeded = false };
            if (error != null)
            {
                operationResult.Errors.Add(error);
            }

            return operationResult;
        }

        public static OperationResult Success()
        {
            return new OperationResult { Succeeded = true };
        }
    }
}
