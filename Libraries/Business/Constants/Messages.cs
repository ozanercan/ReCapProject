using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.Serialization;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ClaimsListed = "Claimler listelendi.";
        public static string ClaimsNotFound = "Claimler bulunamadı.";
        public static string PasswordError = "Şifre yanlış.";
        public static string LoginSuccess = "Giriş başarılı.";
        public static string UserAlreadyExist = "Kullanıcı zaten var.";
        public static string AccessTokenCreated = "Access Token başarıyla oluşturuldu.";
        public static string CarNotFoundById = "Idsi verilen araç bulunamadı.";
        public static string CarBroughtById = "Idsi verilen araç getirildi.";
        public static string PaymentCancelled = "Ödeme iptal edildi.";
        public static string PaymentSuccessful = "Ödeme tamamlandı.";
        public static string CarNotFoundByFilters = "Filtrelere uygun araç bulunamadı.";
        public static string CarGetListByFilters = "Filtrelere uygun araçlar listelendi.";
        public static string CarRentPriceCalculated = "Aracın kira fiyatı hesaplandı.";
        public static string ReturnDateCantLessThanReturnDate = "Kira Bitiş tarihi, Kira Başlangıç tarihinden küçük olamaz.";
        public static string BrandNameAlreadyExist = "Böyle bir Marka Adı zaten kayıtlı.";
        public static string ModelInvalid = "Gönderdiğiniz model onaylanmadı, lütfen alanları kontrol edip tekrar deneyin.";
        public static string ColorNameAlreadyExist = "Bu renk zaten kullanılıyor.";
        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string CarCreditScoreNotAdded = "Araç Kredi Skoru oluşturulamadı.";
        public static string CarCreditScoreAdded = "Araç Kredi Skoru oluşturuldu.";
        public static string CarCreditScoreNotFound = "Araç Kredi Skoru bulunamadı.";
        public static string CarCreditScoreBrought = "Araç Kredi Skoru getirildi.";
        public static string CreditScoreCalculated = "Kredi Skoru hesaplandı.";
        public static string CustomerCreditScoreEnoughtToRentCar = "Müşterinin Kredi Skoru, Aracı kiralamak için yeterli.";
        public static string CustomerCreditScoreNotEnoughtToRentCar = "Müşterinin Kredi Skoru, Aracı kiralamak için yeterli değil.";
        public static string CustomerDetailBroughth = "Müşteri Detayı getirildi.";
        public static string CustomerCreditCardAdded = "Kredi Kartı eklendi.";
        public static string CustomerCreditCardNotAdded = "Kredi Kartı eklenemedi.";
        public static string CustomerCreditCardFound = "Kredi Kartı bulunamadı.";
        public static string CreditCardsListed = "Kredi Kartları listelendi.";

        public static string CarImageNotAdded => "Araç resmi eklenemedi.";
        public static string CarImageAdded => "Araç resmi eklendi.";
        public static string CarImageNotDeleted => "Araç resmi silinemedi.";
        public static string CarImageDeleted => "Araç resmi silindi.";
        public static string CarImageNotFound => "Araç resmi bulunamadı.";
        public static string CarImagesNotFound => "Sistemde kayıtlı araç resmi bulunamadı.";
        public static string CarImageNotUpdated => "Araç resmi güncellenemedi.";
        public static string CarImageUpdated => "Araç resmi güncellendi.";
        public static string CarImagesListed => "Araç resimleri listelendi.";
        public static string CarImageBrought => "Araç resmi getirildi.";
        public static string CarImageCountError => "Araç resim sınırını aştınız.";
        public static string CarImageNotUploaded => "Resim yüklenemedi.";
        public static string RegisteredCarImageNotDeleted => "Kayıtlı araç resmi silinemedi.";
        public static string CarAlreadyRented => "Araç belirlenen tarihler arasında zaten kiralanmış.";
        public static string CarInStock => "Araç stokta.";
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
        public static string CarDailyPriceInvalid => "Aracın günlük fiyatı 0'dan büyük olmalıdır.";
        public static string CarDescriptionInvalid => "Aracın açıklaması 2 karakterden fazla olmalıdır.";

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


        public static string UserAdded => "Kullanıcı başarıyla kayıt edildi.";
        public static string UserDeleted => "Kullanıcı silindi.";
        public static string UserUpdated => "Kullanıcı güncellendi.";
        public static string UserNotAdded => "Kullanıcı kayıt edilemedi.";
        public static string UserNotDeleted => "Kullanıcı silinemedi.";
        public static string UserNotFound => "Kullanıcı bulunamadı.";
        public static string UserNotUpdated => "Kullanıcı güncellenemedi.";
        public static string UserGet => "Kullanıcı getirildi.";
        public static string UserGetListByRegistered => "Kayıtlı kullanıcılar getirildi.";
        public static string CustomerAdded => "Müşteri kayıt edildi.";
        public static string CustomerDeleted => "Müşteri silindi.";
        public static string CustomerUpdated => "Müşteri güncellendi.";
        public static string CustomerNotAdded => "Müşteri kayıt edilemedi.";
        public static string CustomerNotDeleted => "Müşteri silinemedi.";
        public static string CustomerNotFound => "Müşteri bulunamadı.";
        public static string CustomerNotUpdated => "Müşteri güncellenemedi.";
        public static string CustomerGet => "Müşteri getirildi.";
        public static string CustomerGetListByRegistered => "Kayıtlı müşteriler getirildi.";
        public static string RentalAdded => "Kiralama kayıt edildi.";
        public static string RentalDeleted => "Kiralama silindi.";
        public static string RentalUpdated => "Kiralama güncellendi.";
        public static string RentalNotAdded => "Kiralama kayıt edilemedi.";
        public static string RentalNotDeleted => "Kiralama silinemedi.";
        public static string RentalNotFound => "Kiralama bulunamadı.";
        public static string RentalNotUpdated => "Kiralama güncellenemedi.";
        public static string RentalGet => "Kiralama getirildi.";
        public static string RentalListed => "Kiralamalar listelendi.";
        public static string RentalGetListByRegistered => "Kayıtlı kiralamalar getirildi.";
    }
}