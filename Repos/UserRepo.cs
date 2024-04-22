using Examination_System.Controllers;
using Examination_System.Data;
using Examination_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Security.Claims;

namespace Examination_System.Repos
{
    public interface IUserRepo
    {
        public Task<bool> IsUserCredentialsValid(string username, string password); //check if the user credentials are valid
		User GetUser(string Name, string Password);
        public int GetUserId(ClaimsPrincipal user);


    }

    public class UserRepo : IUserRepo
    {
        readonly ExaminationSystemContext db; // database context

        public UserRepo(ExaminationSystemContext _db) //constructor
        {
            db = _db;
        }

        public async Task<bool> IsUserCredentialsValid(string username, string password)
        {
            try
            {
                var parameters = new SqlParameter[] //parameters for the stored procedure
                {
                    new SqlParameter("@userName", username),
                    new SqlParameter("@password", password),
                    new SqlParameter
                    {
                        ParameterName= "@IsValid",
                        SqlDbType = System.Data.SqlDbType.Bit,
                        Direction = ParameterDirection.Output
                    }
                };

                await db.Database.ExecuteSqlRawAsync("EXECUTE IsUserCredentialsValid @userName, @password, @IsValid OUTPUT", parameters); //execute the stored procedure

                return Convert.ToBoolean(parameters[2].Value); //return the output parameter
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

		public User GetUser(string Name, string Password)
		{
			return db.Users.FirstOrDefault(u => u.UserName == Name && u.UserPass == Password);
		}

        public int GetUserId(ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return Convert.ToInt32(userIdClaim.Value);
        }
	}
}
