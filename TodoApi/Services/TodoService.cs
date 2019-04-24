using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Threading.Tasks;
using System;

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

        public Task<List<TodoItem>> Get()
        {
            return _todoItems.Find(TodoItem => true).ToListAsync();
        }

        public Task<TodoItem> Get(string id)
        {
            return _todoItems.Find(todoItem => todoItem.Id == id).SingleAsync();
        }

        public TodoItem Create(TodoItem TodoItem)
        {
            _todoItems.InsertOne(TodoItem);
            return TodoItem;
        }

        public void Update(string id, TodoItem TodoItemIn)
        {
            _todoItems.ReplaceOne(todoItem => todoItem.Id == id, TodoItemIn);
        }

        public void Remove(TodoItem TodoItemIn)
        {
            _todoItems.DeleteOne(todoItem => todoItem.Id == TodoItemIn.Id);
        }

        public void Remove(string id)
        {
            _todoItems.DeleteOne(TodoItem => TodoItem.Id == id);
        }
    }
}

