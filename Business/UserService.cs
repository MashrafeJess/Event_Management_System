using Business.FakeForm;
using Database;
using Database.Context;
using Database.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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
                    IsActive = true,
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
        public Result Update(MockForm form)
        {
            UserInfo user = context.UserInfo.FirstOrDefault(u => u.Email == form.Email);
            if (user == null)
            {
                return new Result(false, "User not found", null);
            }

            // Update only if provided (not null or empty)
            if (!string.IsNullOrEmpty(form.UserName))
                user.UserName = form.UserName;

            if (!string.IsNullOrEmpty(form.Email))
                user.Email = form.Email;

            if (!string.IsNullOrEmpty(form.Password))
                user.PasswordHash = new PasswordHasher<object>().HashPassword(form, form.Password);

            if (!string.IsNullOrEmpty(form.PhoneNum))
                user.PhoneNumber = form.PhoneNum;

            user.Role = form.Role == 0 ? 3 : form.Role;

            user.UpdatedDate = DateTime.Now;
            user.UpdatedBy = form.UpdatedBy;

            return new Result().DBcommit(context, "User info updated successfully", null, user);
        }
        public Result ResetInfo(FakeLoginForm form)
        {
            UserInfo user = context.UserInfo.FirstOrDefault(u => u.Email == form.Email);
            if (user == null)
            {
                return new Result(false, "User not found", null);
            }
            return new Result(true, "Success", user.UserName);
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
        public Result NewSingle(string Id)
        {
            UserData user = context.UserData.FirstOrDefault(u => u.UserId == Id);
            if (user == null)
            {
                return new Result(false, "User not found", null);
            }

            return new Result(true, "User found", user);
        }
    }


}
