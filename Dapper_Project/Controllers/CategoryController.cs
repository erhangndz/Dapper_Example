using Dapper;
using Dapper_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Dapper_Project.Controllers
{
    public class CategoryController : Controller
    {
        private readonly string _connection = "server=ERHAN\\SQLEXPRESS;database=DbNews;integrated security=true";

        public async Task<IActionResult> Index()
        {
            await using var connection = new SqlConnection(_connection);
            var values = await connection.QueryAsync<ResultCategoryViewModel>("select * from category");
            return View(values);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(ResultCategoryViewModel model)
        {
            await using var connection = new SqlConnection(_connection);
            var query = $"insert into category  (categoryname,categorystatus) values ('{model.CategoryName}','true')";
            await connection.ExecuteAsync(query);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await using var connection = new SqlConnection(_connection);
            var query = $"delete from category where categoryId='{id}'";
            await connection.ExecuteAsync(query);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            await using var connection = new SqlConnection(_connection);
            var values = await connection.QueryFirstAsync<ResultCategoryViewModel>($"select * from category where categoryID='{id}'");
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(ResultCategoryViewModel model)
        {
            await using var connection = new SqlConnection(_connection);
            var query = $"update  category set categoryname = '{model.CategoryName}',categorystatus= '{model.CategoryStatus}' where CategoryID='{model.CategoryID}'";
            await connection.ExecuteAsync(query);
            return RedirectToAction("Index");
        }
    }

}
