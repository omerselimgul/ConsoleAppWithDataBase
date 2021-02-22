using System;
using System.Data.SqlClient;

namespace appli
{
  public  class work
    {     

         public void KullaniciEkle(kullanici Ikullanici)
        {
            SqlConnection connection=new SqlConnection(@"Data Source=DESKTOP-0TTDRAK\SQLEXPRESS;Initial Catalog=users ;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            connection.Open();
            cmd.Connection=connection;
            cmd.CommandText=           
            @"
           insert into Kullanici (ad,soyad,cinsiyet,telno,yas,medenidurumu) values (@p1,@p2,@p3,@p4,@p5,@p6)
            ";
            cmd.Parameters.AddWithValue("@p1",Ikullanici.ad);
            cmd.Parameters.AddWithValue("@p2",Ikullanici.soyad);
            cmd.Parameters.AddWithValue("@p3",Ikullanici.Cinsiyet);
            cmd.Parameters.AddWithValue("@p4",Ikullanici.kullaniciTel);
            cmd.Parameters.AddWithValue("@p5",Ikullanici.yas);
            cmd.Parameters.AddWithValue("@p6",Ikullanici.Medenidurumu);
            cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Kullanıcı Eklendi");

        }
        public void UygulamaBilgisiileEkle(kullanici Ikullanici,_uygulama _Uygulama)
        {
            SqlConnection connection=new SqlConnection(@"Data Source=DESKTOP-0TTDRAK\SQLEXPRESS;Initial Catalog=users ;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            connection.Open();
            cmd.Connection=connection;
            cmd.CommandText= @"
           insert into Kullanici (ad,soyad,cinsiyet,telno,yas,medenidurumu) values (@p1,@p2,@p3,@p4,@p5,@p6)
            "  ; 
             cmd.Parameters.AddWithValue("@p1",Ikullanici.ad);
            cmd.Parameters.AddWithValue("@p2",Ikullanici.soyad);
            cmd.Parameters.AddWithValue("@p3",Ikullanici.Cinsiyet);
            cmd.Parameters.AddWithValue("@p4",Ikullanici.kullaniciTel);
            cmd.Parameters.AddWithValue("@p5",Ikullanici.yas);
            cmd.Parameters.AddWithValue("@p6",Ikullanici.Medenidurumu);
            cmd.ExecuteNonQuery();



            cmd.CommandText=@"declare @son int
                            set @son =(select top 1 kullaniciid from Kullanici order by kullaniciid desc)
                            insert into UygulamaBilgileri(uygulamaid,gecensure,kullaniciid) values(@i1,@i2,@son)

                            update UygulamaBilgileri set uygulamadi=(select uygulamaadi from Uygulama where uygulamaid=@i1)
                            where kullaniciid=@son ";

            cmd.Parameters.AddWithValue("@i1",_Uygulama.Uygulamaid);
            cmd.Parameters.AddWithValue("@i2",_Uygulama.Gecensure);
            cmd.ExecuteNonQuery();
            
            connection.Close();

            Console.WriteLine("Kullanıcı Eklendi");

        }
        public void uygulamabilgisiolmadansil(int kullaniciid)
        {
           SqlConnection connection=new SqlConnection(@"Data Source=DESKTOP-0TTDRAK\SQLEXPRESS;Initial Catalog=users ;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            connection.Open();
            cmd.Connection=connection;
            cmd.CommandText= @"delete  from Kullanici where kullaniciid=@k1";
            cmd.Parameters.AddWithValue("@k1",kullaniciid);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Kullnıcı Silindi ");
            connection.Close();
        }
        public void uygulamabilgisiolanilesil(int kullaniciid)
        {
          
           SqlConnection connection=new SqlConnection(@"Data Source=DESKTOP-0TTDRAK\SQLEXPRESS;Initial Catalog=users ;Integrated Security=True");
          SqlTransaction sqlTransaction=null;
          try
          {
            SqlCommand cmd = new SqlCommand();
            connection.Open();
            sqlTransaction=connection.BeginTransaction();
            cmd.Connection=connection;
            cmd.CommandText=@"delete  from UygulamaBilgileri where kullaniciid=@k1 ";
            cmd.Parameters.AddWithValue("@k1",kullaniciid);
            cmd.ExecuteNonQuery();
            cmd=new SqlCommand();
            cmd.Connection=connection;
            cmd.CommandText= @"delete  from Kullanici where kullaniciid=@k2";
            cmd.Parameters.AddWithValue("@k2",kullaniciid);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Kullnıcı Silindi ");
            sqlTransaction.Commit();
            connection.Close(); 
          }
          catch (System.Exception)
          {
              if (sqlTransaction!=null)
              {
                  sqlTransaction.Rollback();                
              }
              if (connection!=null)
              {
                  connection.Close();
              }
          }

        }
        public void uygulamabilgisiolmayanuygulamasilme(int _uygulamaid)
        {
          SqlConnection connection=new SqlConnection(@"Data Source=DESKTOP-0TTDRAK\SQLEXPRESS;Initial Catalog=users ;Integrated Security=True");
          SqlCommand cmd = new SqlCommand();
          connection.Open();
          cmd.Connection=connection;
          cmd.CommandText=@"delete from Uygulama where Uygulamaid= @T1";
          cmd.Parameters.AddWithValue("@T1",_uygulamaid);
          Console.WriteLine("Uygulama Silindi");
          connection.Close();
        }        
        public void uygulamabilgisiolanuygulamayisil(int _uygulamaid)
        {
          SqlConnection connection=new SqlConnection(@"Data Source=DESKTOP-0TTDRAK\SQLEXPRESS;Initial Catalog=users ;Integrated Security=True");
          SqlCommand cmd = new SqlCommand();
          connection.Open();
          cmd.Connection=connection;
          cmd.CommandText=@"update UygulamaBilgileri set uygulamaid=null ,uygulamadi=null where uygulamaid=@R1";
          cmd.Parameters.AddWithValue("@R1",_uygulamaid);
          cmd.ExecuteNonQuery();
          cmd.CommandText=@"delete from Uygulama where uygulamaid=@R2";
          cmd.Parameters.AddWithValue("@R2",_uygulamaid);
          cmd.ExecuteNonQuery();
          Console.WriteLine("Uygulama Silindi");
          connection.Close();

        }
        public void kullaniciyauygulamabilgisiekleme(_uygulama uygulama)
        {
          SqlConnection connection=new SqlConnection(@"Data Source=DESKTOP-0TTDRAK\SQLEXPRESS;Initial Catalog=users ;Integrated Security=True");
          SqlCommand cmd = new SqlCommand();
          connection.Open();
          cmd.Connection=connection;
          cmd.CommandText=@"insert into UygulamaBilgileri (kullaniciid,uygulamaid,gecensure) values (@E1,@E2,@E3)";
          cmd.Parameters.AddWithValue("@E1",uygulama.KullaniciId);
          cmd.Parameters.AddWithValue("@E2",uygulama.Uygulamaid);
          cmd.Parameters.AddWithValue("@E3",uygulama.Gecensure);
          cmd.ExecuteNonQuery();
          cmd.CommandText=@"update UygulamaBilgileri set uygulamadi=(select uygulamaadi 
                            from Uygulama where UygulamaBilgileri.uygulamaid=Uygulama.Uygulamaid )";           
          cmd.ExecuteNonQuery();
          connection.Close();
          Console.WriteLine("Kullanıcı uygulama bilgileri eklendi ");
        }

        
    }
}
