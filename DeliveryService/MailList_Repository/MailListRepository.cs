using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailList_Repository
{
    public class MailListRepository
    {
        public readonly List<MailList> _history = new List<MailList>();

        public void AddDeliveryToHistory(MailList order) {
            _history.Add(order);
        }

        public List<MailList> ListAll() {
            return _history;
        }

        public MailList GetDeliveriesList() {
            return _history.Find(order => order.Status == OrderStatus.EnRoute || order.Status == OrderStatus.Complete);


        }

        public bool UpdateOrder(double IDOfCust, MailList newOrder) {
            MailList orderToUpdate = _history.Find(order => order.CustID == IDOfCust);

            if (orderToUpdate != default) {
                orderToUpdate.Status = newOrder.Status;
                orderToUpdate.ItemNum = newOrder.ItemNum;
                orderToUpdate.ItemQuantity = newOrder.ItemQuantity;
                orderToUpdate.CustID = newOrder.CustID;
                return true;
            } else {
                return false;
            }
        }

        public bool DeleteOrder(double custID) {
            MailList orderToDelete = _history.Find(order => order.CustID == custID);

            if (orderToDelete != default) {
                return _history.Remove(orderToDelete);
            } else {
                return false;
            }
        }
    }
}