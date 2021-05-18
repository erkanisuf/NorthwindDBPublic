using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{// Custom Function to get from the param href the orderid and product id
    public static class ProductAndOrder
    {
        public static ProductIdOrderId FindIds(string id) {
            ProductIdOrderId resultIDs = new ProductIdOrderId();
            string searchParam = "product"; // these are from the href param e.g 12345product31
            string orderParam = "order";
            int index = id.IndexOf(searchParam); // finds index of 'product in href'

            if (index != -1)
            {
                var result = id.Substring(index + searchParam.Length); // and takes value after 'product' which are product`s id
                var resultOrder = id.Substring(id.IndexOf(orderParam) + orderParam.Length, 5);

                int val = 0;
                if (Int32.TryParse(resultOrder, out val))
                {
                    resultIDs.productid = Convert.ToInt32(result);
                    resultIDs.orderid = Convert.ToInt32(resultOrder);
                }

            }
            return resultIDs;
        }




    }
    public class ProductIdOrderId
    {
        public int productid { get; set; }
        public int orderid { get; set; }
    }
}
