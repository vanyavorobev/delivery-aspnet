
namespace hitsLab.presentation.dto.validation.Model
{
    public enum OrderStatusModel
    {
        InProcess, 
        Delivered
    }
    
    public static class OrderStatusMapper
    {
        public static string? ToString(OrderStatusModel model)
        {
            return model switch
            {
                OrderStatusModel.Delivered => "Delivered",
                OrderStatusModel.InProcess => "InProcess",
                _ => null
            };
        }
        
        public static OrderStatusModel ToModel(string model)
        {
            return model switch
            {
                "Delivered" => OrderStatusModel.Delivered,
                "InProcess" => OrderStatusModel.InProcess,
                _ => OrderStatusModel.Delivered
            };
        }
    }
}
