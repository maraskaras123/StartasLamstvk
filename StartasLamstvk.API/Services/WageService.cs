using Microsoft.EntityFrameworkCore;
using StartasLamstvk.API.Entities;
using StartasLamstvk.Shared.Models.Wage;
using System.ComponentModel.DataAnnotations;

namespace StartasLamstvk.API.Services
{
    public interface IWageService
    {
        Task<int> CreateWage(int eventId, int raceOfficialId, WageWriteModel model);
        Task<List<WageReadModel>> GetWages(int eventId, int raceOfficialId);
        Task<bool> UpdateWage(int eventId, int raceOfficialId, int wageId, WageWriteModel model);
        Task<bool> ConfirmTransaction(int eventId, int raceOfficialId, int wageId);
        Task<bool> DeleteWage(int eventId, int raceOfficialId, int wageId);
    }

    public class WageService : IWageService
    {
        private readonly ApplicationDbContext _context;

        public WageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateWage(int eventId, int raceOfficialId, WageWriteModel model)
        {
            var @event = await _context.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == eventId);
            if (@event is null)
            {
                throw new ValidationException($"Event {eventId} doesn't exist");
            }

            var raceOfficial = await _context.RaceOfficials.Include(x => x.Wages).FirstOrDefaultAsync(x => x.Id == raceOfficialId);
            if (raceOfficial is null)
            {
                throw new ValidationException($"Race Official {raceOfficialId} doesn't exist");
            }

            if (raceOfficial.EventId != eventId)
            {
                throw new ValidationException($"Race Official {raceOfficialId} doesn't work in event {eventId}");
            }

            var wage = new Wage
            {
                RaceOfficialId = raceOfficialId,
                Amount = model.Amount,
                Note = model.Note
            };

            raceOfficial.Wages.Add(wage);
            await _context.SaveChangesAsync();
            return wage.Id;
        }

        public async Task<List<WageReadModel>> GetWages(int eventId, int raceOfficialId)
        {
            var @event = await _context.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == eventId);
            if (@event is null)
            {
                throw new ValidationException($"Event {eventId} doesn't exist");
            }

            var raceOfficial = await _context.RaceOfficials.FirstOrDefaultAsync(x => x.Id == raceOfficialId);
            if (raceOfficial is null)
            {
                throw new ValidationException($"Race Official {raceOfficialId} doesn't exist");
            }

            if (raceOfficial.EventId != eventId)
            {
                throw new ValidationException($"Race Official {raceOfficialId} doesn't work in event {eventId}");
            }

            var wages = await _context.Wages
                .AsNoTracking()
                .Include(x => x.RaceOfficial).ThenInclude(x => x.User)
                .Where(x => x.RaceOfficialId == raceOfficialId)
                .Select(x => new WageReadModel
                {
                    Id = x.Id,
                    RaceOfficial = new ()
                    {
                        Id = x.RaceOfficial.Id,
                        Title = x.RaceOfficial.Title,
                        Date = x.RaceOfficial.Date,
                        ArrivalTime = x.RaceOfficial.ArrivalTime.HasValue ? x.RaceOfficial.ArrivalTime.ToString() : null,
                        User = new ()
                        {
                            FullName = $"{x.RaceOfficial.User.Name} {x.RaceOfficial.User.Surname}",
                            Id = x.RaceOfficial.UserId,
                            PhoneNumber = x.RaceOfficial.User.PhoneNumber
                        }
                    },
                    Amount = x.Amount,
                    Note = x.Note,
                    IsTransactionDone = x.Done
                })
                .ToListAsync();

            return wages;
        }

        public async Task<bool> UpdateWage(int eventId, int raceOfficialId, int wageId, WageWriteModel model)
        {
            var @event = await _context.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == eventId);
            if (@event is null)
            {
                throw new ValidationException($"Event {eventId} doesn't exist");
            }

            var raceOfficial = await _context.RaceOfficials.FirstOrDefaultAsync(x => x.Id == raceOfficialId);
            if (raceOfficial is null)
            {
                throw new ValidationException($"Race Official {raceOfficialId} doesn't exist");
            }

            if (raceOfficial.EventId != eventId)
            {
                throw new ValidationException($"Race Official {raceOfficialId} doesn't work in event {eventId}");
            }

            var wage = await _context.Wages.FirstOrDefaultAsync(x => x.Id == wageId);
            if (wage is null)
            {
                return false;
            }

            if (wage.RaceOfficialId != raceOfficialId)
            {
                throw new ValidationException($"Wage {wageId} doesn't relate to Race Official {raceOfficialId}");
            }

            wage.Amount = model.Amount;
            wage.Note = model.Note;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ConfirmTransaction(int eventId, int raceOfficialId, int wageId)
        {
            var @event = await _context.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == eventId);
            if (@event is null)
            {
                throw new ValidationException($"Event {eventId} doesn't exist");
            }

            var raceOfficial = await _context.RaceOfficials.FirstOrDefaultAsync(x => x.Id == raceOfficialId);
            if (raceOfficial is null)
            {
                throw new ValidationException($"Race Official {raceOfficialId} doesn't exist");
            }

            if (raceOfficial.EventId != eventId)
            {
                throw new ValidationException($"Race Official {raceOfficialId} doesn't work in event {eventId}");
            }

            var wage = await _context.Wages.FirstOrDefaultAsync(x => x.Id == wageId);
            if (wage is null)
            {
                return false;
            }

            if (wage.RaceOfficialId != raceOfficialId)
            {
                throw new ValidationException($"Wage {wageId} doesn't relate to Race Official {raceOfficialId}");
            }

            wage.Done = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteWage(int eventId, int raceOfficialId, int wageId)
        {
            var @event = await _context.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == eventId);
            if (@event is null)
            {
                throw new ValidationException($"Event {eventId} doesn't exist");
            }

            var raceOfficial = await _context.RaceOfficials.FirstOrDefaultAsync(x => x.Id == raceOfficialId);
            if (raceOfficial is null)
            {
                throw new ValidationException($"Race Official {raceOfficialId} doesn't exist");
            }

            if (raceOfficial.EventId != eventId)
            {
                throw new ValidationException($"Race Official {raceOfficialId} doesn't work in event {eventId}");
            }

            var wage = await _context.Wages.FirstOrDefaultAsync(x => x.Id == wageId);
            if (wage is null)
            {
                return false;
            }

            if (wage.RaceOfficialId != raceOfficialId)
            {
                throw new ValidationException($"Wage {wageId} doesn't relate to Race Official {raceOfficialId}");
            }

            _context.Wages.Remove(wage);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}