namespace task1.Models.Errors
{
    [Serializable]
    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public Error(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
    }
}
