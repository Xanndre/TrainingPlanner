﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Repositories.Repositories
{
    public class BodyMeasurementRepository : BaseRepository, IBodyMeasurementRepository
    {
        public BodyMeasurementRepository(TrainingPlannerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<BodyMeasurement> UpdateBodyMeasurement(BodyMeasurement measurement)
        {
            _trainingPlannerDbContext.Update(measurement);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return measurement;
        }

        public async Task<BodyMeasurement> CreateBodyMeasurement(BodyMeasurement measurement)
        {
            await _trainingPlannerDbContext.BodyMeasurements.AddAsync(measurement);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return measurement;
        }

        public async Task DeleteBodyMeasurement(BodyMeasurement measurement)
        {
            _trainingPlannerDbContext.BodyMeasurements.Remove(measurement);
            await _trainingPlannerDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BodyMeasurement>> GetBodyMeasurements(string userId)
        {
            return await GetBodyMeasurementQuery()
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<BodyMeasurement> GetBodyMeasurement(int id)
        {
            return await GetBodyMeasurementQuery()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<BodyInjury>> GetBodyInjuriesToDelete(BodyMeasurement measurement)
        {
            return await _trainingPlannerDbContext.BodyInjuries
                .Where(c => c.BodyMeasurementId == measurement.Id)
                .Except(measurement.Injuries)
                .ToListAsync();
        }

        public async Task RemoveBodyInjuries(IEnumerable<BodyInjury> injuries, bool isSavingChanges = true)
        {
            _trainingPlannerDbContext.BodyInjuries.RemoveRange(injuries);

            if (isSavingChanges)
            {
                await _trainingPlannerDbContext.SaveChangesAsync();
            }
        }

        private IQueryable<BodyMeasurement> GetBodyMeasurementQuery()
        {
            return _trainingPlannerDbContext.BodyMeasurements
                .Include(t => t.Injuries);
        }
    }
}
