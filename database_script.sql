-- Ticari Otomasyon Veritabanı Oluşturma Script'i
CREATE DATABASE DboTicariOtomasyon
GO

USE DboTicariOtomasyon
GO

-- Admin tablosu
CREATE TABLE TBL_ADMIN (
    ID int IDENTITY(1,1) PRIMARY KEY,
    KullaniciAd varchar(20),
    Sifre varchar(20)
)
GO

-- Ürünler tablosu
CREATE TABLE TBL_URUNLER (
    ID int IDENTITY(1,1) PRIMARY KEY,
    UrunAd varchar(50),
    Marka varchar(50),
    Model varchar(50),
    Yil char(4),
    Adet int,
    AlisFiyat decimal(18,2),
    SatisFiyat decimal(18,2),
    Detay varchar(MAX)
)
GO

-- Müşteriler tablosu
CREATE TABLE TBL_MUSTERILER (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Ad varchar(50),
    Soyad varchar(50),
    Telefon varchar(20),
    Telefon2 varchar(20),
    TC varchar(11),
    Mail varchar(50),
    Il varchar(20),
    Ilce varchar(30),
    Adres varchar(MAX),
    VergiDaire varchar(50)
)
GO

-- Firmalar tablosu
CREATE TABLE TBL_FIRMALAR (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Ad varchar(50),
    YetkiliStatu varchar(50),
    YetkiliAdSoyad varchar(50),
    Telefon1 varchar(20),
    Telefon2 varchar(20),
    Telefon3 varchar(20),
    Mail varchar(50),
    Fax varchar(20),
    Il varchar(20),
    Ilce varchar(30),
    VergiDaire varchar(50),
    Adres varchar(MAX),
    YetkiliTC varchar(11),
    Sektor varchar(50)
)
GO

-- Personeller tablosu
CREATE TABLE TBL_PERSONELLER (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Ad varchar(50),
    Soyad varchar(50),
    Telefon varchar(20),
    TC varchar(11),
    Mail varchar(50),
    Il varchar(20),
    Ilce varchar(30),
    Adres varchar(MAX),
    Gorev varchar(50)
)
GO

-- Giderler tablosu
CREATE TABLE TBL_GIDERLER (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Ay varchar(20),
    Yil char(4),
    Elektrik decimal(18,2),
    Su decimal(18,2),
    Dogalgaz decimal(18,2),
    Internet decimal(18,2),
    Maaslar decimal(18,2),
    Ekstra decimal(18,2),
    Notlar varchar(MAX)
)
GO

-- Bankalar tablosu
CREATE TABLE TBL_BANKALAR (
    ID int IDENTITY(1,1) PRIMARY KEY,
    BankaAdi varchar(50),
    Sube varchar(50),
    IBAN varchar(50),
    HesapNo varchar(20),
    Yetkili varchar(50),
    Tarih date,
    HesapTuru varchar(20)
)
GO

-- Fatura Bilgi tablosu
CREATE TABLE TBL_FATURABILGI (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Seri varchar(1),
    SiraNo varchar(10),
    Tarih date,
    Saat varchar(5),
    VergiDaire varchar(50),
    Alici varchar(50),
    TeslimEden varchar(50),
    TeslimAlan varchar(50)
)
GO

-- Fatura Detay tablosu
CREATE TABLE TBL_FATURADETAY (
    ID int IDENTITY(1,1) PRIMARY KEY,
    UrunAd varchar(50),
    Miktar int,
    Fiyat decimal(18,2),
    Tutar decimal(18,2),
    FaturaID int FOREIGN KEY REFERENCES TBL_FATURABILGI(ID)
)
GO

-- Notlar tablosu
CREATE TABLE TBL_NOTLAR (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Tarih date,
    Saat varchar(5),
    Baslik varchar(50),
    Detay varchar(MAX),
    Olusturan varchar(50),
    Hitap varchar(50)
)
GO

-- Örnek veriler - Admin
INSERT INTO TBL_ADMIN (KullaniciAd, Sifre) VALUES ('admin', '123456')
GO

-- Örnek veriler - Ürünler
INSERT INTO TBL_URUNLER (UrunAd, Marka, Model, Yil, Adet, AlisFiyat, SatisFiyat, Detay)
VALUES 
('Laptop', 'Dell', 'Latitude', '2024', 10, 15000.00, 17500.00, 'İş Bilgisayarı'),
('Telefon', 'Samsung', 'Galaxy S21', '2023', 15, 12000.00, 14000.00, 'Akıllı Telefon')
GO

-- Örnek veriler - Müşteriler
INSERT INTO TBL_MUSTERILER (Ad, Soyad, Telefon, Mail, Il, Ilce)
VALUES 
('Ahmet', 'Yılmaz', '5551234567', 'ahmet@mail.com', 'İstanbul', 'Kadıköy'),
('Mehmet', 'Kaya', '5559876543', 'mehmet@mail.com', 'Ankara', 'Çankaya')
GO

-- İndeksler
CREATE INDEX IX_URUNLER_AD ON TBL_URUNLER(UrunAd)
CREATE INDEX IX_MUSTERILER_TC ON TBL_MUSTERILER(TC)
CREATE INDEX IX_FIRMALAR_AD ON TBL_FIRMALAR(Ad)
GO

-- Görünümler
CREATE VIEW VW_URUN_STOK AS
SELECT UrunAd, Marka, Model, Adet, AlisFiyat, SatisFiyat
FROM TBL_URUNLER
WHERE Adet > 0
GO

CREATE VIEW VW_MUSTERI_FIRMA AS
SELECT 
    'Müşteri' as Tip,
    Ad + ' ' + Soyad as AdSoyad,
    Telefon,
    Mail,
    Il,
    Ilce
FROM TBL_MUSTERILER
UNION ALL
SELECT 
    'Firma' as Tip,
    Ad,
    Telefon1,
    Mail,
    Il,
    Ilce
FROM TBL_FIRMALAR
GO
