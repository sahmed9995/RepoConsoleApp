using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailList_Repository;

    public class ProgramUI
    {
        public readonly MailListRepository _repo = new MailListRepository();

        public void Run() {
            Seed();

            RunMenu();
        }

        public void RunMenu() {

            bool continueToRun = true;

            do {
                Console.Clear();
                System.Console.WriteLine("Welcome to our Delivery Service! Please choose from the following options\n" +
                "1. Add a delivery order\n" +
                "2. List all orders that are Enroute or Completed\n" +
                "3. Update an Order\n" +
                "4. Delete an Order\n" +
                "5. Exit");

                string selection = Console.ReadLine();

                switch (selection) 
                {
                    case "1":
                        CreateNewOrder();
                        break;
                    case "2":
                        ListEnRouteAndCompletedOrders();
                        break;
                    case "3":
                        UpdateExistingOrder();
                        break;
                    case "4":
                        DeleteExistingOrder();
                        break;
                    case "5":
                        Console.Clear();
                        System.Console.WriteLine("Thank you for choosing us!");
                        continueToRun=false;
                        break;
                    default:
                        Console.Clear();
                        System.Console.WriteLine("Please choose a valid option");
                        break;
                }
            } while (continueToRun);
        }

        public void CreateNewOrder() {

            Console.Clear();

            System.Console.WriteLine("Enter the date ordered (YYYY, MM, DD):");
            DateTime ordDate = Convert.ToDateTime(Console.ReadLine());

            System.Console.WriteLine("Enter the delivery date (YYYY, MM, DD):");
            DateTime delDate = Convert.ToDateTime(Console.ReadLine());

            System.Console.WriteLine("Please select a number for the STATUS of the order:\n" +
            "1. Scheduled\n" +
            "2. EnRoute\n" +
            "3. Completed\n" +
            "4. Canceled");
            string ordStatus = Console.ReadLine();
            OrderStatus status = ordStatus switch {
                "1" => OrderStatus.Scheduled,
                "2" => OrderStatus.EnRoute,
                "3" => OrderStatus.Complete,
                "4" => OrderStatus.Canceled
            };

            System.Console.WriteLine("Please enter the ITEM NUMBER of the order:");
            double itemNum = Convert.ToDouble(Console.ReadLine());

            System.Console.WriteLine("Please enter the QUANTITY of the item for the order:");
            double itemQuantity = Convert.ToDouble(Console.ReadLine());

            System.Console.WriteLine("Please enter the CUSTOMER ID NUMBER for the order:");
            double custID = Convert.ToDouble(Console.ReadLine());

            MailList newOrder = new MailList(ordDate, delDate, status, itemNum, itemQuantity, custID);

            _repo.AddDeliveryToHistory(newOrder);

            WaitForKey();
        }

        public void ListEnRouteAndCompletedOrders() {

            Console.Clear();

            List<MailList> allOrders = _repo.ListAll();

            Console.Clear();

            System.Console.WriteLine("These are all the orders that are enroute and complete:\n" +
            "--------------------------------------------------");


            foreach (MailList order in allOrders)
            {
                if (order.Status == OrderStatus.EnRoute) {
                    System.Console.WriteLine($"Date Ordered: {order.OrdDate}\n");
                    System.Console.WriteLine($"Date of Delivery or Expected Delivery: {order.DelDate}\n");
                    System.Console.WriteLine($"Status: {order.Status}\n");
                    System.Console.WriteLine($"Item Number: {order.ItemNum}\n");
                    System.Console.WriteLine($"Item Quantity: {order.ItemQuantity}\n");
                    System.Console.WriteLine($"Customer ID Number: {order.CustID}\n");
                    System.Console.WriteLine("--------------------------------------------\n");
                } else if (order.Status == OrderStatus.Complete) {
                    System.Console.WriteLine($"Date Ordered: {order.OrdDate}\n");
                    System.Console.WriteLine($"Date of Delivery or Expected Delivery: {order.DelDate}\n");
                    System.Console.WriteLine($"Status: {order.Status}\n");
                    System.Console.WriteLine($"Item Number: {order.ItemNum}\n");
                    System.Console.WriteLine($"Item Quantity: {order.ItemQuantity}\n");
                    System.Console.WriteLine($"Customer ID Number: {order.CustID}\n");
                    System.Console.WriteLine("--------------------------------------------\n");
                }
            }

            WaitForKey();
        }

        public void UpdateExistingOrder() {

            Console.Clear();

            System.Console.WriteLine("Please give the previous CUSTOMER ID NUMBER of the order you would like to update:");

            double custID = Convert.ToDouble(Console.ReadLine());

            MailList updatedOrder = new MailList();

            if (_repo.UpdateOrder(custID, updatedOrder)) {
                System.Console.WriteLine("Enter the updated ordered date (YYYY, MM, DD):");
                DateTime ordDate = Convert.ToDateTime(Console.ReadLine());

                System.Console.WriteLine("Enter the updated date for delivery (YYYY, MM, DD):");
                DateTime delDate = Convert.ToDateTime(Console.ReadLine());

                System.Console.WriteLine("Please select a number for the updated STATUS of the order:\n" +
                "1. Scheduled\n" +
                "2. EnRoute\n" +
                "3. Completed\n" +
                "4. Canceled");
                string ordStatus = Console.ReadLine();
                updatedOrder.Status = ordStatus switch {
                "1" => OrderStatus.Scheduled,
                "2" => OrderStatus.EnRoute,
                "3" => OrderStatus.Complete,
                "4" => OrderStatus.Canceled
                };

                System.Console.WriteLine("Please enter the updated ITEM NUMBER of the order:");
                updatedOrder.ItemNum = Convert.ToDouble(Console.ReadLine());

                System.Console.WriteLine("Please enter the updated QUANTITY of the item for the order:");
                updatedOrder.ItemQuantity = Convert.ToDouble(Console.ReadLine());

                System.Console.WriteLine("Please enter the updated CUSTOMER ID NUMBER for the order:");
                updatedOrder.CustID = Convert.ToDouble(Console.ReadLine());

                MailList UpdatedOrder = new MailList(ordDate, delDate, updatedOrder.Status, updatedOrder.ItemNum, updatedOrder.ItemQuantity, updatedOrder.CustID);
            
                _repo.AddDeliveryToHistory(UpdatedOrder);

                System.Console.WriteLine("The order has been sucessfully updated!");
            } else {
                System.Console.WriteLine($"The customer ID: {custID} cannot be found. Please try a different number.");
            }

            WaitForKey();
        }

        public void DeleteExistingOrder() {

            Console.Clear();

            System.Console.WriteLine("Please give the customer ID number of the order you would like to delete:");

            double custID = Convert.ToDouble(Console.ReadLine());

            if (_repo.DeleteOrder(custID)) {
                System.Console.WriteLine("The order has been canceled");
            } else {
                System.Console.WriteLine($"The customer ID: {custID} cannot be found. Please try a different number.");
            }

            WaitForKey();
        }

        private void WaitForKey() {

            System.Console.WriteLine("Press any key...");

            Console.ReadKey();
        }

        private void Seed() {
            MailList makeup = new MailList(new DateTime(1998, 01, 14), new DateTime(1998, 07, 18), OrderStatus.Canceled, 000015732, 5, 88845732);
            MailList cases = new MailList(new DateTime(2008, 12, 15), new DateTime(2009, 04, 23), OrderStatus.Complete, 0000159892, 70, 88845902);
            MailList flowers = new MailList(new DateTime(2011, 03, 16), new DateTime(2011, 03, 26), OrderStatus.EnRoute, 000015467, 150, 88845673);

            _repo.AddDeliveryToHistory(makeup);
            _repo.AddDeliveryToHistory(cases);
            _repo.AddDeliveryToHistory(flowers);
        }
    }