//burası dönkümanların tutulacağı cs dosyası

using System ; // datetime değişkenini kullanmak için eklendi

namespace StajYonetimSistemi.Models
{
    public class Document
    {
        public int id {get ; init ;} // evrakların karışmaması için etiket
        public int StudentId {get; set;} //her evrağın hangi öğrenciye ait olduğunu etiketlemek için 
        public DateTime UploadDate { get; set; } // yükleme tarihi
        public DocumentStatus Status { get; set; } = DocumentStatus.Yuklendi; // dökümanın durumu için ennum kullanıyorz ve defoult olarak yüklendi atıyoruz
    }
}