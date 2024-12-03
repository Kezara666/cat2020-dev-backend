namespace CAT20.WebApi.Resources.OnlinePayment;

public class BocResponse
{
    public string checkoutMode { get; set; }
    public string merchant { get; set; }
    public string result { get; set; }
    public SessionInfo session { get; set; }
    public string successIndicator { get; set; }
}