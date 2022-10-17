using Microsoft.EntityFrameworkCore;
using StartasLamstvk.API.Entities;
using System.ComponentModel.DataAnnotations;
using StartasLamstvk.Shared.Models.RaceOfficial;

namespace StartasLamstvk.API.Services
{
    public interface IRaceOfficialService
    {
        Task<int> CreateRaceOfficial(int eventId, RaceOfficialWriteModel model);
        Task<List<RaceOfficialReadModel>> GetRaceOfficials(int eventId);
        Task<bool> UpdateRaceOfficial(int eventId, int raceOfficialId, RaceOfficialWriteModel model);
        Task<bool> DeleteRaceOfficial(int eventId, int raceOfficialId);
    }

    public class RaceOfficialService : IRaceOfficialService
    {
        private readonly ApplicationDbContext _context;

        public RaceOfficialService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateRaceOfficial(int eventId, RaceOfficialWriteModel model)
        {
            var @event = await _context.Events.FirstOrDefaultAsync(x => x.Id == eventId);
            if (@event is null)
            {
                throw new ValidationException($"Event {eventId} doesn't exist");
            }

            var userExists = await _context.Users.AnyAsync(x => x.Id == model.UserId);
            if (!userExists)
            {
                throw new ValidationException($"User {model.UserId} doesn't exist");
            }

            var raceOfficial = new RaceOfficial
            {
                UserId = model.UserId, Title = model.Title, Location = model.Location, Date = model.Date,
                ArrivalTime = model.ArrivalTime
            };

            @event.RaceOfficials.Add(raceOfficial);
            await _context.SaveChangesAsync();
            return raceOfficial.Id;
        }

        public async Task<List<RaceOfficialReadModel>> GetRaceOfficials(int eventId)
        {
            var eventExists = await _context.Events.AnyAsync(x => x.Id == eventId);
            if (!eventExists)
            {
                throw new ValidationException($"Event {eventId} doesn't exist");
            }

            var raceOfficials = await _context.RaceOfficials
                .AsNoTracking()
                .Include(x => x.User)
                .Where(x => x.EventId == eventId)
                .Select(x => new RaceOfficialReadModel
                {
                    Id = x.Id,
                    User = new ()
                    {
                        FullName = $"{x.User.Name} {x.User.Surname}", Id = x.UserId, PhoneNumber = x.User.PhoneNumber
                    },
                    Title = x.Title,
                    Location = x.Location,
                    Date = x.Date,
                    ArrivalTime = x.ArrivalTime
                })
                .ToListAsync();

            return raceOfficials;
        }

        public async Task<bool> UpdateRaceOfficial(int eventId, int raceOfficialId, RaceOfficialWriteModel model)
        {
            var raceOfficial = await _context.RaceOfficials.FirstOrDefaultAsync(x => x.Id == raceOfficialId);
            if (raceOfficial is null)
            {
                return false;
            }

            var @event = await _context.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == eventId);
            if (@event is null)
            {
                throw new ValidationException($"Event {eventId} doesn't exist");
            }

            if (raceOfficial.EventId != eventId)
            {
                throw new ValidationException($"Race Official {raceOfficialId} doesn't work in event {eventId}");
            }

            raceOfficial.UserId = model.UserId;
            raceOfficial.Title = model.Title;
            raceOfficial.Location = model.Location;
            raceOfficial.Date = model.Date;
            raceOfficial.ArrivalTime = model.ArrivalTime;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRaceOfficial(int eventId, int raceOfficialId)
        {
            var raceOfficial = await _context.RaceOfficials.FirstOrDefaultAsync(x => x.Id == raceOfficialId);
            if (raceOfficial is null)
            {
                return false;
            }

            var @event = await _context.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == eventId);
            if (@event is null)
            {
                throw new ValidationException($"Event {eventId} doesn't exist");
            }

            if (raceOfficial.EventId != eventId)
            {
                throw new ValidationException($"Race Official {raceOfficialId} doesn't work in event {eventId}");
            }

            _context.RaceOfficials.Remove(raceOfficial);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}