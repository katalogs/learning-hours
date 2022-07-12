using ArchUnit.Kata.Examples.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArchUnit.Kata.Examples.Controllers
{
    [ApiController]
    public class Controller
    {
        public ApiResponse<int> Matching() => new ApiResponse<int>(42);
        public void NotMatching() { }
        public int Universe() => 42;
    }
}