using System;
using System.Collections.Generic;
using System.Linq;                                                                                                                              
using System.Net;
using System.Net.Http;
using MongoDB.Driver;
using MongoDB.Bson;
using CrudWithMongoDB.Models;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace CrudWithMongoDB.Controllers
{
    [RoutePrefix("Api/Employee")]
    public class EmpController : ApiController
    {

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("InsertEmployee")]
       
        public object Addemployee(Employee objVM)
        {
            try
            {   ///Insert Emoloyeee  
                #region InsertDetails  
                if (objVM.Id == null)
                {
                    string constr = ConfigurationManager.AppSettings["connectionString"];
                    var Client = new MongoClient(constr);
                    var DB = Client.GetDatabase("Employee");
                    var collection = DB.GetCollection<Employee>("EmployeeDetails");
                    collection.InsertOne(objVM);
                    return new Status
                    { Result = "Success", Message = "Employee Details Insert Successfully" };
                }
                #endregion
                ///Update Emoloyeee  
                #region updateDetails  
                else
                {
                    string constr = ConfigurationManager.AppSettings["connectionString"];
                    var Client = new MongoClient(constr);
                    var Db = Client.GetDatabase("Employee");
                    var collection = Db.GetCollection<Employee>("EmployeeDetails");
                    var update = collection.FindOneAndUpdateAsync(Builders<Employee>.Filter.Eq("Id", objVM.Id), Builders<Employee>.Update.Set("Name", objVM.Name).Set("Department", objVM.Department).Set("Address", objVM.Address).Set("City", objVM.City).Set("Country", objVM.Country));
                    return new Status
                    { Result = "Success", Message = "Employee Details Update Successfully" };
                }
                #endregion
            }
            catch (Exception ex)
            {
                return new Status
                { Result = "Error", Message = ex.Message.ToString() };
            }
        }
        #region Getemployeedetails  
        [System.Web.Http.Route("GetAllEmployee")]
        [System.Web.Http.HttpGet]
        public object GetAllEmployee()
        {
            string constr = ConfigurationManager.AppSettings["connectionString"];
            var Client = new MongoClient(constr);
            var db = Client.GetDatabase("Employee");
            var collection = db.GetCollection<Employee>("EmployeeDetails").Find(new BsonDocument()).ToList();
            return Json(collection);
        }
        #endregion
        #region EmpdetaisById  
        [System.Web.Http.Route("GetEmployeeById")]
        [System.Web.Http.HttpGet]
        public object GetEmployeeById(string id)
        {
            string constr = ConfigurationManager.AppSettings["connectionString"];
            var Client = new MongoClient(constr);
            var DB = Client.GetDatabase("Employee");
            var collection = DB.GetCollection<Employee>("EmployeeDetails");
            var plant = collection.Find(Builders<Employee>.Filter.Where(s => s.Id == id)).FirstOrDefault();
            return Json(plant);
        }
        #endregion
        #region DeleteEmployee  
        [System.Web.Http.Route("Delete")]
        [System.Web.Http.HttpGet]
        public object Delete(string id)
        {
            try
            {
                string constr = ConfigurationManager.AppSettings["connectionString"];
                var Client = new MongoClient(constr);
                var DB = Client.GetDatabase("Employee");
                var collection = DB.GetCollection<Employee>("EmployeeDetails");
                var DeleteRecored = collection.DeleteOneAsync(
                               Builders<Employee>.Filter.Eq("Id", id));
                return new Status
                { Result = "Success", Message = "Employee Details Delete  Successfully" };
            }
            catch (Exception ex)
            {
                return new Status
                { Result = "Error", Message = ex.Message.ToString() };
            }
        }
        #endregion
    }
}