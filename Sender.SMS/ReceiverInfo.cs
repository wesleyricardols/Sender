namespace Sender.SMS
{
    public class ReceiverInfo
    {
        public string Number { get; private set; }
        public string BodyMessage { get; private set; }
        public string UrlAPI { get; set; }

        #region Constructor

        public ReceiverInfo(string number, string bodyMessage, string api)
        {
            this.Number = number;
            this.BodyMessage = bodyMessage;
            this.UrlAPI = string.Format(api + "&to={0}&content={1}", this.Number, this.BodyMessage);
        }

        #endregion
    }
}
