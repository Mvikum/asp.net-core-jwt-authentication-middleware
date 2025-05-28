namespace StudentService.Middlewares.helpers.Response
{
    public class BaseResponse
    {
        public int status_code { get; set; }
        public object data { get; set; } = null;

        public BaseResponse(int statusCode,object data)
        {
            this.status_code = statusCode;
            this.data = data;
            
        }
        public BaseResponse()
        {

        }

    }
}
