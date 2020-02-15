using FBF.Service.DBService;
using FBF.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FBF.Service.ClassService
{
    public class Forums
    {
        public static bool AddUpdate(Forum forum)
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {
                if (forum.ForumId > 0)
                {
                    var old = context.Fora.Where(i => i.ForumId == forum.ForumId).FirstOrDefault();
                    if (old != null)
                    {
                        old.Name = forum.Body;
                        old.Body = forum.Body;
                        old.IsActive = forum.IsActive;
                    }
                }
                else
                {
                    forum.IsActive = true;
                    forum.UserView = 0;
                    forum.Activity = DateTime.Now.ToString() ;
                    forum.CreatedOn = DateTime.Now;
                    context.Fora.Add(forum);
                }
                return context.SaveChanges() > 0;
            }
        }
        public static bool AddUpdateFormType(ForumType forum)
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {
                if (forum.TypeId > 0)
                {
                    var old = context.ForumTypes.Where(i => i.TypeId == forum.TypeId).FirstOrDefault();
                    if (old != null)
                    {
                        old.Name = forum.Name;
                    }
                }
                else
                {
                    context.ForumTypes.Add(forum);
                }
                return context.SaveChanges() > 0;
            }
        }
        public static bool AddUpdateComment(Comment comment)
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {
                if (comment.CommentId> 0)
                {
                    var old = context.Comments.Where(i => i.CommentId == comment.CommentId).FirstOrDefault();
                    if (old != null)
                    {
                        old.Name = comment.Name;
                        old.UserId = comment.UserId;
                        old.IsActive = comment.IsActive;
                    }
                }
                else
                {
                    comment.IsActive = true;
                    comment.Createon = DateTime.Now;
                    comment.Name = comment.Name;
                    comment.UserId = comment.UserId;
                    context.Comments.Add(comment);
                }
                return context.SaveChanges() > 0;
            }
        }

        public static List<Forum> GetForumList()
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {
                return context.Fora.Where(c => c.IsActive == true).OrderBy(c => c.ForumId).ToList();
            }
        }
        public static List<Forum> GetForumListDetail()
        {
            FloBroFitnessEntities context = new FloBroFitnessEntities();
            
                var res = context.Fora.Include(c => c.ForumType).Include(c=>c.tblUser)
                    .Include(c => c.Comments).ToList();
                return res;
            
        }
        public static List<ForumType> GetForumTypeList()
        {
            FloBroFitnessEntities context = new FloBroFitnessEntities();
                return context.ForumTypes.Include(c=>c.Fora).OrderBy(c => c.TypeId).ToList();
        }

        public static bool UpdateView(long forumId)
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {
                if (forumId > 0)
                {
                    var old = context.Fora.Where(i => i.ForumId == forumId).FirstOrDefault();
                    if (old != null)
                    {
                        old.UserView = old.UserView + 1;
                        old.Activity = DateTime.Now.ToString();
                    }
                }
               
                return context.SaveChanges() > 0;
            }
        }

        
        public static Forum GetForumDetail(long id)
        {
            FloBroFitnessEntities context = new FloBroFitnessEntities();
           
                return context.Fora.Where(c => c.ForumId== id).FirstOrDefault();
            
        }
        public static List<Comment> GetForumComments(long id)
        {
            FloBroFitnessEntities context = new FloBroFitnessEntities();
            
                return context.Comments.Where(c => c.ForumId == id).Include(c=>c.tblUser).ToList();
            
        }
        public static bool DeleteComment(long id)
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {

                Comment comments = (from c in context.Comments
                                    where c.CommentId ==id
                                    select c
                                     ).SingleOrDefault();
                context.Comments.Remove(comments);
                bool value= context.SaveChanges() > 0;
                return value;
            }
        }
        public static long MaxCommentId()
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {

                return context.Comments.Max(c=>c.CommentId);
            }
        }
    }
}
