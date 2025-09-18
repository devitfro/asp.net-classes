using System.ComponentModel.DataAnnotations;

namespace app_hw.Models
{
    public class Matrix
    {
        [Required]
        public int Size { get; set; }

        public double[] MatrixA { get; set; }
        public double[] MatrixB { get; set; }

        public double[] Result { get; set; }
    }
}
