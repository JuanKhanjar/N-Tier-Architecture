namespace N_Tier_Architecture.data.QueryObjects
{
    public class OrderQueryParameters
    {
        public Guid? OrderId { get; set; } // البحث بناءً على معرف الطلب
        public Guid? CustomerId { get; set; } // البحث بناءً على معرف العميل
        public DateTime? StartDate { get; set; } // نطاق تاريخ البداية
        public DateTime? EndDate { get; set; } // نطاق تاريخ النهاية
        public bool IncludeOrderDetails { get; set; } = false; // تضمين تفاصيل الطلب
        public bool IncludeCustomer { get; set; } = false;
    }
}
