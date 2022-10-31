using System.ComponentModel;

namespace Api.Enums
{
    public enum OrderStatusEnum
    {
        [Description("Aguardando pagamento")]
        PendingPayment,
        [Description("Pagamento Aprovado")]
        ApprovedPayment,
        [Description("Enviado para Transportador")]
        Shipped,
        [Description("Entregue")]
        Completed,
        [Description("Cancelada")]
        Canceled
    }
}