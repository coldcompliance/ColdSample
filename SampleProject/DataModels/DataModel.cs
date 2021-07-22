using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleProject.Data {

    // This is a mock class for data. 
    public class DataModelContext : IDisposable  {

        public DataModelContext() {
            // We want to have load/save.
            var tempPath = System.IO.Path.GetTempPath();
            var database = System.IO.Path.Combine(tempPath, "sampledatabase.json");
            if (System.IO.File.Exists(database)) {
                var data = System.IO.File.ReadAllText(database);
                if (!string.IsNullOrWhiteSpace(data)) {
                    Newtonsoft.Json.JsonConvert.PopulateObject(data, this);
                }
            } else {
                Companies = new List<CompanyEntity>() {
                    new CompanyEntity() { CompanyID = 10, Name = "Microsoft" },
                    new CompanyEntity() { CompanyID = 11, Name = "Clorox" },
                    new CompanyEntity() { CompanyID = 12, Name = "General Electric" }
                };
                Users = new List<UserEntity>() {
                    new UserEntity() { UserID = 100, Name = "John Doe", Email = "john.doe@microsoft.com", WorksForCompanyID = 10 },
                    new UserEntity() { UserID = 101, Name = "Robert Scott", Email = "RobertAScott@jourrapide.com", WorksForCompanyID = 11 },
                    new UserEntity() { UserID = 102, Name = "Michael Howe", Email = "MichaelDHowe@jourrapide.com", WorksForCompanyID = 12 },
                    new UserEntity() { UserID = 103, Name = "George Johnson", Email = "GeorgeTJohnson@rhyta.com", WorksForCompanyID = 11 },
                    new UserEntity() { UserID = 104, Name = "Claude Olson", Email = "ClaudeCOlson@jourrapide.com", WorksForCompanyID = 10 }
                };
            }
        }

        public IList<CompanyEntity> Companies { get; set; } 

        public IList<UserEntity> Users { get; set; }

        public void SaveChanges() {
            // Delete the old
            var tempPath = System.IO.Path.GetTempPath();
            var database = System.IO.Path.Combine(tempPath, "sampledatabase.json");
            if (System.IO.File.Exists(database)) {
                System.IO.File.Delete(database);
            }
            // Write new
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            using (var stream = System.IO.File.CreateText(database)) {
                stream.Write(data);
                stream.Flush();
                stream.Close();
            }
        }

        public void Dispose() {
            // Since most contexts are IDisposable this is here to mark the class as idisposble
        }
    }

    public class CompanyEntity {

        public int CompanyID { get; set; }

        public string Name { get; set; }



    }
    public class UserEntity {
        
        public int UserID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int WorksForCompanyID { get; set; }

    }
}