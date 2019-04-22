using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace TodoApi.Services
{
    public class TodoService
    {
        private readonly IMongoCollection<TodoItem> _todoItems;
        public TodoService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("BookstoreDb"));
            var database = client.GetDatabase("TodoApp");
            _todoItems = database.GetCollection<TodoItem>("TodoList");
        }

        public List<TodoItem> Get()
        {
            return _todoItems.Find(TodoItem => true).ToList();
        }

        public TodoItem Get(long id)
        {
            return _todoItems.Find<TodoItem>(todoItem => todoItem.IdProp == id).FirstOrDefault();
        }

        public TodoItem Create(TodoItem TodoItem)
        {
            _todoItems.InsertOne(TodoItem);
            return TodoItem;
        }

        public void Update(long id, TodoItem TodoItemIn)
        {
            _todoItems.ReplaceOne(todoItem => todoItem.IdProp == id, TodoItemIn);
        }

        public void Remove(TodoItem bookIn)
        {
            _todoItems.DeleteOne(todoItem => todoItem.IdProp == todoItem.IdProp);
        }

        public void Remove(long id)
        {
            _todoItems.DeleteOne(TodoItem => TodoItem.IdProp == id);
        }
    }
}

