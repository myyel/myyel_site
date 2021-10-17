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
                    About2="3D mimari modelleme tekniği ile söz konusu projelere görselleştirme yapılmasının nasıl faydalar sağladığını bir düşünün! Projelerinin en iyi hale gelmesi, kontrol edilebilir maliyet, pazarlamacıların ve müşteri adaylarının her birine zaman ve para kaybı yaşamadan ürünlerini tanıtabilmeleri, projelerin inşaat öncesi yapısal sorunlarının tespiti ve çözümü için mimar ve mühendislere olan inanılmaz yardımı, sunum kalitesi, maliyet gibi nedenlerden dolayı 3d mimari tasarım yapı sektöründe vazgeçilmez bir ihtiyaçtır. İşte tam bu noktada sizlere anlatmış olduğumuz konu ile ilgili kaliteli hizmet vermek noktasında 3d görselleştirme hizmeti vermekteyiz.Umarız sizlere yardımcı olabiliriz.",
                    AdItemImageA="ad-1",
                    AdItemImageAText1="Bu gerçekçi ve detaylı görselleri, sizin için uygun bütçeli olarak tam vaktinde hazırlıyoruz!",
                    AdItemImageAText2="Görsel anlamda tüm beklentilerinizi karşılamak için projelerinizin tüm detaylarına dikkat edip hiç bir noktasını atlamadan çizimlerimize aktarıyoruz.",
                    AdItemImageB="ad-2",
                    AdItemImageBText1="Ofisimizin ilk hedefi müşterilerimizin fikirlerini hiç bir kayıp olmadan gerçekçi görsellere dönüştürmektir.",
                    AdItemImageBText2="Çalışma prensiplerimiz ve sistemimiz, dünyanın her yerinden tasarımcılara, mimarlara, proje geliştiricilerine, iç mimarlara ve üreticilere uyumlu bir hizmet verecek şekildedir.",
                    AdItemImageC="ad-3",
                    AdItemImageCText1="Müşterilerimiz için ister projelerinin tasarım aşamasında, ister tasarlanmış projenin görselleştirme aşamasında hizmet verebiliriz",
                    AdItemImageCText2="3 Boyutlu görselleştirme projeleriniz için bizden hemen teklif alın! Fiyat tekliflerimiz açık ve anlaşılır bir şekilde hazırlanmaktadır, kıyaslama ve değerlendirme yapabilirsiniz.",
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
                    SliderItemA1="Yenilikçi Yaklaşımlar,",
                    SliderItemA2="Yeni Nesil İç Mimari",
                    SliderItemB1="Yaşamak ve",
                    SliderItemB2="Yaşatmak için",
                    SliderItemC1="Düşünür,",
                    SliderItemC2="Tasarlar, Uygular"
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