using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace myyel.Entity
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var homes = new List<HomeEntity>()
            {
                new HomeEntity()
                {
                    About1="3D mimari tasarım, yapı tasarımlarının ya da 3d çizim projelerinin süsleme çalışmasıdır. Bir başka ifadeyle yapıların iki boyutlu verilerden üç boyutlu simülasyon haline dönüştürülme işlemidir. Bu çalışmalar daha sonra tek tek görüntü olarak ya da 3d iç mekan veye dış cephe tasarım içerisine texture, güneş ışığı, yapay ışık, gölge etkisi gibi ayarlar yapılarak montajlanır. Böylece hazırlanan 3d modelleme çalışmaları özellikle henüz tamamlanmamış yapıların görselleştirilmesi ve bütçe bedelinin tespit edilmesinde son derece önemlidir. Çizim tekniğinin özelleştirilmesiyle oluşan görüntüler sayesinde farklı açılardan bina, ev, alışveriş merkezleri gibi yapıların iç tasarım ve dış cephe tasarım çalışmaları çok daha düşük maliyetle ve çok daha az zamanda görülebilir.",
                    About2="3D mimari modelleme tekniği ile söz konusu projelere görselleştirme yapılmasının nasıl faydalar sağladığını bir düşünün! Projelerinin en iyi hale gelmesi, kontrol edilebilir maliyet, pazarlamacıların ve müşteri adaylarının her birine zaman ve para kaybı yaşamadan ürünlerini tanıtabilmeleri, projelerin inşaat öncesi yapısal sorunlarının tespiti ve çözümü için mimar ve mühendislere olan inanılmaz yardımı, sunum kalitesi, maliyet gibi nedenlerden dolayı 3d mimari tasarım yapı sektöründe vazgeçilmez bir ihtiyaçtır. İşte tam bu noktada sizlere anlatmış olduğumuz konu ile ilgili kaliteli hizmet vermek noktasında 3d görselleştirme hizmeti vermekteyiz. Umarız sizlere yardımcı olabiliriz.",
                    AdItemImageA="ad-1",
                    AdItemImageAText1="Mimari projelerinizi gerçekçi ve detaylı bir şekilde tam vaktinde hazırlıyoruz",
                    AdItemImageAText2="Tüm beklentilerinize titizlikle cevap vermeye çalışıyoruz. Memnuniyetiniz en önemli önceliğimiz.",
                    AdItemImageB="ad-2",
                    AdItemImageBText1="Siz müşterilerimizin fikirleri ve beklentileri doğrultusunda en uygun hizmeti vermeye çalışıyoruz.",
                    AdItemImageBText2="Aklınızdaki tasarımı, görsele dökmek için elimizden geleni yapmaya çalışıyoruz.",
                    AdItemImageC="ad-3",
                    AdItemImageCText1="Projelerinize ister görsel olarak, ister video olarak da hizmet verebiliriz.",
                    AdItemImageCText2="Hem dış mimari, hem iç mimari, hem kat planı, hem de parça mimari tasarımlarınızı gönül rahatlığıyla bize yaptırabilirsiniz.",
                    AdTitle1="3 Boyutlu Görselleştime Hizmeti Almak için Doğru Adrestesiniz!",
                    AdTitle2="Müşterilerinize, yüksek kaliteli fotorealistik görsellerle etkileyici sunumlar yapabilirsiniz.",
                    ContactFacebook="myyeldesign",
                    ContactMail="myyeldesign@gmail.com",
                    ContactPhone="0 544 295 19 87",
                    ContactTitleA="HAYDİ BAŞLAYALIM",
                    ContactTitleB="Bizimle tanışmak ve hizmetlerimiz hakkında bilgi almak için aşağıdaki iletişim kanallarından bizimle irtibata geçebilirsiniz.",
                    ContactInsta="myyeldesign",
                    FooterImage="logo01 kopya",
                    FooterText="Ilham verici tasarımlar saglar...",
                    LessonItem1="Başlangıçtan İleri Seviyeye",
                    LessonItem2="Bina Modelleme",
                    LessonItem3="Uygun Malzeme Kullanımı",
                    LessonItem4="Render",
                    LessonItem5="25 Derste A'dan Z'ye Uygulamalı Eğitim",
                    LessonItem6="7/24 Erişim",
                    LessonTitle="MİMARİ MODELLEME ÖZEL DERS",
                    ProjectImage1="project3",
                    ProjectImage2="project1",
                    ProjectImage3="project2",
                    SliderItemA1="Modern Yaklaşımlar,",
                    SliderItemA2="Modern Mimari",
                    SliderItemB1="En üst kalitede",
                    SliderItemB2="Profesyonel Hizmet",
                    SliderItemC1="Mimari Görselleştirme için",
                    SliderItemC2="Doğru adres"
                }
            };

            foreach (var home in homes)
            {
                context.HomeEntities.Add(home);
            }


            var counts = new List<Counter>()
            {
                new Counter()
                {
                    Id=1,
                    Count=0
                }
            };
            foreach(var count in counts)
            {
                context.counters.Add(count);
            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}