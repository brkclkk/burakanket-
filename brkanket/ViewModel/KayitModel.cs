using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace brkanket.ViewModel
{
    public class KayitModel
    {
        public string kayitId { get; set; }
        public string kayitUyeId { get; set; }
        public string kayitSoruId { get; set; }
        public UyeModel UyeBilgi { get; set; }
        public SoruModel SoruBilgi { get; set; }
    }
}