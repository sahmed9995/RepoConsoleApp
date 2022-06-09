using Xunit;
using MailList_Repository;


namespace MailList_Tests;

public class MailListTests
{
    [Fact]
    public void ShouldAddOrderToList()
    {
        MailListRepository repository = new MailListRepository();
        MailList MailEx = new MailList(new DateTime(2005, 05, 14), new DateTime(2006, 03, 26), OrderStatus.Complete, 009988, 30, 92093983);

        repository.AddDeliveryToHistory(MailEx);

        int expected = 1;
        int actual = repository._history.Count;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldDeleteOrderFromList()
    {
        MailListRepository repository = new MailListRepository();
        MailList MailEx = new MailList(new DateTime(2005, 05, 14), new DateTime(2006, 03, 26), OrderStatus.Complete, 009988, 30, 92093983);

        repository.AddDeliveryToHistory(MailEx);

        bool expected = true;
        bool actual = repository.DeleteOrder(92093983);

        Assert.Equal(expected, actual);
    }
}