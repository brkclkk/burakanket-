using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using brkanket.Models;
using brkanket.ViewModel;

namespace brkanketform.Controllers
{
    public class ServisController : ApiController
    {
        brkanketEntities db = new brkanketEntities();
        SonucModel sonuc = new SonucModel();

        #region Üyeler

        [HttpGet]
        [Route("api/uyeliste")]

        public List<UyeModel> UyeListe()
        {
            List<UyeModel> liste = db.Uye.Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                uyeNo = x.uyeNo,
                uyeAdsoyad = x.uyeAdsoyad,
                uyeDogTarih = x.uyeDogTarih
            }).ToList();
            return liste;
        }


        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]

        public UyeModel uyebyid(string uyeId)
        {
            UyeModel kayit = db.Uye.Where(s => s.uyeId == uyeId).Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                uyeNo = x.uyeNo,
                uyeAdsoyad = x.uyeAdsoyad,
                uyeDogTarih = x.uyeDogTarih
            }).SingleOrDefault();
            return kayit;

        }

        [HttpPost]
        [Route("api/uyeekle")]

        public SonucModel UyeEkle(UyeModel model)
        {
            if (db.Uye.Count(s => s.uyeNo == model.uyeNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = " Girilen Üye No Kayıtlıdır!";
                return sonuc;
            }
            Uye yeni = new Uye();
            yeni.uyeId = Guid.NewGuid().ToString();
            yeni.uyeNo = model.uyeNo;
            yeni.uyeAdsoyad = model.uyeAdsoyad;
            yeni.uyeDogTarih = model.uyeDogTarih;
            db.Uye.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/uyeduzenle")]

        public SonucModel uyeduzenle(UyeModel model)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == model.uyeId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadı";
                return sonuc;
            }

            kayit.uyeNo = model.uyeNo;
            kayit.uyeAdsoyad = model.uyeAdsoyad;
            kayit.uyeDogTarih = model.uyeDogTarih;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public SonucModel UyeSil(string uyeId)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == uyeId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadı";
                return sonuc;
            }

            if (db.Kayit.Count(s => s.kayitUyeId == uyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Silinemez";
                return sonuc;
            }

            db.Uye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi";
            return sonuc;
        }
        #endregion

        #region Soru

        [HttpGet]
        [Route("api/soruliste")]

        public List<SoruModel> SoruListe()
        {
            List<SoruModel> liste = db.Soru.Select(x => new SoruModel()
            {
                soruId = x.soruId,
                soruKodu = x.soruKodu,
                soruAdi = x.soruAdi,
                soruSayisi = x.soruSayisi,
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/sorubyid/{soruId}")]

        public SoruModel SoruById(string soruId)
        {
            SoruModel kayit = db.Soru.Where(s => s.soruId == soruId).Select(x => new SoruModel()
            {
                soruId = x.soruId,
                soruKodu = x.soruKodu,
                soruAdi = x.soruAdi,
                soruSayisi = x.soruSayisi,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/soruekle")]
        public SonucModel SoruEkle(SoruModel model)
        {
            if (db.Soru.Count(s => s.soruKodu == model.soruKodu) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Soru Kodu Kayıtlıdır";
            }
            Soru yeni = new Soru();
            yeni.soruId = Guid.NewGuid().ToString();
            yeni.soruKodu = model.soruKodu;
            yeni.soruAdi = model.soruAdi;
            yeni.soruSayisi = model.soruSayisi;
            db.Soru.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/soruduzenle")]

        public SonucModel SoruDuzenle(SoruModel model)
        {
            Soru kayit = db.Soru.Where(s => s.soruId == model.soruId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Soru Bulunamadı";
                return sonuc;
            }
            kayit.soruKodu = model.soruKodu;
            kayit.soruAdi = model.soruAdi;
            kayit.soruSayisi = model.soruSayisi;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/sorusil/{soruId}")]
        public SonucModel SoruSil(string soruId)
        {
            Soru kayit = db.Soru.Where(s => s.soruId == soruId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Soru Bulunamadı";
                return sonuc;
            }
            if (db.Kayit.Count(s => s.kayitSoruId == soruId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Soru Ankete Kayıtlı";
                return sonuc;
            }

            db.Soru.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Bilgileri Silindi";
            return sonuc;
        }

        #endregion

        #region Kayıt
        [HttpGet]
        [Route("api/sorukullaniciliste/{soruId}")]

        public List<KayitModel> SoruKullaniciListe(string soruId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitSoruId == soruId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitUyeId = x.kayitUyeId,
                kayitSoruId = x.kayitSoruId,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.SoruBilgi = SoruById(kayit.kayitSoruId);
                kayit.UyeBilgi = uyebyid(kayit.kayitUyeId);
            }
            return liste;
        }

        [HttpGet]
        [Route("api/kullanicisoruliste/{kullaniciId}")]

        public List<KayitModel> kullanicisoruliste(string soruId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitUyeId == soruId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitUyeId = x.kayitUyeId,
                kayitSoruId = x.kayitSoruId,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.SoruBilgi = SoruById(kayit.kayitSoruId);
                kayit.UyeBilgi = uyebyid(kayit.kayitUyeId);
            }
            return liste;
        }

        [HttpPost]
        [Route("api/kayitekle")]
        public SonucModel KayitEkle(KayitModel model)
        {
            if (db.Kayit.Count(s => s.kayitUyeId == model.kayitUyeId && s.kayitSoruId == model.kayitSoruId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu Üye Ankete Kayıtlıdır";
                return sonuc;

            }

            Kayit yeni = new Kayit();
            yeni.kayitId = Guid.NewGuid().ToString();
            yeni.kayitSoruId = model.kayitSoruId;
            yeni.kayitUyeId = model.kayitUyeId;
            db.Kayit.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Soru Ankete Kayıt Edildi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kayitsil/{kayitId}")]
        public SonucModel KayitSil(string kayitId)
        {
            Kayit kayit = db.Kayit.Where(s => s.kayitId == kayitId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            db.Kayit.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Kaydı Silindi";

            return sonuc;
        }

        #endregion

    }
}

