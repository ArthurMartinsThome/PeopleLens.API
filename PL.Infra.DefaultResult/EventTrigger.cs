using PL.Infra.DefaultResult.Model.Enum;

namespace PL.Infra.DefaultResult
{
    internal class EventTrigger
    {
        internal static EEvent? GetEventId<TLog>(TLog? oldObject, TLog? newObject)
        {
            //if (oldObject is Order)
            //{
            //    var oldOrder = oldObject as Order;
            //    var newOrder = newObject as Order;
            //    if (oldOrder != null && newOrder != null && oldOrder.Status != EStatusOrder.Canceled && newOrder.Status == EStatusOrder.Canceled)
            //        return Domain.Model.En.EEvent.OrderCancellation;
            //}
            //if (oldObject is SellerAddStep1Identify)
            //{
            //    var oldSeller = oldObject as SellerAddStep1Identify;
            //    var newSeller = newObject as SellerAddStep1Identify;
            //    if (oldSeller != null && newSeller != null)
            //    {
            //        if (oldSeller.StatusId != newSeller.StatusId)
            //            switch ((EStatusSeller)newSeller.StatusId)
            //            {
            //                case EStatusSeller.Active:
            //                    return EEvent.SellerActivated;
            //                case EStatusSeller.Inactive:
            //                    return EEvent.SellerInactivated;
            //                case EStatusSeller.Blocked:
            //                    return EEvent.SellerBlocked;
            //                case EStatusSeller.Hidden:
            //                    return EEvent.SellerHidden;
            //            }
            //    }
            //}

            return null;
        }
    }
}