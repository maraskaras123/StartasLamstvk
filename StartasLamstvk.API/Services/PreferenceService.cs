using Microsoft.EntityFrameworkCore;
using StartasLamstvk.API.Entities;
using StartasLamstvk.Shared.Models.Enum;
using System.ComponentModel.DataAnnotations;
using StartasLamstvk.Shared;
using StartasLamstvk.Shared.Models.RacePreference;

namespace StartasLamstvk.API.Services
{
    public interface IPreferenceService
    {
        Task<int> CreateRacePreference(int userId, RacePreferenceWriteModel model);
        Task<int> CreatePreference(int userId, EnumRaceType raceTypeId);
        Task<List<RacePreferenceReadModel>> GetRacePreferences(int userId);
        Task<List<PreferenceReadModel>> GetPreferences(int userId);
        Task<bool> DeleteRacePreference(int preferenceId);
        Task<bool> DeletePreference(int preferenceId);
    }

    public class PreferenceService : IPreferenceService
    {
        private readonly ApplicationDbContext _context;

        public PreferenceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateRacePreference(int userId, RacePreferenceWriteModel model)
        {
            var date = model.Date.Date;
            var @event = await _context.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.EventId);
            if (@event is null)
            {
                throw new ValidationException($"Event {model.EventId} doesn't exist");
            }

            if (!(date >= @event.DateFrom && date <= (@event.DateTo ?? @event.DateFrom)))
            {
                throw new ValidationException($"Date {date} is invalid for Event {model.EventId}");
            }

            var userExists = await _context.Users.AnyAsync(x => x.Id == userId);
            if (!userExists)
            {
                throw new ValidationException($"User {userId} doesn't exist");
            }

            var preference = new UserRacePreference { UserId = userId, EventId = model.EventId, Date = date };
            await _context.UserRacePreferences.AddAsync(preference);
            await _context.SaveChangesAsync();
            return preference.Id;
        }

        public async Task<int> CreatePreference(int userId, EnumRaceType raceTypeId)
        {
            if (!Enum.IsDefined(raceTypeId))
            {
                throw new ValidationException($"RaceType {raceTypeId} doesn't exist");
            }

            var userExists = await _context.Users.AnyAsync(x => x.Id == userId);
            if (!userExists)
            {
                throw new ValidationException($"User {userId} doesn't exist");
            }

            var preference = new UserPreference
                { UserId = userId, RaceTypeId = raceTypeId, Year = (short)DateTime.Now.Year };
            await _context.UserPreferences.AddAsync(preference);
            await _context.SaveChangesAsync();
            return preference.Id;
        }

        public async Task<List<RacePreferenceReadModel>> GetRacePreferences(int userId)
        {
            var userExists = await _context.Users.AnyAsync(x => x.Id == userId);
            if (!userExists)
            {
                throw new ValidationException($"User {userId} doesn't exist");
            }

            var preferences = await _context.UserRacePreferences
                .AsNoTracking()
                .Include(x => x.Event)
                .Where(x => x.UserId == userId)
                .Select(x => new RacePreferenceReadModel{ EventId = x.EventId, Title = x.Event.Title })
                .ToListAsync();

            return preferences;
        }

        public async Task<List<PreferenceReadModel>> GetPreferences(int userId)
        {
            var userExists = await _context.Users.AnyAsync(x => x.Id == userId);
            if (!userExists)
            {
                throw new ValidationException($"User {userId} doesn't exist");
            }

            var preferences = await _context.UserPreferences
                .AsNoTracking()
                .Include(x => x.RaceType)
                .ThenInclude(x => x.RaceTypeTranslations.Where(t => t.LanguageCode == Languages.Lt))
                .Where(x => x.UserId == userId)
                .Select(x => new PreferenceReadModel
                {
                    RaceTypeId = x.RaceTypeId,
                    Title = x.RaceType.RaceTypeTranslations.Select(t => t.Text).FirstOrDefault()
                })
                .ToListAsync();

            return preferences;
        }

        public async Task<bool> DeleteRacePreference(int preferenceId)
        {
            var preference = await _context.UserRacePreferences.FirstOrDefaultAsync(x => x.Id == preferenceId);
            if (preference is null)
            {
                return false;
            }

            _context.UserRacePreferences.Remove(preference);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePreference(int preferenceId)
        {
            var preference = await _context.UserPreferences.FirstOrDefaultAsync(x => x.Id == preferenceId);
            if (preference is null)
            {
                return false;
            }

            _context.UserPreferences.Remove(preference);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}