namespace Navtech.Oms.Abstractions.Communication
{

    public class EmailMessage
    {
        public string[] ToAddress { get; set; }
        public string Subject  { get; set; }
        public string Body { get; set; }
    }
}
