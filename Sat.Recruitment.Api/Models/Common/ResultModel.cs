using System.Collections.Generic;

namespace Sat.Recruitment.Api.Models.Common
{
    public class ResultModel
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public static ResultModel CreateSuccessResultModel()
        {
            return new ResultModel()
            {
                IsSuccess = true
            };
        }

        public static ResultModel CreateFailedResultModel(IEnumerable<string> errors)
        {
            return new ResultModel()
            {
                IsSuccess = false,
                Errors = errors
            };
        }
    }
}
