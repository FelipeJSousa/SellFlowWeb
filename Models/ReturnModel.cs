namespace Models
{
    public class ReturnModel<T>
    {
        public bool status { get; set; }

        public T dados { get; set; }

        public string erro { get; set; }

        public string mensagem { get; set; }

    }
}
