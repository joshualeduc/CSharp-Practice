using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages the vendors from whom we purchase our inventory.
    /// </summary>
    public class Vendor 
    {
        public enum IncludeAddress { Yes, No };
        public enum SendCopy { Yes, No };
        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }

        //Instead of using these overload functions, a better approach would be to use default values in our function with 4 parameters.
        /// <summary>
        /// Sends a product order to the vendor.
        /// </summary>
        /// <param name="product">Product to order.</param>
        /// <param name="quantity">Quantity of the product to order.</param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity)
        {
            return PlaceOrder(product, quantity, null, null); //This is called method chaining, where all the code is placed in the overloaded function with the most parameters
        }

        /// <summary>
        /// Sends a product order to the vendor.
        /// </summary>
        /// <param name="product">Product to order.</param>
        /// <param name="quantity">Quantity of the product to order.</param>
        /// <param name="deliveryBy">Requested delivery date.</param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliveryBy)
        {
            return PlaceOrder(product, quantity, deliveryBy, null);
        }

        /// <summary>
        /// Sends a product order to the vendor.
        /// </summary>
        /// <param name="product">Product to order.</param>
        /// <param name="quantity">Quantity of the product to order.</param>
        /// <param name="deliveryBy">Requested delivery date.</param>
        /// <param name="instructions">Delivery instructions.</param>
        /// <returns></returns> 
        //A good tip with parameters, list them in this order:
        //- Acted upon or key to the operation
        //- Required for the operation
        //- Flags
        //- Optional parameters

        public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliveryBy, string instructions)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));
            if (deliveryBy <= DateTimeOffset.Now)
                throw new ArgumentOutOfRangeException(nameof(deliveryBy));

            var success = false;

            var orderText = $"Order from Acme, Inc{System.Environment.NewLine}" +
                            $"Product: {product.ProductCode}{System.Environment.NewLine}" +
                            $"Quantity: {quantity}";
            if (deliveryBy.HasValue)
            {
                orderText += $"{System.Environment.NewLine}" +
                             $"Deliver By: {deliveryBy.Value.ToString("d")}";
            }
            if (!String.IsNullOrWhiteSpace(instructions))
            {
                orderText += $"{System.Environment.NewLine}" +
                             $"Instructions: {instructions}";
            }

            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New Order", orderText, this.Email);
            if (confirmation.StartsWith("Message sent:"))
            {
                success = true;
            }
            var operationResult = new OperationResult(success, orderText);
            return operationResult;
        }

        /// <summary>
        /// Sends a product order to the vendor.
        /// </summary>
        /// <param name="product">Product to order</param>
        /// <param name="quantity">Quantity of the product to order.</param>
        /// <param name="includeAddress">True to include the shipping address.</param>
        /// <param name="sendCopy">True to send a copy of the email to the current</param>
        /// <returns>Success flag and order text</returns>
        //public OperationResult PlaceOrder(Product product, int quantity, bool includeAddress, bool sendCopy)
        //{
        //    var orderText = "Test";
        //    if (includeAddress) orderText += " With Address";
        //    if (sendCopy) orderText += " With Copy";

        //    var operationResult = new OperationResult(true, orderText);
        //    return operationResult;
        //}
        //example using enums instead of booleans
        public OperationResult PlaceOrder(Product product, int quantity, IncludeAddress includeAddress, SendCopy sendCopy)
        {
            var orderText = "Test";
            if (includeAddress == IncludeAddress.Yes) orderText += " With Address";
            if (sendCopy == SendCopy.Yes) orderText += " With Copy";

            var operationResult = new OperationResult(true, orderText);
            return operationResult;
        }

        public override string ToString()
        {
            string vendorInfo = $"Vendor: {this.CompanyName}";
            if (!String.IsNullOrWhiteSpace(vendorInfo))
            {
                //Strings are an immutable reference type that acts as a value type. These methods are all making a copy of the original string.
                string result;
                result = vendorInfo.ToLower();
                result = vendorInfo.ToUpper();
                result = vendorInfo.Replace("Vendor", "Supplier");

                var length = vendorInfo.Length; //Length is a property, not a method
                var index = vendorInfo.IndexOf(":");
                var begins = vendorInfo.StartsWith("Vendor");
            }
            return vendorInfo;
        }

        public string PrepareDirections()
        {
            var directions = @"Insert \r\n to define a new line"; // \r\n will create a return, adding @ in front of the string will print the verbatim string (no return)
            return directions; 
        }

        //3 ways to make a new line in C#
        public string PrepareDirectionsOnTwoLines()
        {
            var directions = "First do this" + Environment.NewLine + "Then do that";
            var directions2 = "First do this\r\nThen do that";
            var directions3 = @"First do this
Then do that"; //tabs to align the code would put tabs in the string this way.
            return directions;
        }



        /// <summary>
        /// Sends an email to welcome a new vendor.
        /// </summary>
        /// <returns></returns>
        public string SendWelcomeEmail(string message)
        {
            var emailService = new EmailService();
            var subject = ("Hello " + this.CompanyName).Trim();
            var confirmation = emailService.SendMessage(subject, message, this.Email);
            return confirmation;
        }
    }
}
