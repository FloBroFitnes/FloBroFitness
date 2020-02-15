using FBF.Service.DBService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBF.Service.ClassService
{
    public class Products
    {
        public static bool addupdateproduct(long ID, string name, string des, long categoryID, string inventry, string size, int price)
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {
                bool result = false;
                if (ID == 0)
                {
                    var check = context.tblProducts.Where(x => x.Name == name && x.CategoryId == categoryID).FirstOrDefault();
                    if (check != null)
                    {
                        result = false;
                    }
                    else
                    {
                        tblProduct product = new tblProduct();
                        product.Name = name;
                       // product.Inventry = inventry;
                        product.Price = price;
                        product.Size = size;
                        product.Description = des;
                        product.CategoryId = categoryID;
                        context.tblProducts.Add(product);
                        context.SaveChanges();
                        result = true;
                    }
                }
                else
                {
                    var check = context.tblProducts.Find(ID);
                    if (check != null)
                    {
                        var namecheck = context.tblProducts.Where(x => x.Name == name && x.ID != ID && x.CategoryId == categoryID).FirstOrDefault();
                        if (namecheck != null)
                        {
                            result = false;
                        }
                        else
                        {
                            check.Name = name;
                          //  check.Inventry = inventry;
                            check.Price = price;
                            check.Size = size;
                            check.Description = des;
                            check.CategoryId = categoryID;
                            context.SaveChanges();
                            result = true;
                        }
                    }
                }
                return result;
            }
        }

        public static bool deleteproduct(long ID)
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {
                var del = context.tblProducts.Find(ID);
                context.tblProducts.Remove(del);
                return context.SaveChanges() > 0;
            }
        }
        public static bool AddProduct(tblProduct product)
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {
                context.tblProducts.Add(product);
                return context.SaveChanges() > 0;
            }
        }
    }
}
