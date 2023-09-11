using System;
using BookStoreUser.Dtos;
using BookStoreUser.Helpers;
using BookStoreUser.Models;

namespace BookStoreUser.Services
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(AppDbContext context, IWebHostEnvironment environment)
        {
            Context = context;
            HostingEnvironment = environment;
        }

        private AppDbContext Context { get; }
        private IWebHostEnvironment HostingEnvironment;

        public User AddUser(AddUserDto userDto)
        {
            User user = this.GetUserByEmail(userDto.Email);
            if (user is not null)
            {
                throw new AppException("Email already exists.", 400);
            }

            //Upload image
            string image = Misc.UploadFile(userDto.Image, HostingEnvironment);

            User user1 = new User
            {
                Email = userDto.Email,
                Balance = 0.00M,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Image = image,
                Password = userDto.Password,
                PhoneNumber = userDto.PhoneNumber,
                RegistrationUuid = Misc.GenerateReference()
            };
            Context.Users.Add(user1);
            Context.SaveChanges();
            return user1;
        }

        public User EditUser(EditUserDto userDto)
        {
            User user1 = GetUser(userDto.RegistrationUuid);
            user1.Balance = (decimal)(userDto.Balance > 0 ? userDto.Balance : user1.Balance);
            user1.Email = userDto.Email ?? user1.Email;
            user1.FirstName = userDto.FirstName ?? user1.FirstName;
            user1.LastName = userDto.LastName ?? user1.LastName;
            user1.Image = userDto.Image != null ? Misc.UploadFile(userDto.Image, HostingEnvironment) : user1.Image;
            //user1.Password = userDto.Password ?? user1.Password;
            user1.PhoneNumber = userDto.PhoneNumber ?? user1.PhoneNumber;

            Context.Users.Update(user1);
            Context.SaveChanges();
            return user1;
        }

        public User GetUser(string registrationUuid)
        {
            User? user = Context.Users.FirstOrDefault(opt => opt.RegistrationUuid == registrationUuid);
            if (user is null)
            {
                throw new AppException("User not exists", 404);
            }

            return user;
        }

        public User GetUserByEmail(string emailAddress)
        {
            User user = Context.Users.FirstOrDefault(opt => opt.Email == emailAddress)!;
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return Context.Users;
        }

        public User Login(LoginDto loginDto)
        {
            User user = GetUserByEmail(loginDto.Email);
            if(user is null)
            {
                throw new AppException("Invalid login details.", 400);
            }

            if(!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                throw new AppException("Invalid login details.", 400);
            }

            return user;
        }
    }
}

