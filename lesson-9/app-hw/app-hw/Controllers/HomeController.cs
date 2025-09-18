using Microsoft.AspNetCore.Mvc;
using app_hw.Models;

namespace app_hw.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Matrix()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Matrix(int Size)
        {
            var matrix = new Matrix
            {
                Size = Size,
                MatrixA = new double[Size * Size],
                MatrixB = new double[Size * Size]
            };

            return View(matrix);
        }

        [HttpPost]
        public IActionResult Calculate(Matrix matrix, string operation)
        {
            int size = matrix.Size;
            int totalElements = size * size;

            matrix.Result = new double[totalElements];

            if (operation == "Add")
            {
                for (int i = 0; i < totalElements; i++)
                    matrix.Result[i] = matrix.MatrixA[i] + matrix.MatrixB[i];
            }
            else if (operation == "Multiply")
            {
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                    {
                        double sum = 0;
                        for (int k = 0; k < size; k++)
                            sum += matrix.MatrixA[i * size + k] * matrix.MatrixB[k * size + j];
                        matrix.Result[i * size + j] = sum;
                    }
            }

            return View("Result", matrix);
        }
    }
}
