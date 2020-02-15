using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FBF.Service.DBService;

namespace FBF.Service.ClassService
{
   public class Accounts
    {
        public static bool Register(tblUser user)
        {
            try
            {
                using (FloBroFitnessEntities context = new FloBroFitnessEntities())
                {
                    context.tblUsers.Add(user);
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
           
        }
        public static tblUser Login(string emailOrUserId,string password)
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {
                return context.tblUsers.Where(i => (i.Email == emailOrUserId || i.UserID == emailOrUserId) && i.Passworrd == password).FirstOrDefault();
            }
        }
        public static bool EmailExist(string email)
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {
                var record = context.tblUsers.Where(i => i.Email.ToLower().Contains(email)).FirstOrDefault();
                return record == null ? true : false;
            }
        }
        public static bool AccountVerification(Int64 Id,int StatusId)
        {
            using (FloBroFitnessEntities context = new FloBroFitnessEntities())
            {
                var record = context.tblUsers.Where(i => i.ID == Id).FirstOrDefault();
                if (record != null)
                {
                    record.StatusID = StatusId;
                }
                return context.SaveChanges() > 0;
            }
        }
        public static List<tblUser> GetAthleteList()
        {
            FloBroFitnessEntities context = new FloBroFitnessEntities();
            return context.tblUsers.Where(i => i.IsAthlete == true).ToList();
        }
        public static List<tblUser> GetAdminUserList()
        {
            FloBroFitnessEntities context = new FloBroFitnessEntities();
            return context.tblUsers.Where(i => i.IsAdmin == true).ToList();
        }
    }
}
