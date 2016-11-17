using Acme.Common; //references must only be one way
using static Acme.Common.LoggingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages products carried in inventory
    /// </summary>
    public class Product
    {
        public const double InchesPerMeter = 39.37;
        public readonly

        #region Constructors
        public Product()
        {
            Console.WriteLine("Product instance created");
            //Initialize an object in the constructor when its always needed by your class
            //this.ProductVendor = new Vendor();
        }

        public Product(int productId,
                        string productName,
                        string description): this()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Description = description;

            Console.WriteLine($"Product instance has a name: {ProductName}");
        }
        #endregion

        #region Properties
        //Backing Fields(the private variables) have default initialization values if none are set - int 0, bool false, string/object null, dateType 1/1/0001, etc
        private DateTime? availabilityDate; //nullable type, the question mark helps check for null value vs default value

        public DateTime? AvailabilityDate
        {
            get { return availabilityDate; }
            set { availabilityDate = value; }
        }

        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        private Vendor productVendor;

        public Vendor ProductVendor
        {
            get {
                if(productVendor == null)
                {
                    productVendor = new Vendor(); //lazy loading - initialize class only when it is needed
                }
                return productVendor;
            }
            set { productVendor = value; }
        }

        #endregion

        public string SayHello()
        {
            //initializing objects inside a method is used when that method is the only thing that needs the class
            //var vendor = new Vendor(); 
            //vendor.SendWelcomeEmail("Message from Product Class");

            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New Product",
                this.ProductName, "sales@abc.com");

            var result = LogAction("Saying hello again"); //doesn't need class name with the new 'using static' feature
            return $"Hello {ProductName} ({ProductId}): {Description} Available on: {AvailabilityDate?.ToShortDateString()}"; 
            //the question mark is needed here for ToShortDateString to work - C#5 would need if (AvailabilityDate.HasValue) instead?
        }
    }
}
