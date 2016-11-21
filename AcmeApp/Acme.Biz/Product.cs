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
        //const vs readonly
        //    const
        //        - compile-time constant
        //        - assigned to an expression evaluated at compile time
        //        - assigned on declaration
        //        - only number, boolean, or string
        //        - always static
        //    readonly
        //        - runtime constant
        //        - assigned to any valid expression at runtime
        //        - assigned on declaration or constructor
        //        - any data type
        //        - optionally static
        //Generally constants are for simple data types that will never change, while read-only is used for defining a field that comes from a file, table, or code but then should never be changed

        public const double InchesPerMeter = 39.37;
        public readonly decimal MinimumPrice;

        #region Constructors
        public Product()
        {
            Console.WriteLine("Product instance created");
            //Initialize an object in the constructor when its always needed by your class
            //this.ProductVendor = new Vendor();
            this.MinimumPrice = .96m;
            this.Category = "Tools";
        }

        public Product(int productId,
                        string productName,
                        string description): this()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Description = description;
            if (ProductName.StartsWith("Bulk"))
            {
                this.MinimumPrice = 9.99m;
            }

            Console.WriteLine($"Product instance has a name: {ProductName}");
        }
        #endregion

        #region Properties
        //Backing Fields(the private variables) have default initialization values if none are set - int 0, bool false, string/object null, dateType 1/1/0001, etc

        //Typical uses for a getter:
        //- Check user's credentials
        //- Check application state
        //- formate the returned value
        //- log
        //- lazy loading
        //Other additional uses for a setter - validate incoming value, format/convert/cleanup
        private DateTime? availabilityDate; //nullable type, the question mark helps check for null value vs default value

        public DateTime? AvailabilityDate
        {
            get { return availabilityDate; }
            set { availabilityDate = value; }
        }

        private string productName;

        public string ProductName
        {
            get {
                var formattedValue = productName?.Trim(); //if null, then null. if not, then dot
                return formattedValue;
            }
            set {
                if(value.Length < 3)
                {
                    ValidationMessage = "Product Name must be at least 3 characters";
                }
                else if (value.Length > 20)
                {
                    ValidationMessage = "Product Name cannot be more than 20 characters";
                }
                else
                {
                    productName = value;
                }
            }
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

        internal string Category { get; set; } //change had to be made in assemblyInfo so that our tests could access Category
        public int SequenceNumber { get; set; } = 1;

        public string ProductCode => $"{this.Category}-{this.SequenceNumber}";


        public string ValidationMessage { get; private set; }

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
