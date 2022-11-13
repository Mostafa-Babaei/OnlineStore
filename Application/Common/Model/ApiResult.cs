using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Common.Model
{
    public class ApiResult
    {

        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }

        public static ApiResult ToSuccessModel()
        {
            return new ApiResult() { IsSuccess = true };
        }
        public static ApiResult ToSuccessModel(string Message)
        {
            return new ApiResult() { IsSuccess = true, Message = Message };
        }
        public static ApiResult ToSuccessModel(string Message, object Data)
        {
            return new ApiResult() { IsSuccess = true, Message = Message, Data = Data };
        }
        public static ApiResult ToErrorModel(string Message, object Data)
        {
            return new ApiResult() { IsSuccess = false, Message = Message, Data = Data };
        }
        public static ApiResult ToErrorModel(string Message)
        {
            return new ApiResult() { IsSuccess = false, Message = Message };
        }
        public static ApiResult ToErrorModel()
        {
            return new ApiResult() { IsSuccess = false };
        }

    }

}
