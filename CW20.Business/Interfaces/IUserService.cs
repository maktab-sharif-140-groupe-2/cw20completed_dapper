using CW20.Domain;

namespace CW20.Services
{
    public interface IUserService
    {
        /// <summary>
        /// دریافت تمام کاربران 
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetAllUsersAsync();

        /// <summary>
        /// دریافت کاربر با ایدی 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User?> GetUserByIdAsync(int id);

        /// <summary>
        /// اضافه کردن کاربر به دیتابیس 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddUserAsync(User user);

        /// <summary>
        /// حذف کاربر از دیتابیس 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveUserAsync(int id);

        /// <summary>
        /// اپدیت کاربر 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task UpdateUserAsync(User user);

        /// <summary>
        /// کاربران بر اساس صفحه و تعدا در هر صفحه
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<User>> GetUsersPaged(int pageNumber, int pageSize);

    }
}
