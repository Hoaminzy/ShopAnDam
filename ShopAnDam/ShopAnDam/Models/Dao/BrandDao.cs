using PagedList;
using ShopAnDam.Models.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopAnDam.Models.Dao
{
    public class BrandDao
    {
        AnDamDBContext db = null;
        public BrandDao()
        {
            db = new AnDamDBContext();
        }
        string cs = ConfigurationManager.ConnectionStrings["AnDamDBContext"].ConnectionString;
        public List<Brand> ListAll()
        {
            List<Brand> lst = new List<Brand>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SelectBrand", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new Brand
                    {
                        ID = Convert.ToInt32(rdr["ID"]),
                        Name = rdr["Name"].ToString(),
                        Logo = rdr["Logo"].ToString(),
                      
                    });
                }
                return lst;
            }


        }
        public IEnumerable<Brand> ListAllPageList(string searchString, int page, int pageSize)
        {

            IQueryable<Brand> model = db.Brands;
            if (!string.IsNullOrEmpty(searchString))
            {//Contains: tìm kiếm gần đúng
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public int Add(Brand brands)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertBrand", con);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@Name", brands.Name);
                com.Parameters.AddWithValue("@Logo", brands.Logo);
                // com.Parameters.AddWithValue("@Action", "Insert");
                int rs = com.ExecuteNonQuery();
                return rs;
            }
        }
        public int Update(Brand brands)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("UpdateBrand", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ID", brands.ID);
                com.Parameters.AddWithValue("@Name", brands.Name);
                com.Parameters.AddWithValue("@Logo", brands.Logo);
            
                //   com.Parameters.AddWithValue("@Action", "Update");
                int i = com.ExecuteNonQuery();
                return i;

            }


        }

        //Method for Deleting an Employee
        public int Delete(int ID)
        {

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("DeleteBrand", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ID", ID);
                int i = com.ExecuteNonQuery();
                return i;
            }

        }

    }
}