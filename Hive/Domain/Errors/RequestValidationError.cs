﻿namespace Hive.Domain.Errors
{
    public class RequestValidationError
    {
        public RequestValidationError(string title, int statusCode, string details, string[] errors)
        {
            Title = title;
            StatusCode = statusCode;
            Details = details;
            Errors = errors;
        }

        public string Title { get; }
        public int StatusCode { get; }
        public string Details { get; }
        public string[] Errors { get; }
    }
}
