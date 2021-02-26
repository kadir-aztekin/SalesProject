﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        //MAGİC STRING YANI HAZIR MESAJLARIMIZI BURAYA YAZIYORUZ GEREKLI YERLERDE KULLANACAGIZ
        // İLERDE MESAJLARI DEGISTIRMEK ISTEDIGINIZ DE BURDAN DIREK DEGISTIREBİLİRİZ 
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün İsim Geçersiz";
        public static string MainintenanceTime= "Sistem Bakımda";
        public static string ProductsListed =  "ÜRÜNLER LİSTELENDİ";
        public static string ProductUpdate = "Ürün Güncellendi";
        public static string OrderAdded = "Sipariş Eklendi";
        public static string OrderUpdate = "Sipariş Güncellendi";
        public static string OrderNotAdded = "Sipariş Eklenemedi";
        public static string CustomerDeleted = "Müşteri Silindi";
        public static string CustomerNotAdded = "Müşteri Eklenemedi";
        public static string CustomerAdded = "Müşteri Eklendi";
        public static string CustomerUpdate = "Müşteri Güncellendi";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün vardir";
        public static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var ";
        internal static string CategoryLimitExceded;
    }
}

