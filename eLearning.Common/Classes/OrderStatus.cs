using System.Collections.Generic;

namespace eLearning.Common.Classes
{
    public enum OrderStatusKeys
    {
        Pending,
        Canceled,
        Paid
    }

    public class OrderStatus
    {
        public readonly List<KeyValuePair<OrderStatusKeys, string>> OrderStatuses = new List<KeyValuePair<OrderStatusKeys, string>>() {
                                    new KeyValuePair<OrderStatusKeys, string>(OrderStatusKeys.Pending, "Pending"),
                                    new KeyValuePair<OrderStatusKeys, string>(OrderStatusKeys.Canceled, "Canceled"),
                                    new KeyValuePair<OrderStatusKeys, string>(OrderStatusKeys.Paid, "Paid off"),
                                };
    }
}