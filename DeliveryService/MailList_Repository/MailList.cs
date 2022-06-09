namespace MailList_Repository;
public class MailList
{
    public MailList() {

    }

    public MailList(DateTime ordDate, DateTime delDate, OrderStatus status, double itemNum, double itemQuantity, double custID) {
        
        OrdDate = ordDate;
        DelDate = delDate;
        Status = status;        
        ItemNum = itemNum;
        ItemQuantity = itemQuantity;
        CustID = custID;
    }


    public DateTime OrdDate {get;set;}

    public DateTime DelDate {get; set;}

    public OrderStatus Status { get; set; }

    public double ItemNum { get; set; }

    public double ItemQuantity { get; set; }

    public double CustID { get; set; }
}

public enum OrderStatus {
    Scheduled,
    EnRoute,
    Complete,
    Canceled
}
