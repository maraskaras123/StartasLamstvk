using Microsoft.EntityFrameworkCore;
using StartasLamstvk.API.Entities;
using StartasLamstvk.Shared;
using StartasLamstvk.Shared.Models.Event;
using StartasLamstvk.Shared.Models.RaceOfficials;
using System.ComponentModel.DataAnnotations;

namespace StartasLamstvk.API.Services
{
    public interface IEventService
    {
        Task<int> CreateEvent(EventWriteModel model);
        Task<EventReadModel> GetEvent(int eventId);
        Task<List<EventReadModel>> GetEvents();
        Task<bool> UpdateEvent(int eventId, EventWriteModel model);
        Task<bool> DeleteEvent(int eventId);
    }

    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateEvent(EventWriteModel model)
        {
            if (!Enum.IsDefined(model.RaceTypeId))
            {
                throw new ValidationException($"Race type {model.RaceTypeId} not found");
            }

            var managerExists = await _context.Users.AnyAsync(x => x.Id == model.ManagerId);
            if (!managerExists)
            {
                throw new ValidationException($"User {model.ManagerId} doesn't exist");
            }

            var @event = new Event
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                AuthorId = await _context.Users.Select(x => x.Id).FirstAsync(),
                ManagerId = model.ManagerId,
                Location = model.Location,
                Championship = model.Championship,
                Title = model.Title,
                Description = model.Description,
                RaceTypeId = model.RaceTypeId
            };

            await _context.Events.AddAsync(@event);
            await _context.SaveChangesAsync();
            return @event.Id;
        }

        public async Task<EventReadModel> GetEvent(int eventId)
        {
            var @event = await _context.Events
                .AsNoTracking()
                .Include(x => x.RaceOfficials).ThenInclude(x => x.Wages)
                .Include(x => x.RaceOfficials).ThenInclude(x => x.User)
                .Include(x => x.Author)
                .Include(x => x.Manager)
                .Include(x => x.UserRacePreferences).ThenInclude(x => x.User).ThenInclude(x => x.LasfCategory)
                .Include(x => x.UserRacePreferences).ThenInclude(x => x.User).ThenInclude(x => x.MotoCategory)
                .Include(x => x.RaceType)
                .ThenInclude(x => x.RaceTypeTranslations.Where(t => t.LanguageCode == Languages.Lt))
                .FirstOrDefaultAsync(x => x.Id == eventId);

            if (@event is null)
            {
                return null;
            }

            var model = new EventReadModel
            {
                Id = @event.Id,
                DateFrom = @event.DateFrom,
                DateTo = @event.DateTo,
                Author = new ()
                {
                    FullName = $"{@event.Author.Name} {@event.Author.Surname}", Id = @event.AuthorId,
                    PhoneNumber = @event.Author.PhoneNumber
                },
                Manager = new ()
                {
                    FullName = $"{@event.Manager.Name} {@event.Manager.Surname}", Id = @event.ManagerId,
                    PhoneNumber = @event.Manager.PhoneNumber
                },
                Location = @event.Location,
                Championship = @event.Championship,
                Title = @event.Title,
                Description = @event.Description,
                RaceType = new ()
                {
                    RaceTypeId = @event.RaceTypeId,
                    Title = @event.RaceType.RaceTypeTranslations.Select(t => t.Text).FirstOrDefault()
                },
                RaceOfficials = @event.RaceOfficials.Select(x => new RaceOfficialReadModel
                {
                    Id = x.Id,
                    User = new ()
                    {
                        FullName = $"{x.User.Name} {x.User.Surname}", Id = x.UserId, PhoneNumber = x.User.PhoneNumber
                    }
                }).ToList()
            };

            return model;
        }

        public async Task<List<EventReadModel>> GetEvents()
        {
            var query = _context.Events
                .AsNoTracking()
                .Include(x => x.RaceOfficials).ThenInclude(x => x.Wages)
                .Include(x => x.Author)
                .Include(x => x.Manager)
                .Include(x => x.UserRacePreferences).ThenInclude(x => x.User).ThenInclude(x => x.LasfCategory)
                .Include(x => x.UserRacePreferences).ThenInclude(x => x.User).ThenInclude(x => x.MotoCategory)
                .Include(x => x.RaceType)
                .ThenInclude(x => x.RaceTypeTranslations.Where(t => t.LanguageCode == Languages.Lt))
                .AsQueryable();

            var events = await query
                .Select(x => new EventReadModel
                {
                    Id = x.Id,
                    DateFrom = x.DateFrom,
                    DateTo = x.DateTo,
                    Author = new ()
                    {
                        FullName = $"{x.Author.Name} {x.Author.Surname}",
                        Id = x.AuthorId,
                        PhoneNumber = x.Author.PhoneNumber
                    },
                    Manager = new ()
                    {
                        FullName = $"{x.Manager.Name} {x.Manager.Surname}",
                        Id = x.ManagerId,
                        PhoneNumber = x.Manager.PhoneNumber
                    },
                    Location = x.Location,
                    Championship = x.Championship,
                    Title = x.Title,
                    Description = x.Description,
                    RaceType = new ()
                    {
                        RaceTypeId = x.RaceTypeId,
                        Title = x.RaceType.RaceTypeTranslations.Select(t => t.Text).FirstOrDefault()
                    },
                    RaceOfficials = x.RaceOfficials.Select(o => new RaceOfficialReadModel
                    {
                        Id = x.Id,
                        User = new ()
                        {
                            FullName = $"{o.User.Name} {o.User.Surname}",
                            Id = o.UserId,
                            PhoneNumber = o.User.PhoneNumber
                        }
                    }).ToList()
                })
                .ToListAsync();

            return events;
        }

        public async Task<bool> UpdateEvent(int eventId, EventWriteModel model)
        {
            if (!Enum.IsDefined(model.RaceTypeId))
            {
                throw new ValidationException($"Race type {model.RaceTypeId} not found");
            }

            var @event = await _context.Events.FirstOrDefaultAsync(x => x.Id == eventId);
            if (@event is null)
            {
                return false;
            }

            @event.DateFrom = model.DateFrom;
            @event.DateTo = model.DateTo;
            @event.AuthorId = await _context.Users.Select(x => x.Id).FirstAsync();
            @event.ManagerId = model.ManagerId;
            @event.Location = model.Location;
            @event.Championship = model.Championship;
            @event.Title = model.Title;
            @event.Description = model.Description;
            @event.RaceTypeId = model.RaceTypeId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEvent(int eventId)
        {
            var @event = await _context.Events.FirstOrDefaultAsync(x => x.Id == eventId);
            if (@event is null)
            {
                return false;
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}