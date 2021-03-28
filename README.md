# Araç Kiralama Projesi

Merhaba, bu proje sayın <b>Engin Demiroğ</b>'un ücretsiz olarak sunduğu 'Yazılım Geliştirici Yetiştirme Kampı' için verilen ödevler doğrultusunda geliştirilmiştir. 

## Kullanılan Teknolojiler & Yapılar

<ul>
    <li>Back-End
    <ul>
        <li>C# Vers. 7.3</li>
        <li>Restful Web Api Vers. .Net Core 3.1</li>
        <li>Katmanlı Mimari</li>
        <li>Temel Result Türü</li>
        <li>Interceptor, Aspectler</li>
        <li>Cache, Validate, Authorize Aspect</li>
        <li>Autofac</li>
        <li>Fluent Validation</li>
        <li>Json Web Token</li>
        <li>Repository Design Pattern</li>
        <li>Asenkron Yapı</li>
    </ul>
    </li>
    <li>Front-End
    <ul>
        <li>Angular 11</li>
        <li>Bootstrap 5.0</li>
        <li>HttpClient Interceptor</li>
        <li>Temel Guard</li>
        <li>Özel Pipeler</li>
    </ul>
    </li>
</ul>

## Temel Ayarlar
ReCapProject içerisinde Libraries > DataAccess > Concrete > EntityFramework > Contexts içerisinde bulunan <b>ReCapContext</b> dosyasının içerisine kullanılacak veritabanı türünün ayarları ve bağlantı cümlesi yapılmalıdır. Varsayılan ayarlar Sql Server içindir.

Token ayarları için, Presentations > WebAPI > <b>appsettings.json</b> dosyasına token kimliği ile ilgili ayar yapılması önerilir.

ReCapProject içerisinde Presentations > AngularUI > src > environments klasörü içerisinde bulunan <b>environment.ts</b> dosyasının içerisindeki <b>apiUrl</b> adlı değişkene Web API bağlantısı için gerekli ip adresi ve portu verilmelidir.

## Kullanım
*Komut İsteminde AngularUI klasörü seçili olmalıdır.*

Aşağıdaki kod, projede kullanılan ancak sizin cihazınıdaki eksik paketleri indirecektir.
```bash
npm install
```

Aşağıdaki kod, Angular projesini ayağa kaldırmak için kullanılır.
```bash
ng serve --open
```

## Temel Tablo Tanımları
#### Marka & Renk
Sisteme markalar ve renkler tanımlanır, kayıt edilecek araçların markası ve rengi seçilir.

#### Araç
Araçlar; Marka, Renk, Model Yılı, Günlük Fiyat, Açıklama ve En Düşük Kredi Skoru alanlarına sahiptir.

#### Müşteri
Müşteriler; Firma Adı, İsim, Soyisim, E-Posta, Şifre ve Durum alanlarına sahiptir.

#### Kiralama
Kiralama; Araç, Müşteri, Kira Tarihi, Kira Bitiş Tarihi alanlarına sahiptir.

#### Ödeme
Ödeme; Kira Kimliği, Ödenen Fiyat alanlarına sahiptir.

#### Müşteri Kredi Kartları
Kredi Kartları; Müşteri, Kart Sahibinin Adı ve Soyadı, Kart Numarası, Son Kullanma Tarihi ve Cvv Kodu alanlarına sahiptir.

## Özellikler
#### Araç İle İlgili
Her bir araç için <b>5</b> adet fotoğraf sınırı ayarlanmıştır. Fotoğraflar benzersiz isimlerle saklanır. Aracın hiç fotoğrafı yoksa varsayılan bir resim gösterilir.

#### Kira İle İlgili
Müşteri sistemi kullanarak istediği bir aracı belirlediği tarihler arasında kiralayabilir. <br>
Müşterinin Findeks numarası hesaplanır, aracın Findeks Numarasıyla karşılaştırılır, müşterinin Findeks numarası büyükse aracı kiralayabilir.<br>
Belirtilen tarihler arasında araç başka bir müşteriye kiralanmışsa ekranda uyarı çıkacaktır.<br>
Kiralama işlemi bittikten sonra <b>Ödeme Sayfası</b> açılacaktır.<br>

#### Ödeme İle İlgili
Ödeme sayfasında Kart bilgilerini doldurmak için bir form ve yan tarafında sanal bir kart tasarımı vardır. Sanal Kartın arasında daha önce müşteri bir kartını kayıt etmişse, kayıtlı kartlar listelenecektir. Kayıtlı bir kart seçildiği zaman gerekli alanlar otomatik doldurulacaktır.<br>
Kart bilgileri girilip <b>Ödemeyi Tamamla</b> butonuna basınca ödeme işlemi tamamlanacak ve kartın sisteme kayıt edilip edilmeyeceği sorulur. 

#### Kayıt İle İlgili
Kayıt olmayan birisi sistemde belirlenen sayfalara erişemez ve bir uyarı mesajı ile birlikte kayıt sayfasına yönlendirilir.

#### Kullanıcı İle İlgili
Kullanıcı menüden hızlıca kayıt olabilir ya da giriş yapabilir. Giriş yaptığı zaman aynı yerden profilini düzenleyebilir.

#### Yetki İle İlgili
Her kullanıcının yetkileri özelleştirilebilir. Back-End tarafında kullanılan her metot kullanıcının yetkileriyle çalışır. Kullanıcının yapmaya çalıştığı işlem için yetkisi yoksa durumla ilgili bir uyarı mesajı alır.

#### Birkaç Görsel

![Image of Yaktocat](https://raw.githubusercontent.com/ozanercan/ReCapProject/master/previewImages/detayliarama.JPG)


![Image of Yaktocat](https://raw.githubusercontent.com/ozanercan/ReCapProject/master/previewImages/aracdetay.JPG)

![Image of Yaktocat](https://raw.githubusercontent.com/ozanercan/ReCapProject/master/previewImages/kira.jpg)

![Image of Yaktocat](https://raw.githubusercontent.com/ozanercan/ReCapProject/master/previewImages/odeme.JPG)


## License
[MIT](https://choosealicense.com/licenses/mit/)
