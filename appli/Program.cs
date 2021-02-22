using System;
using System.Data.SqlClient;
using System.Threading;

namespace appli
{
    class Program
    {
        static void Main(string[] args)
        {

            #region SqlConnect
            start:  
             SqlConnection connection=new SqlConnection(@"Data Source=DESKTOP-0TTDRAK\SQLEXPRESS;Initial Catalog=users ;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            connection.Open();
            cmd.Connection=connection;
            #endregion
            
            #region Menü
            work wrk=new work();
            Console.WriteLine("Yapmak istediğiniz işlem numarasını Seçiniz ");
            Console.WriteLine(" 1 Kullanıcı Ekle \n 2 Kullanıcıyı Uygulama Bilgisi İle Ekle \n 3 Kullanıcı Sil \n 4 Uygulama Sil \n 5 Kullanıcıya Uygulama Ekle \n 6 Exit");
            int answer=Convert.ToInt16(Console.ReadLine());    
            #endregion

            #region Answer 1

            if(answer==1)
            {
                
                kullanici Ikullanici=new kullanici();
                Console.WriteLine("Kullanıcı Adini Giriniz");
                Ikullanici.ad=Console.ReadLine();
                Console.WriteLine("Kullanıcı SoyAdini Giriniz");
                Ikullanici.soyad=Console.ReadLine();
                Console.WriteLine("Kullanıcının Yas Giriniz");
                Ikullanici.yas=Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("Kullanıcının Cinsiyetini Giriniz ");
                Ikullanici.Cinsiyet=Console.ReadLine();
                Console.WriteLine("Kullanıcının MedeniDurumu Giriniz ");
                Ikullanici.Medenidurumu=Console.ReadLine();
                Console.WriteLine("Kullanıcının TelefonNo Giriniz ");
                Ikullanici.kullaniciTel=Console.ReadLine();
                

                wrk.KullaniciEkle(Ikullanici);
                Thread.Sleep(2000);
                Console.WriteLine("\n");
                goto start;
                
            }
            #endregion

            #region Answer 2
            if(answer==2)
            {
                kullanici Ikullanici=new kullanici();
                Console.WriteLine("Kullanıcı Adini Giriniz");
                Ikullanici.ad=Console.ReadLine();
                Console.WriteLine("Kullanıcı SoyAdini Giriniz");
                Ikullanici.soyad=Console.ReadLine();
                Console.WriteLine("Kullanıcının Yas Giriniz");
                Ikullanici.yas=Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("Kullanıcının Cinsiyetini Giriniz ");
                Ikullanici.Cinsiyet=Console.ReadLine();
                Console.WriteLine("Kullanıcının MedeniDurumu Giriniz ");
                Ikullanici.Medenidurumu=Console.ReadLine();
                Console.WriteLine("Kullanıcının TelefonNo Giriniz ");
                Ikullanici.kullaniciTel=Console.ReadLine();
                _uygulama Iuygulama=new _uygulama();

             cmd.CommandText=@"select *from Uygulama";
                SqlDataReader reader=cmd.ExecuteReader();
             while(reader.Read())
              {
                  var name =reader.GetInt32(0);
                 var  name2 =reader.GetString(1);
                 Console.WriteLine("Uygulama id "+name+" "+ name2);
              }
               
                Console.WriteLine("uygulama id giriniz ");
                Iuygulama.Uygulamaid=Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("uygulamada Geçen süreyi Giriniz");
                Iuygulama.Gecensure=Convert.ToInt16(Console.ReadLine());
               
                wrk.UygulamaBilgisiileEkle(Ikullanici,Iuygulama);
                  
                connection.Close();
                reader.Close();
                Thread.Sleep(2000);
                Console.WriteLine("\n");
              goto start;
            }
            #endregion

            #region Answer 3
            if (answer==3)
            {

                Console.WriteLine("Silmek istediğiniz kullanıcının ismini giriniz ");
                // string kullaniciisim=Console.ReadLine();
                cmd.CommandText=@"select * from Kullanici ";
                SqlDataReader reader=cmd.ExecuteReader();
                while (reader.Read())
                {
                    var kulid=reader.GetInt32(0);
                    var kulname=reader.GetString(1);
                    var kulsoyad=reader.GetString(2);
                    var kultel=reader.GetString(3);
                    Console.WriteLine("Kullanici id : "+kulid +" "+"Kullanici isim "+kulname+" "+kulsoyad+" "+"Telefon numarası "+kultel);
                }

                reader.Close();
                Console.WriteLine("Silmek istediğiniz Kullanicinin id'ni giriniz");
                int search=Convert.ToInt16(Console.ReadLine());
                cmd.CommandText=@"select * from UygulamaBilgileri where kullaniciid=@a1 ";
                cmd.Parameters.AddWithValue("@a1",search);
                SqlDataReader kontrol=cmd.ExecuteReader();
                int times=0;
                while (kontrol.Read())
                {
                    times++;
                
                }
                 if (times==0)
                    {
                        wrk.uygulamabilgisiolmadansil(search);
                        
                    }
                    else
                    {
                        Console.WriteLine("Kullanıcın Uygulama Bilgisi var silmek istermiiniz Evet=1 Hayır=0 ");
                        int f=Convert.ToInt16(Console.ReadLine());
                        if (f==1)
                        {
                            wrk.uygulamabilgisiolanilesil(search);
                            
                        }
                        else
                        {
                            
                            goto start ;
                        }

                    }
                kontrol.Close();
                connection.Close();
                Thread.Sleep(2000);
                Console.WriteLine("\n");
                goto start;
            }
            #endregion 
            
            #region Answer 4
            if (answer==4)
            {
                cmd.CommandText=@"select *from Uygulama";
                SqlDataReader readd=cmd.ExecuteReader();
                while (readd.Read())
                {
                   var uyid=readd.GetInt32(0);
                   var uyname=readd.GetString(1);
                   Console.WriteLine("Uygulama id " + uyid + " " + uyname);
                }
                readd.Close();
                Console.WriteLine("Uygulama id Giriniz");
                int uygid4=Convert.ToInt16(Console.ReadLine());
                cmd.CommandText=@"select * from UygulamaBilgileri where uygulamaid=@u1";
                cmd.Parameters.AddWithValue("@u1",uygid4);
                readd=cmd.ExecuteReader();
                int sayac=0;
                while (readd.Read())
                {
                    sayac++;
                }                     
                    if (sayac!=0)
                    {
                        
                        Console.WriteLine("Uygulama kullanıclar tarafından kullanılmakta silmek istediğinize eminmisiniz \n Evet=1 Hayır=0 ");
                        int x=Convert.ToInt16(Console.ReadLine());
                        if (x==1)
                        {
                            wrk.uygulamabilgisiolanuygulamayisil(uygid4);
                        }
                        else
                        {
                            goto start;
                        }
                    }
                    else
                    {
                        wrk.uygulamabilgisiolmayanuygulamasilme(uygid4);                        
                    }

                readd.Close();
                connection.Close();
                Thread.Sleep(2000);
                Console.WriteLine("\n");
                goto start;
            }
            #endregion 

            #region Answer 5
            if (answer==5)
            {
                cmd.CommandText=@"select * from Kullanici ";
                SqlDataReader reader=cmd.ExecuteReader();
                while (reader.Read())
                {
                    var kulid=reader.GetInt32(0);
                    var kulname=reader.GetString(1);
                    var kulsoyad=reader.GetString(2);
                    var kultel=reader.GetString(3);
                    Console.WriteLine("Kullanici id : "+kulid +" "+"Kullanici isim "+kulname+" "+kulsoyad+" "+"Telefon numarası "+kultel);
                }
                _uygulama Iuygulama=new _uygulama();
                reader.Close();

                Console.WriteLine("Kullanıcı id yi giriniz  ");
                Iuygulama.KullaniciId=Convert.ToInt16(Console.ReadLine());

                cmd.CommandText=@"select *from Uygulama";
                SqlDataReader readd=cmd.ExecuteReader();
                while (readd.Read())
                {
                   var uyid=readd.GetInt32(0);
                   var uyname=readd.GetString(1);
                   Console.WriteLine("Uygulama id " + uyid + " " + uyname);
                }
                readd.Close();

                Console.WriteLine("uygulama id yi giriniz ");
                Iuygulama.Uygulamaid=Convert.ToInt16(Console.ReadLine());

                Console.WriteLine("Geçen Süre ");
                Iuygulama.Gecensure=Convert.ToInt16(Console.ReadLine());

                wrk.kullaniciyauygulamabilgisiekleme(Iuygulama);  
                 
                connection.Close();

                Thread.Sleep(2000);

                Console.WriteLine("\n");
                
                goto start;

            }
            #endregion

            #region Answer 6
            if (answer==6)
            {
                connection.Close();
               System.Environment.Exit(0);

            }
            #endregion
            

            
        }

    }
}
