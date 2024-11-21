namespace Esewa_Integration.Dtos
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string[] Errors { get; set; }
        public ResponseModel(string message)
        {
            Success = false;
            Errors = new[] { message };
        }
        public ResponseModel(T data)
        {
            Success = true;
            Data = data;
        }
    }

}
