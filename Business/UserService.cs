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
            else
            {
                UserInfo user = new UserInfo()
                {
                    UserName = form.UserName,
                    Email = form.Email,
                    PasswordHash = new PasswordHasher<object>().HashPassword(form, form.Password),
                    PhoneNumber = form.PhoneNum,
                    CreatedBy = form.CreatedBy,
                    UpdatedDate =form.UpdatedDate,
                    UpdatedBy = form.UpdatedBy,
                };
                context.UserInfo.Add(user);
                return Result.DBcommit(context, "Registration successful", null, user);
            }
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
                return new Result(true, $"Logged in successfully", form);
            }
            else
            {
                return new Result(false, "Incorrect Password");
            }
        }
    }


}
