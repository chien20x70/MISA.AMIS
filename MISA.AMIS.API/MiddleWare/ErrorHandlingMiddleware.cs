using Microsoft.AspNetCore.Http;
using MISA.AMIS.Core.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.AMIS.API.MiddleWare
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Xử lý exception.
        /// </summary>
        /// <param name="context">.</param>
        /// <param name="exception">các exception.</param>
        /// <returns>response chứa các thông tin lỗi.</returns>
        /// Created By: NXCHIEN 07/05/2021
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Kiểm tra xem có phải là EmployeeExceptions
            if (exception is EmployeeExceptions)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                context.Response.StatusCode = 500;
            }

            // Custom 1 response trả về.
            var response = new
            {
                devMsg = exception.Message,     //message lỗi thông báo cho dev
                userMsg = exception.Message,    //message lỗi thông báo cho người dùng
                errorCode = "",                 // Mã nội bộ
                Data = exception.Data,          //
                moreInfo = "",                  // Hỗ trợ Dev về lỗi
                traceId = "",                   // Tra cứu thông tin log
            };
            //var stackTrace = String.Empty;
            //message = exception.Message;
            //var exceptionType = exception.GetType();
            var result = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}
