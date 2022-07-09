using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class VendingMachine
    {
        private long TotalCash;
        private long CurrentCash;
        private long ReturnCash;

        private State State = new State();
        private Display Display = new Display();
        Dictionary<string, int> ProductCodePrice = new Dictionary<string, int>();

        public void InitializeProducts()
        {
            ProductCodePrice.Add("Cola", 100);
            ProductCodePrice.Add("Chips", 50);
            ProductCodePrice.Add("Candy", 65);
        }

        public void NewSession()
        {
            this.Display.ShowMessage("Press 'SelectProduct' to start selection");
            this.CurrentCash = 0;
            this.ReturnCash = 0;
        }

        public VendingMachine()
        {
            this.InitializeProducts();
            this.NewSession();
        }

        public void displayProducts()
        {
            foreach (var item in ProductCodePrice)
            {
                this.Display.ShowMessage("Item: " + item.Key + "   Price: " + item.Value);
            }
        }

        public void SelectProduct()
        {
            displayProducts();
            string product = Console.ReadLine();
            State.SetCurrentState(State.StateEnum.CollectingCash);
            int productPrice = ProductCodePrice[product];
            this.Display.ShowMessage("Product price: " + productPrice);
            while (this.CurrentCash < productPrice)
            {
                CollectCash();
                this.Display.ShowMessage("Current cash: " + this.CurrentCash);
            }
            DispenseProduct(product);
            this.ReturnCash += this.CurrentCash - productPrice;
            this.TotalCash += productPrice;
            DispenseReturnCash();
            this.Display.ShowMessage("THANK YOU\n\n");
            this.NewSession();
        }

        public void CollectCash()
        {
            int cash = Convert.ToInt32(Console.ReadLine());
            // check if cash is valid
            if (!(cash == 5 || cash == 10 || cash == 25))
            {
                // add it to return pile
                this.ReturnCash += cash;
                // errror
                this.Display.ShowMessage("INVALID COIN");
            }
            else
            {
                this.CurrentCash += cash;
            }
        }

        public void DispenseProduct(string product)
        {
            State.SetCurrentState(State.StateEnum.DispenseProduct);
            this.Display.ShowMessage("Dispensing product: " + product);
        }

        public void DispenseReturnCash()
        {
            State.SetCurrentState(State.StateEnum.ReturningCash);
            if (this.ReturnCash > 0)
            {
                this.Display.ShowMessage("Returning cash..");
                this.Display.ShowMessage("$" +  ((float) this.ReturnCash / 100));
            }
        }

        public void GetUserAction()
        {
            string action = Console.ReadLine();
            while (true)
            {
                try
                {
                    Enum.TryParse(action, out State.ActionEnum actionEnum);
                    switch (actionEnum)
                    {
                        case State.ActionEnum.SelectProduct:
                            SelectProduct(); break;
                        case State.ActionEnum.AddCash:
                            this.CollectCash();
                            break;
                        case State.ActionEnum.Cancel:
                            this.ReturnCash = this.CurrentCash;
                            this.DispenseReturnCash();
                            this.NewSession();
                            break;
                    }

                }
                catch (Exception e)
                {
                    this.Display.ShowMessage("INVALID USER SELECTION");
                }
            }
        }
    }
}
 