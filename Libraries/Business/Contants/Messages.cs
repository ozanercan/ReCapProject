namespace Business.Contants
{
    public static class Messages
    {
        public static string CarAdded => "Araç kayıt edildi.";
        public static string CarNotAdded => "Araç kayıt edilemedi.";
        public static string CarUpdated => "Araç güncellendi.";
        public static string CarNotUpdated => "Araç güncellemedi.";
        public static string CarDeleted => "Araç silindi.";
        public static string CarNotDeleted => "Araç silinemedi.";
        public static string CarNotFound => "Araç bulunamadı.";
        public static string CarNotFoundByBrand => "Markaya ait araç bulunamadı.";
        public static string CarNotFoundByColor => "Bu renkte araç bulunamadı.";

        public static string CarGet => "Araç getirildi.";
        public static string CarGetListByBrand => "Markaya ait araçlar getirildi.";
        public static string CarGetListByRegistered => "Kayıtlı araçlar getirildi.";
        public static string CarGetListByColor => "Bu renkte olan araçlar getirildi.";

        public static string CarDailyPriceInvalid => "Aracın günlük fiyatı 0'dan büyük olmalıdır. İşleminiz başarısız.";
        public static string CarDescriptionInvalid => "Aracın açıklaması 2 karakterden fazla olmalıdır. İşleminiz başarısız.";

        public static string BrandAdded => "Marka kayıt edildi.";
        public static string BrandDeleted => "Marka silindi.";
        public static string BrandUpdated => "Marka güncellendi.";

        public static string BrandNotAdded => "Marka kayıt edilemedi.";
        public static string BrandNotDeleted => "Marka silinemedi.";
        public static string BrandNotFound => "Marka bulunamadı.";
        public static string BrandNotUpdated => "Marka güncellenemedi.";

        public static string BrandGet => "Marka getirildi.";
        public static string BrandGetListByRegistered => "Kayıtlı markalar getirildi.";

        public static string ColorAdded => "Renk kayıt edildi.";
        public static string ColorDeleted => "Renk silindi.";
        public static string ColorUpdated => "Renk güncellendi.";
        public static string ColorNotAdded => "Renk kayıt edilemedi.";
        public static string ColorNotDeleted => "Renk silinemedi.";
        public static string ColorNotFound => "Renk bulunamadı.";
        public static string ColorNotUpdated => "Renk güncellenemedi.";
        public static string ColorGet => "Renk getirildi.";
        public static string ColorGetListByRegistered => "Kayıtlı Renklar getirildi.";
    }
}