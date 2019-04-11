using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Norm;
using WebApplication1.Contracts;
using IMongoDatabase = Norm.IMongoDatabase;


namespace WebApplication1.Repository
{
    public class RepositoryBase: IRepositoryBase
    {
        private IMongo _provider;
        private IMongoDatabase _db { get { return this._provider.Database; } }
//        
//        private readonly IConfiguration _configuration;
        public RepositoryBase(IConfiguration configuration)
        {
//            _configuration = configuration;
            _provider = Mongo.Create(configuration.GetSection("ConnectionStrings:ConnectionString").Value);
        }

//        private readonly DbContext _db = null;
//        
//        public RepositoryBase(IConfiguration configuration)
//        {
//           
//            _db = new DbContext(configuration)
//        }

//        private IMongoDatabase _db = null;
//
//        public RepositoryBase(IConfiguration configuration)
//        {
//            var client = new MongoClient(configuration.GetSection("ConnectionStrings:ConnectionString").Value);
//            _db = client.GetDatabase(configuration.GetSection("ConnectionStrings:Database").Value);
//        }

        public void Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            var items = All<T>().Where(expression);
            foreach (T item in items)
            {
                Delete(item);
            }
        }
        public void Delete<T>(T item) where T : class, new()
        {
            // Remove the object.
            _db.GetCollection<T>().Delete(item);
        }
        public void DeleteAll<T>() where T : class, new()
        {
            _db.DropCollection(typeof(T).Name);
        }
        public T Single<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            return All<T>().Where(expression).SingleOrDefault();
        }
        public IQueryable<T> All<T>() where T : class, new()
        {
            return _db.GetCollection<T>().AsQueryable();
        }
        
        public void Add<T>(T item) where T : class, new()
        {
            _db.GetCollection<T>().Save(item);
        }
        public void Add<T>(IEnumerable<T> items) where T : class, new()
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }
        public void Dispose()
        {
            _provider.Dispose();
        }
    }
}