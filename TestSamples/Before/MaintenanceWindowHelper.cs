using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSamples.Before
{
    public static class MaintenanceWindowHelper
    {
        public static string OverlappingWindowsExist(MaintenanceWindow maintenanceWindow)
        {
            if (maintenanceWindow.Status == "Cancelled")
                return string.Empty;

            var unitOfWork = new UnitOfWork();
            var windows =
                unitOfWork.Query<MaintenanceWindow>()
                    .Where(
                        b => b.Id != maintenanceWindow.Id && b.Status != "Cancelled");

            var  overlappingWindow =
                windows.FirstOrDefault(
                    b =>
                        maintenanceWindow.StartDate >= b.StartDate
                        && maintenanceWindow.StartDate < b.EndDate
                        || maintenanceWindow.EndDate > b.StartDate
                        && maintenanceWindow.EndDate <= b.EndDate);

            return  overlappingWindow == null ? string.Empty :  overlappingWindow.Reference;
        }
    }

    public class UnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class MaintenanceWindow
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reference { get; set; }
    }
}


