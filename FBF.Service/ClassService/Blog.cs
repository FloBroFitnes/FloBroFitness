using FBF.Service.DBService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBF.Service.ClassService
{
    public class Blog
    {
        public static bool Add(tblBlog blog)
        {
            try
            {
                using (FloBroFitnessEntities context = new FloBroFitnessEntities())
                {
                    context.tblBlogs.Add(blog);
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool Update(tblBlog blog)
        {
            try
            {
                using (FloBroFitnessEntities context = new FloBroFitnessEntities())
                {
                    tblBlog oldBlog = context.tblBlogs.FirstOrDefault(i => i.ID == blog.ID);
                    if (oldBlog != null)
                    {
                        oldBlog.Discription = blog.Discription;
                        oldBlog.Title = blog.Title;
                        oldBlog.IsActive = blog.IsActive;
                        oldBlog.Tags = blog.Tags;
                        oldBlog.Date = blog.Date;
                    }
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public static bool Delete(long id)
        {
            try
            {
                using (FloBroFitnessEntities context = new FloBroFitnessEntities())
                {
                    tblBlog oldBlog = context.tblBlogs.FirstOrDefault(i => i.ID == id);
                    if (oldBlog != null)
                    {
                        context.tblBlogs.Remove(oldBlog);
                    }
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public static tblBlog GetByID(long id)
        {
            try
            {
                using (FloBroFitnessEntities context = new FloBroFitnessEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.tblBlogs.Include("tblUser").FirstOrDefault(i => i.ID == id);
                   
                  //  return oldBlog;
                }
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public static List<tblBlog> GetAllBlogs()
        {
            FloBroFitnessEntities context = new FloBroFitnessEntities();
                  return context.tblBlogs.ToList();
        }

        public static List<tblBlog> GetAllActiveBlogs()
        {
            FloBroFitnessEntities context = new FloBroFitnessEntities();
            return context.tblBlogs.Where(i=>i.IsActive==true).ToList();
        }

        public static bool AddComment(tblBlogComment comment)
        {
            try
            {
                using (FloBroFitnessEntities context = new FloBroFitnessEntities())
                {
                    context.tblBlogComments.Add(comment);
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<tblBlogComment> GetCommentsByBlogID(long blogID)
        {
            FloBroFitnessEntities context = new FloBroFitnessEntities();
            context.Configuration.LazyLoadingEnabled = false;
            return context.tblBlogComments.Include("tblUser").Where(i => i.BlogID == blogID).ToList();
        }
    }
}
