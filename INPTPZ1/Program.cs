namespace INPTPZ1
{
    class Program
    {
        static void Main(string[] args)
        {
            NewtonsFractalSolver solver = new NewtonsFractalSolver();

            solver.ParseArguments(args);

            solver.RenderImage();

            solver.SaveImage();
        }
    }
}
