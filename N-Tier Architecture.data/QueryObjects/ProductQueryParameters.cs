namespace N_Tier_Architecture.data.QueryObjects
{
    public class ProductQueryParameters
    {
        public Guid? ProductId { get; set; } // البحث بناءً على معرف المنتج
        public string? ProductName { get; set; } // البحث بناءً على اسم المنتج
        public Guid? CategoryId { get; set; } // البحث بناءً على الفئة
        public decimal? MinPrice { get; set; } // نطاق السعر الأدنى
        public decimal? MaxPrice { get; set; } // نطاق السعر الأعلى
        public bool IncludeCategory { get; set; } = false; // تضمين الفئة المرتبطة بالمنتج
    }
}
