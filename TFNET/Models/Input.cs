using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFNET.Models
{
    public class Input
    {
        public string UserId { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string UserType { get; set; }
        public string TransactionId { get; set; }
        public string Timestamp { get; set; }
        public string ItemId { get; set; }
        public string Quantity { get; set; }
        public string Value { get; set; }
        public string Location { get; set; }
        public string ProductCategory { get; set; }
        public string Group { get; set; }
        public string ChurnPeriod { get; set; }

        public List<string> Genders { get; set; } = new List<string>();

        public string result { get; set; }

        public Dictionary<string,string> dictionary()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("UserId", UserId);
            dic.Add("Age", Age);
            dic.Add("Address", Address);
            dic.Add("Gender", Gender);
            dic.Add("UserType", UserType);
            dic.Add("TransactionId", TransactionId);
            dic.Add("Timestamp", Timestamp);
            dic.Add("ItemId", ItemId);
            dic.Add("Quantity", Quantity);
            dic.Add("Value", Value);
            dic.Add("Location", Location);
            dic.Add("ProductCategory", ProductCategory);
            dic.Add("Group", Group);
            dic.Add("ChurnPeriod", ChurnPeriod);

            return dic;
        }
    }
}