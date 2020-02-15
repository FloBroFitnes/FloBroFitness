using FBF.Service.DBService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBF.Service.ClassService
{
    public class Categories
    {
        public static bool addupdatecategory(long ID, string name)
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {
                bool result = false;
                if (ID == 0)
                {
                    var check = context.tblCategories.Where(x => x.Name == name).FirstOrDefault();
                    if (check != null)
                    {
                        result = false;
                    }
                    else
                    {
                        tblCategory tblCategory = new tblCategory();
                        tblCategory.Name = name;
                        context.tblCategories.Add(tblCategory);
                        context.SaveChanges();
                        result = true;
                    }
                }
                else
                {
                    var check = context.tblCategories.Find(ID);
                    if (check != null)
                    {
                        var namecheck = context.tblCategories.Where(x => x.Name == name && x.ID != ID).FirstOrDefault();
                        if (namecheck != null)
                        {
                            result = false;
                        }
                        else
                        {
                            check.Name = name;
                            context.SaveChanges();
                            result = true;
                        }
                    }
                }
                return result;
            }
        }

        public static bool deletecategory(long ID)
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {
                var del = context.tblCategories.Find(ID);
                context.tblCategories.Remove(del);
                return context.SaveChanges() > 0;
            }
        }

        public static List<tblCategory> GetCategoryList()
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {
                return context.tblCategories.ToList();
            }
        }
        public static bool AddUpdate(tblCategory category)
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {
                if (category.ID > 0)
                {
                    var old = context.tblCategories.Where(i => i.ID == category.ID).FirstOrDefault();
                    if (old != null)
                    {
                        old.Name = category.Name;
                    }
                }
                else
                {
                    context.tblCategories.Add(category);
                }
               return context.SaveChanges() > 0;
            }
        }
    }
}
