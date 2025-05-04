using Database;
using Database.Context;
using Business.FakeForm;
using Microsoft.AspNetCore.Identity;
namespace Business
{
    public class UserService
    {
        EventContext context = new EventContext();
        public Result Registration(MockForm form)
        {
            bool x = context.UserInfo.Any(u => u.Email == form.Email);
            if (x)
            {
                return new Result(false, "Email already exists", null);
            }
                UserInfo user = new UserInfo()
                {
                    UserName = form.UserName,
                    Email = form.Email,
                    PasswordHash = new PasswordHasher<object>().HashPassword(form, form.Password),
                    PhoneNumber = form.PhoneNum,
                    Role = form.Role == 0 ? 3 : form.Role,
                    CreatedBy = form.CreatedBy,
                    UpdatedDate =form.UpdatedDate,
                    UpdatedBy = form.UpdatedBy,
                };
                context.UserInfo.Add(user);
                return new Result().DBcommit(context, "Registration successful", null, user);
        }
        public Result Login(FakeLoginForm form)
        {
            UserInfo userInfo = context.UserInfo.FirstOrDefault(u => u.Email == form.Email);
            if (userInfo == null)
            {
                return new Result(true, "Email not found.Register First!", null);
            }
            PasswordVerificationResult HashResult = new PasswordHasher<UserInfo>().VerifyHashedPassword(userInfo, userInfo.PasswordHash, form.Password);
            if (HashResult != PasswordVerificationResult.Failed)
            {
                return new Result(true, $"Logged in successfully", userInfo);
            }
            else
            {
                return new Result(false, "Incorrect Password");
            }
        }
        public Result Update(UserInfo userInfo)
        {
            UserInfo user = context.UserInfo.FirstOrDefault(u => u.UserId == userInfo.UserId);
            if (user == null)
            {
                return new Result(false, "User not found", null);
            }
            user.UserName = userInfo.UserName;
            user.Email = userInfo.Email;
            user.PhoneNumber = userInfo.PhoneNumber;
            user.Role = userInfo.Role;
            user.UpdatedDate = DateTime.Now;
            return new Result().DBcommit(context, "User info updated successfully", null, user);
        }
        public Result List(int role)
        {
            try
            {
                var Users = context.UserInfo.Where(x=>x.Role == role).ToList();
                return new Result(true, "Success", Users);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public Result Single (string id)
        {
            UserInfo user = context.UserInfo.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return new Result(false, "User not found", null);
            }

            return new Result(true, "User found", user);
        }
    }


}
