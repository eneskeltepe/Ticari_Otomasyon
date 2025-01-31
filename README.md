# Ticari Otomasyon Yazılımı

<div align="center">

![Ana Sayfa](Screenshots/1%20-%20Anasayfa.png)

</div>

## 📋 İçindekiler

- [Proje Hakkında](#-proje-hakkında)
- [Özellikler](#-özellikler)
- [Gereksinimler](#-gereksinimler)
- [Kurulum](#-kurulum)
  - [DevExpress Kurulumu](#devexpress-kurulumu)
  - [Veritabanı Kurulumu](#veritabanı-kurulumu)
- [Kullanım](#-kullanım)
- [Ekran Görüntüleri](#-ekran-görüntüleri)
- [Teknik Detaylar](#-teknik-detaylar)
- [Katkıda Bulunma](#-katkıda-bulunma)
- [Lisans](#-lisans)

## 🎯 Proje Hakkında

Bu proje, işletmelerin ticari işlemlerini yönetmelerine yardımcı olan kapsamlı bir otomasyon yazılımıdır. C# ve SQL Server kullanılarak geliştirilmiştir. DevExpress bileşenleri ile modern ve kullanıcı dostu bir arayüz sunmaktadır.

## ✨ Özellikler

- **📦 Ürün Yönetimi**
  - Ürün ekleme, silme, güncelleme
  - Stok takibi
  - Barkod sistemi

- **👥 Müşteri ve Firma Yönetimi**
  - Müşteri/firma bilgilerinin kaydı
  - Detaylı müşteri/firma takibi
  - İletişim bilgileri yönetimi

- **👨‍💼 Personel Yönetimi**
  - Personel bilgileri
  - Yetkilendirme sistemi

- **💰 Finans Yönetimi**
  - Kasa işlemleri
  - Gelir-gider takibi
  - Banka işlemleri

- **📄 Fatura ve Raporlama**
  - Fatura oluşturma ve takibi
  - Detaylı raporlama araçları
  - PDF çıktı alma

- **🔔 Diğer Özellikler**
  - Not sistemi
  - Mail gönderimi
  - Rehber
  - Ajanda

## 💻 Gereksinimler

- Windows 10 veya üzeri
- Visual Studio 2019 veya üzeri
- SQL Server 2014 veya üzeri
- .NET Framework 4.7.2
- DevExpress v22.2.3.0

## 🚀 Kurulum

### DevExpress Kurulumu

1. Visual Studio'yu açın
2. Tools > Extensions and Updates menüsüne gidin
3. Online sekmesinde "DevExpress" araması yapın
4. DevExpress için deneme sürümünü veya lisanslı sürümü yükleyin

**Önemli Notlar:**
- DevExpress ticari bir üründür ve lisans gerektirir
- Deneme sürümü ile projeyi test edebilirsiniz
- Ticari kullanım için lisans satın almanız gerekmektedir

### Veritabanı Kurulumu

1. SQL Server Management Studio'yu açın
2. `database_script.sql` dosyasını yeni bir sorgu penceresinde açın
3. Script'i çalıştırın (F5)
4. Oluşturulan veritabanı: `DboTicariOtomasyon`

### Bağlantı Ayarları

1. `sqlbaglantisi.cs` dosyasını açın
2. Aşağıdaki bağlantı bilgilerini kendi SQL Server ayarlarınıza göre güncelleyin:
```csharp
Data Source=SUNUCU_ADI;Initial Catalog=DboTicariOtomasyon;Integrated Security=True
```

### Varsayılan Giriş Bilgileri
- **Kullanıcı Adı:** admin
- **Şifre:** 123456

## 📱 Kullanım

1. Projeyi derleyin ve çalıştırın
2. Giriş ekranında varsayılan kullanıcı bilgileriyle giriş yapın
3. Ana menüden istediğiniz modülü seçerek kullanmaya başlayın

## 📸 Ekran Görüntüleri

<details>
<summary>Tüm ekran görüntülerini göster</summary>

### Modüller
| Modül | Görüntü |
|-------|----------|
| Ürünler | ![Ürünler](Screenshots/2%20-%20Ürünler.png) |
| Stoklar | ![Stoklar](Screenshots/3%20-%20Stoklar.png) |
| Müşteriler | ![Müşteriler](Screenshots/4%20-%20Müşteriler.png) |
| Firmalar | ![Firmalar](Screenshots/5%20-%20Firmalar.png) |
| Personeller | ![Personeller](Screenshots/6%20Personeller.png) |
| Giderler | ![Giderler](Screenshots/7%20-%20Giderler.png) |
| Kasa | ![Kasa Giriş](Screenshots/8%20-%20Kasa%20Giriş.png) |
| Notlar | ![Notlar](Screenshots/10%20-%20Notlar.png) |
| Bankalar | ![Bankalar](Screenshots/11%20-%20Bankalar.png) |
| Rehber | ![Rehber](Screenshots/12%20-%20Rehber.png) |
| Mail | ![Mail Paneli](Screenshots/13%20-%20Mail%20Paneli.png) |
| Faturalar | ![Faturalar](Screenshots/14%20-%20Faturalar.png) |
| Ayarlar | ![Ayarlar](Screenshots/18%20-%20Ayarlar.png) |

</details>

## 🔧 Teknik Detaylar

### Veritabanı Şeması

Proje aşağıdaki ana tabloları kullanmaktadır:
- TBL_URUNLER (Ürün bilgileri)
- TBL_MUSTERILER (Müşteri bilgileri)
- TBL_FIRMALAR (Firma bilgileri)
- TBL_PERSONELLER (Personel bilgileri)
- TBL_GIDERLER (Gider kayıtları)
- TBL_BANKALAR (Banka hesap bilgileri)
- TBL_FATURABILGI (Fatura başlık bilgileri)
- TBL_FATURADETAY (Fatura detay bilgileri)
- TBL_NOTLAR (Not kayıtları)

### Proje Yapısı

```
Ticari_Otomasyon/
│
├── Forms/
│   ├── FrmAdmin.cs          # Giriş ekranı
│   ├── FrmAnaModul.cs       # Ana menü
│   ├── FrmUrunler.cs        # Ürün yönetimi
│   └── ...
│
├── Reports/
│   ├── Report1.rdlc         # Müşteri raporları
│   └── ...
│
└── Database/
    └── sqlbaglantisi.cs     # Veritabanı bağlantı ayarları
```

## 🤝 Katkıda Bulunma

1. Bu depoyu fork edin
2. Yeni bir branch oluşturun (`git checkout -b yeni-ozellik`)
3. Değişikliklerinizi commit edin (`git commit -am 'Yeni özellik eklendi'`)
4. Branch'inizi push edin (`git push origin yeni-ozellik`)
5. Bir Pull Request oluşturun

Detaylı bilgi için [CONTRIBUTING.md](CONTRIBUTING.md) dosyasına bakınız.

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakınız.
