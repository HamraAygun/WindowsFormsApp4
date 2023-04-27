using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public class UrunDal
    {
        public List<object> GetAll() 
        {
            using (AA_OdevEntities context=new AA_OdevEntities () )
            {
                var sonuc = from u in context.tblUrun
                            join k in context.tblKategori
                            on u.KategoriId equals k.Id
                            select new
                            {
                                Id=u.Id,
                                UrunAdi=u.UrunAdi,
                                Fiyat=u.Fiyat,  
                                Stok=u.Stok,
                                Kategori=k.KatagoriAdi
                            };
                return sonuc.ToList<object>();
            }
        } 
        public List<object> KategoriGetAll()
        {
            using (AA_OdevEntities context=new AA_OdevEntities())
            {
                var sonuc = from k in context.tblKategori
                            select new 
                            {
                            Id=k.Id,
                           KatagoriAdi = k.KatagoriAdi,
                            };
                return sonuc.ToList<object>();  
            }
        }
        public  int kategoriId(string KatagoriAdi)
        {
            using (AA_OdevEntities contex = new AA_OdevEntities())
            {
                var kategori = contex.tblKategori.FirstOrDefault(k => k.KatagoriAdi == KatagoriAdi);
                if (kategori !=null)
                {
                    return kategori.Id;
                }
                else
                {
                    return -1;  
                }
            }
        
        }
        public void Addurun(tblUrun tblUrun)
        {
            using (AA_OdevEntities context=new AA_OdevEntities())
            {
                var x=context.Entry(tblUrun);
                x.State=EntityState.Added;
                context.SaveChanges();

            }
        }


        


      



    }
}
