namespace N_Tier_Architecture.data.QueryObjects
{
    public class CategoryQueryParameters
    {
        public Guid? CategoryId { get; set; } // البحث بناءً على معرف الفئة
        public string? CategoryName { get; set; } // البحث بناءً على اسم الفئة
        public bool IncludeProducts { get; set; } = false; // تضمين المنتجات المرتبطة بالفئة
    }
}
