using CW20.Domain;

namespace CW20.Business.Interfaces;

public interface IUserDapperService
{
    /// <summary>
    /// دریافت کاربران از دیتابیس
    /// </summary>
    /// <returns></returns>
    Task<List<User>> GetAllUsers();

    /// <summary>
    /// دریافت کاربر با ایدی
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<User?> GetUserById(int id);

    /// <summary>
    /// اضافه کردن کاربر به دیتابیس
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<int> AddUser(User user);

    /// <summary>
    /// اپدیت کاربر
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<int> UpdateUser(User user);

    /// <summary>
    /// حذف کاربر
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<int> DeleteUser(int id);

    /// <summary>
    /// پیجینگ کاربر
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<List<User>> GetUsersPaged(int pageNumber, int pageSize);
}
