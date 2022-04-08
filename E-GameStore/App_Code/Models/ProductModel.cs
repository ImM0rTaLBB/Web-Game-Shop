using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductModel
/// </summary>
public class ProductModel
{
    public string InsertProduct(Product product)
    {
        try
        {
            GameDBEntities1 db = new GameDBEntities1();
            db.Products.Add(product);
            db.SaveChanges();

            return product.Name + " was successfully inserted";

        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }

    public string UpdateProduct(int id, Product product)
    {
        try
        {
            GameDBEntities1 db = new GameDBEntities1();

            Product p = db.Products.Find(id);

            p.Name = product.Name;
            p.Price = product.Price;
            p.TypeID = product.TypeID;
            p.Description = product.Description;
            p.Image = product.Image;

            db.SaveChanges();
            return product.Name + " was succesfully updated";
        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }

    public string DeleteProduct(int id)
    {
        try
        {
            GameDBEntities1 db = new GameDBEntities1();
            Product product = db.Products.Find(id);

            db.Products.Attach(product);
            db.Products.Remove(product);
            db.SaveChanges();

            return product.Name + " was successfully deleted";
        }
        catch (Exception e)
        {
            return "Error:" + e;
        }
    }

    public Product GetProduct(int id)
    {
        try
        {
            using (GameDBEntities1 db = new GameDBEntities1())
            {
                Product product = db.Products.Find(id);
                return product;
            }
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public List<Product>GetAllProducts()
    {
        try
        {
            using (GameDBEntities1 db = new GameDBEntities1())
            {
                List<Product> products = (from x in db.Products
                                          select x).ToList();

                return products;
            }
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public List<Product> GetProductsByType(int typeid)
    {
        try
        {
            using (GameDBEntities1 db = new GameDBEntities1())
            {
                List<Product> products = (from x in db.Products
                                          where x.TypeID == typeid
                                          select x).ToList();

                return products;
            }
        }
        catch (Exception e)
        {
            return null;
        }
    }
}