using Microsoft.EntityFrameworkCore;
using StartasLamstvk.API.Entities;
using StartasLamstvk.Shared;
using StartasLamstvk.Shared.Models.Category;
using StartasLamstvk.Shared.Models.Enum;
using StartasLamstvk.Shared.Models.User;
using System.ComponentModel.DataAnnotations;

namespace StartasLamstvk.API.Services
{
    public interface IUserService
    {
        Task<int> CreateUser(UserWriteModel model);
        Task<UserBaseModel> GetUser(int userId);
        Task<List<UserBaseModel>> GetUsers(EnumRole? roleId, List<int> userIds);
        Task<bool> UpdateUser(int id, UserWriteModel model);
        Task<bool> UpdateMotoCategory(int userId, EnumMotoCategory categoryId);
        Task<bool> UpdateLasfCategory(int userId, EnumLasfCategory categoryId);
        Task<bool> DeleteUser(int userId);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateUser(UserWriteModel model)
        {
            if (model.LasfCategoryId.HasValue && !Enum.IsDefined(model.LasfCategoryId.Value))
            {
                throw new ValidationException($"Lasf Category {model.LasfCategoryId} not found");
            }

            if (model.MotoCategoryId.HasValue && !Enum.IsDefined(model.MotoCategoryId.Value))
            {
                throw new ValidationException($"Moto Category {model.MotoCategoryId} not found");
            }

            var user = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                BirthDate = model.BirthDate,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Location = model.Location,
                LasfCategoryId = model.LasfCategoryId,
                MotoCategoryId = model.MotoCategoryId
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<UserBaseModel> GetUser(int userId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Include(x => x.LasfCategory).ThenInclude(x =>
                    x.LasfCategoryTranslations.Where(t => t.LanguageCode == Languages.Lt))
                .Include(x => x.MotoCategory).ThenInclude(x =>
                    x.MotoCategoryTranslations.Where(t => t.LanguageCode == Languages.Lt))
                .Include(x => x.Role).ThenInclude(x => x.RoleTranslations.Where(t => t.LanguageCode == Languages.Lt))
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                return null;
            }

            var model = new UserReadModel
            {
                Id = user.Id,
                FullName = $"{user.Name} {user.Surname}",
                BirthDate = user.BirthDate,
                Location = user.Location,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                LasfCategory = user.LasfCategoryId.HasValue
                    ? new LasfCategoryReadModel
                    {
                        Id = user.LasfCategoryId.Value,
                        Title = user.LasfCategory.LasfCategoryTranslations.Select(i => i.Text).FirstOrDefault()
                    }
                    : null,
                MotoCategory = user.MotoCategoryId.HasValue
                    ? new MotoCategoryReadModel
                    {
                        Id = user.MotoCategoryId.Value,
                        Title = user.MotoCategory.MotoCategoryTranslations.Select(i => i.Text).FirstOrDefault()
                    }
                    : null,
                Role = new()
                    { Id = user.RoleId, Name = user.Role.RoleTranslations.Select(x => x.Text).FirstOrDefault() }
            };

            return model;
        }

        public async Task<List<UserBaseModel>> GetUsers(EnumRole? roleId, List<int> userIds)
        {
            var query = _context.Users
                .AsNoTracking()
                .Include(x => x.LasfCategory).ThenInclude(x =>
                    x.LasfCategoryTranslations.Where(t => t.LanguageCode == Languages.Lt))
                .Include(x => x.MotoCategory).ThenInclude(x =>
                    x.MotoCategoryTranslations.Where(t => t.LanguageCode == Languages.Lt))
                .Include(x => x.Role).ThenInclude(x => x.RoleTranslations.Where(t => t.LanguageCode == Languages.Lt))
                .AsQueryable();
            if (roleId.HasValue)
            {
                query = query.Where(x => x.RoleId == roleId.Value);
            }

            if (userIds.Any())
            {
                query = query.Where(x => userIds.Contains(x.Id));
            }

            var users = await query
                .Select(x => new UserReadModel
                {
                    Id = x.Id,
                    FullName = $"{x.Name} {x.Surname}",
                    BirthDate = x.BirthDate,
                    Location = x.Location,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    LasfCategory = x.LasfCategoryId.HasValue
                        ? new LasfCategoryReadModel
                        {
                            Id = x.LasfCategoryId.Value,
                            Title = x.LasfCategory.LasfCategoryTranslations.Select(i => i.Text).FirstOrDefault()
                        }
                        : null,
                    MotoCategory = x.MotoCategoryId.HasValue
                        ? new MotoCategoryReadModel
                        {
                            Id = x.MotoCategoryId.Value,
                            Title = x.MotoCategory.MotoCategoryTranslations.Select(i => i.Text).FirstOrDefault()
                        }
                        : null,
                    Role = new() { Id = x.RoleId, Name = x.Role.RoleTranslations.Select(x => x.Text).FirstOrDefault() }
                })
                .Cast<UserBaseModel>()
                .ToListAsync();

            return users;
        }

        public async Task<bool> UpdateUser(int userId, UserWriteModel model)
        {
            if (model.LasfCategoryId.HasValue && !Enum.IsDefined(model.LasfCategoryId.Value))
            {
                throw new ValidationException($"Lasf Category {model.LasfCategoryId} not found");
            }

            if (model.MotoCategoryId.HasValue && !Enum.IsDefined(model.MotoCategoryId.Value))
            {
                throw new ValidationException($"Moto Category {model.MotoCategoryId} not found");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                return false;
            }

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.BirthDate = model.BirthDate;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Location = model.Location;
            user.LasfCategoryId = model.LasfCategoryId;
            user.MotoCategoryId = model.MotoCategoryId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMotoCategory(int userId, EnumMotoCategory categoryId)
        {
            if (!Enum.IsDefined(categoryId))
            {
                throw new ValidationException($"Moto Category {categoryId} not found");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                return false;
            }

            user.MotoCategoryId = categoryId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateLasfCategory(int userId, EnumLasfCategory categoryId)
        {
            if (!Enum.IsDefined(categoryId))
            {
                throw new ValidationException($"Lasf Category {categoryId} not found");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                return false;
            }

            user.LasfCategoryId = categoryId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}